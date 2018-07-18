﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Ocuda.Ops.Service.Interfaces.Ops.Services;
using Ocuda.Utility.Helpers;
using Ocuda.Utility.Keys;

namespace Ocuda.Ops.Controllers.Filter
{
    public class RestoreModelStateAttribute : ActionFilterAttribute
    {
        private readonly ILogger<RestoreModelStateAttribute> _logger;
        private readonly ISiteSettingService _siteSettingService;
        public RestoreModelStateAttribute(ILogger<RestoreModelStateAttribute> logger,
            ISiteSettingService siteSettingService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _siteSettingService = siteSettingService 
                ?? throw new ArgumentNullException(nameof(siteSettingService));
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
                                         ActionExecutionDelegate next)
        {
            var controller = context.Controller as Controller;

            var key = ModelStateHelpers.GetModelStateKey(context.RouteData.Values);

            var modelStateStorage = controller?.TempData[key] as string;

            if (modelStateStorage != null)
            {
                var storage = ModelStateHelpers.DeserializeModelState(modelStateStorage);
                var timeDifference = DateTimeOffset.Now.ToUnixTimeSeconds() - storage.time;
                var modelstateTimeOut = await _siteSettingService
                    .GetSettingIntAsync(SiteSettingKey.ModelState.TimeOutMinutes);
                if (TimeSpan.FromSeconds(timeDifference).Minutes < modelstateTimeOut 
                    || modelstateTimeOut < 1)
                {
                    //Only Import if we are viewing
                    if (context.Result is ViewResult)
                    {
                        context.ModelState.Merge(storage.modelState);
                    }
                }
                else
                {
                    _logger.LogError($"ModelState timed out for key {key}.");
                }
            }

            await next();
        }
    }
}