﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocuda.Ops.Service.Interfaces.Ops.Services;
using Ocuda.Utility.Helpers;
using Ocuda.Utility.Keys;

namespace Ocuda.Ops.Controllers.Filters
{
    public class AuthenticationFilterAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly ILogger<AuthenticationFilterAttribute> _logger;
        private readonly IConfiguration _config;
        private readonly IDistributedCache _cache;
        private readonly WebHelper _webHelper;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILdapService _ldapService;
        private readonly IUserService _userService;

        public AuthenticationFilterAttribute(ILogger<AuthenticationFilterAttribute> logger,
            IConfiguration configuration,
            IDistributedCache cache,
            WebHelper webHelper,
            IAuthorizationService authorizationService,
            ILdapService ldapService,
            IUserService userService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _webHelper = webHelper ?? throw new ArgumentNullException(nameof(webHelper));
            _authorizationService = authorizationService
                ?? throw new ArgumentNullException(nameof(authorizationService));
            _ldapService = ldapService
                ?? throw new ArgumentNullException(nameof(ldapService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context,
            ResourceExecutionDelegate next)
        {
            var now = DateTime.Now;
            var authRedirectUrl = _config[Configuration.OpsAuthRedirect];

            if (!string.IsNullOrEmpty(authRedirectUrl))
            {
                var httpContext = context.HttpContext;

                // check the current user's Username claim to see if they're authenticated
                var usernameClaim = httpContext.User.Claims
                    .FirstOrDefault(_ => _.Type == ClaimType.Username);

                bool authenticateUser = usernameClaim == null;

                if (!authenticateUser)
                {
                    // user is logged in, ensure they exist in the database
                    // TODO probably figure out how to make this use the database less
                    string username = usernameClaim.Value;
                    var user = await _userService.LookupUserAsync(username);
                    if (user?.ReauthenticateUser != false)
                    {
                        // user does not exist in the database or needs to be reauthenticated
                        authenticateUser = true;
                        await httpContext.SignOutAsync();
                    }
                }

                if (authenticateUser)
                {
                    // by default time out cookies and distributed cache in 2 minutes
                    int authTimeoutMinutes = 2;

                    var configuredAuthTimeout = _config[Configuration.OpsAuthTimeoutMinutes];
                    if (configuredAuthTimeout != null
                        && !int.TryParse(configuredAuthTimeout, out authTimeoutMinutes))
                    {
                        _logger.LogWarning($"Configured {Configuration.OpsAuthTimeoutMinutes} could not be converted to a number. It should be a number of minutes (defaulting to 2).");
                    }

                    // all authentication bits will expire after 2 minutes
                    var authenticationExpiration = new TimeSpan(0, authTimeoutMinutes, 0);

                    var cacheExpiration = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(authenticationExpiration);

                    // check existing authentication id cookie
                    var id = httpContext.Request.Cookies[Cookie.OpsAuthId];
                    if (id == null)
                    {
                        // create a new authentication id cookie if none exists
                        id = Guid.NewGuid().ToString();
                        httpContext.Response.Cookies.Append(Cookie.OpsAuthId,
                            id,
                            new Microsoft.AspNetCore.Http.CookieOptions
                            {
                                IsEssential = true,
                                MaxAge = authenticationExpiration
                            }
                        );
                    }

                    // check if there's a username stored in the cache
                    // if so, the authentication has redirected us back here
                    var username
                        = await _cache.GetStringAsync(string.Format(Cache.OpsUsername, id));

                    if (string.IsNullOrEmpty(username))
                    {
                        // if there is no username: set a return url and redirect to authentication
                        await _cache.SetStringAsync(string.Format(Cache.OpsReturn, id),
                            _webHelper.GetCurrentUrl(httpContext),
                            cacheExpiration);

                        string cacheDiscriminator
                            = _config[Configuration.OpsDistributedCacheInstanceDiscriminator]
                            ?? string.Empty;

                        var url = string.Format(authRedirectUrl, id, cacheDiscriminator);

                        httpContext.Response.Redirect(url);
                        return;
                    }
                    else
                    {
                        // remove the username from the cache
                        await _cache.RemoveAsync(string.Format(Cache.OpsUsername, id));

                        // check if there's a domain name specified and strip it from the username
                        var domainName = _config[Configuration.OpsDomainName];
                        if (!string.IsNullOrEmpty(domainName) && username.StartsWith(domainName))
                        {
                            username = username.Substring(domainName.Length + 1);
                        }

                        var user = await _userService.LookupUserAsync(username);
                        var newUser = user == null;
                        if (newUser)
                        {
                            user = new Models.Entities.User
                            {
                                Username = username,
                                LastSeen = now
                            };
                        }

                        // perform ldap update of user object
                        user = _ldapService.LookupByUsername(user);

                        // if the user is new, add them to the database
                        if (newUser)
                        {
                            var rosterUser = await _userService.LookupUserByEmailAsync(user.Email);
                            if (rosterUser != null)
                            {
                                user = await _userService.UpdateRosterUserAsync(rosterUser.Id, user);
                            }
                            else
                            {
                                user = await _userService.AddUser(user);
                            }
                        }
                        else
                        {
                            await _userService.LoggedInUpdateAsync(user);
                        }

                        string userId = user.Id.ToString();

                        // start creating the user's claims with their username
                        var claims = new HashSet<Claim>
                        {
                            new Claim(ClaimType.Username, username),
                            new Claim(ClaimType.UserId, userId),
                            new Claim(ClaimType.AuthenticatedAt, now.ToString("O"))
                        };

                        // loop through groups in the distributed cache from authentication
                        // prime the loop
                        int groupId = 1;
                        var adGroupName
                            = await _cache.GetStringAsync(string.Format(Cache.OpsGroup,
                                id,
                                groupId));
                        var adGroupNames = new List<string>();
                        while (!string.IsNullOrEmpty(adGroupName))
                        {
                            adGroupNames.Add(adGroupName);
                            // once it's in our list, remove it from the cache
                            await _cache.RemoveAsync(string.Format(Cache.OpsGroup, id, groupId));
                            groupId++;
                            adGroupName = await _cache.GetStringAsync(string.Format(Cache.OpsGroup,
                                id,
                                groupId));
                        }

                        bool isSiteManager = false;

                        // pull lists of AD groups that should be site and section managers
                        var claimGroups = await _authorizationService.GetClaimGroupsAsync();
                        var sectionManagerGroups
                            = await _authorizationService.GetSectionManagerGroupsAsync();

                        var sectionManagerOf = new List<string>();
                        var claimantOf = new Dictionary<string, string>();

                        // loop through group names and look up if each group provides claims
                        // claims can be provided via ClaimGroups or SectionManagerGroups
                        foreach (string groupName in adGroupNames)
                        {
                            claims.Add(new Claim(ClaimType.ADGroup, groupName));

                            // once the user is a site manager, we can stop looking up more rights
                            if (!isSiteManager)
                            {
                                var claimList = claimGroups.Where(_ => _.GroupName == groupName);
                                foreach (var claim in claimList)
                                {
                                    if (!claimantOf.ContainsKey(claim.ClaimType))
                                    {
                                        claimantOf.Add(claim.ClaimType, groupName);
                                    }
                                }

                                var sectionsManaged =
                                    sectionManagerGroups.Where(_ => _.GroupName == groupName);

                                foreach (var sectionManaged in sectionsManaged)
                                {
                                    if (!sectionManagerOf.Contains(sectionManaged.SectionName))
                                    {
                                        sectionManagerOf.Add(sectionManaged.SectionName);
                                    }
                                }

                                if (claimantOf.ContainsKey(ClaimType.SiteManager))
                                {
                                    isSiteManager = true;
                                }
                            }
                        }

                        if (isSiteManager)
                        {
                            // if the user is a site manager, add the site manager claim
                            claims.Add(new Claim(ClaimType.SiteManager,
                                ClaimType.SiteManager));

                            // also add each individual permission claim
                            foreach (var claimType in claimGroups)
                            {
                                claims.Add(new Claim(claimType.ClaimType,
                                    ClaimType.SiteManager));
                            }
                        }
                        else
                        {
                            // add permission claims
                            foreach (var claim in claimantOf)
                            {
                                claims.Add(new Claim(claim.Key, claim.Value));
                            }

                            // add section management claims
                            foreach (var sectionName in sectionManagerOf)
                            {
                                claims.Add(new Claim(ClaimType.SectionManager,
                                    sectionName.ToLower()));
                            }
                        }

                        // TODO: probably change the role claim type to our roles and not AD groups
                        var identity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            ClaimType.Username,
                            ClaimType.ADGroup);

                        await httpContext.SignInAsync(new ClaimsPrincipal(identity));

                        // remove the return URL from the cache
                        await _cache.RemoveAsync(string.Format(Cache.OpsReturn, id));

                        httpContext.Response.Cookies.Delete(Cookie.OpsAuthId);

                        // TODO set a reasonable initial nickname
                        httpContext.Items[ItemKey.Nickname] = user.Nickname ?? username;
                    }
                }
            }

            await next();
        }
    }
}
