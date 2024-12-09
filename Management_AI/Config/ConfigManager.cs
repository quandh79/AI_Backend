using Common.Commons;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Management_AI.Config
{
    public class ConfigManager
    {
        public static IConfiguration _configuration = ConfigContainerDJ.CreateInstance<IConfiguration>();
        private static List<string> lstKeyAppsettingEncrypt = new List<string>()
        {
            "DefaultConnection",
            "SECRET_KEY",
            "PASS_REDIS",
            "API_SECRET_KEY_IC",
            "API_SECRET_KEY",
            "Password",
            "CACertFingerprint"
        };

        public static string Get(string nameConfig)
        {
            var value = _configuration.GetSection(nameConfig).Value;
            if (lstKeyAppsettingEncrypt.Contains(nameConfig))
            {
                return EncryptHelper.AES_DecryptText(value, EncryptHelper.SecurityKey);
            }
            else
            {
                return value;
            }
        }

        public static string Get(string nameConfig, string key)
        {
            var value = _configuration.GetSection(nameConfig)[key];

            if (lstKeyAppsettingEncrypt.Contains(key))
            {
                return EncryptHelper.AES_DecryptText(value, EncryptHelper.SecurityKey);
            }
            else
            {
                return value;
            }
        }
    }
}
