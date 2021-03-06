﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocuda.Ops.Service.Interfaces.Ops.Services;

namespace Ocuda.Ops.Service
{
    public class InitialSetupService : IInitialSetupService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISiteSettingService _siteSettingService;
        private readonly IUserService _userService;

        public InitialSetupService(ILogger<InitialSetupService> logger,
            IConfiguration configuration,
            IAuthorizationService authorizationService,
            ISiteSettingService siteSettingService,
            IUserService userService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authorizationService = authorizationService
                ?? throw new ArgumentNullException(nameof(authorizationService));
            _siteSettingService = siteSettingService
                ?? throw new ArgumentNullException(nameof(siteSettingService));
            _userService = userService
                ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task VerifyInitialSetupAsync()
        {
            // ensure the sysadmin user exists
            var sysadminUser = await _userService.EnsureSysadminUserAsync();
            await _siteSettingService.EnsureSiteSettingsExistAsync(sysadminUser.Id);

            var siteManagerGroup = _config[Utility.Keys.Configuration.OpsSiteManagerGroup];
            if (!string.IsNullOrEmpty(siteManagerGroup))
            {
                await _authorizationService.EnsureSiteManagerGroupAsync(sysadminUser.Id,
                    siteManagerGroup);
            }
        }
    }
}
