﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ocuda.Promenade.Models.Entities;
using Ocuda.Promenade.Service.Interfaces.Repositories;

namespace Ocuda.Promenade.Service
{
    public class SiteSettingService
    {
        private readonly ILogger _logger;
        private readonly ISiteSettingRepository _siteSettingRepository;

        public SiteSettingService(ILogger<SiteSettingService> logger,
            ISiteSettingRepository siteSettingRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _siteSettingRepository = siteSettingRepository
                ?? throw new ArgumentNullException(nameof(siteSettingRepository));
        }

        public async Task<bool> GetSettingBoolAsync(string key)
        {
            var settingValue = await GetSettingValueAsync(key);

            if (bool.TryParse(settingValue, out bool result))
            {
                return result;
            }
            else
            {
                _logger.LogError($"Invalid value for promenade boolean setting {key}: {settingValue}");

                var defaultSetting = GetDefaultSetting(key);
                if (bool.TryParse(defaultSetting.Value, out bool defaultResult))
                {
                    return defaultResult;
                }
                else
                {
                    _logger.LogCritical($"Invalid default value for promenade boolean setting {key}: {settingValue}");
                    return default;
                }
            }
        }


        public async Task<int> GetSettingIntAsync(string key)
        {
            var settingValue = await GetSettingValueAsync(key);

            if (int.TryParse(settingValue, out int result))
            {
                return result;
            }
            else
            {
                _logger.LogError($"Invalid value for promenade integer setting {key}: {settingValue}");

                var defaultSetting = GetDefaultSetting(key);
                if (int.TryParse(defaultSetting.Value, out int defaultResult))
                {
                    return defaultResult;
                }
                else
                {
                    _logger.LogCritical($"Invalid default value for promenade integer setting {key}: {settingValue}");
                    return default;
                }
            }
        }

        public async Task<string> GetSettingStringAsync(string key)
        {
            return await GetSettingValueAsync(key);
        }

        private async Task<string> GetSettingValueAsync(string key)
        {
            var siteSetting = await _siteSettingRepository.FindByKeyAsync(key);
            if (siteSetting == null)
            {
                _logger.LogError($"Promenade site setting key \"{key}\" not found.");
                siteSetting = GetDefaultSetting(key);
            }

            return siteSetting.Value;
        }

        private SiteSetting GetDefaultSetting(string key)
        {
            return Models.Defaults.SiteSettings.Get.Where(_ => _.Key == key).Single();
        }
    }
}
