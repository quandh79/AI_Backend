using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;


namespace Common.Commons
{
    public class ConfigHelper
    {
        private static string jsonFileName = "appsettings.json";

        private static List<string> lstKeyAppsettingEncrypt = new List<string>()
        {
            "DefaultConnection",
            "SECRET_KEY",
            "PASS_REDIS",
            "API_SECRET_KEY_IC",
            "API_SECRET_KEY",
            "Password",
            "CACertFingerprint",
            "Password_CRM",
            "Pass_Login",
            "Basic_Authen",
            "ConnectionStrings",
            "BCC01_Connection",
            "BCC03_Connection",
            "PASS_AUDIO",
            "PASS_FTP",
            "FILE_ENCRYPTION_KEY"
        };


        public static string Get(string nameConfig)
        {
            var value = new ConfigurationBuilder().AddJsonFile(jsonFileName).Build().GetSection(nameConfig).Value;
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
            var value = new ConfigurationBuilder().AddJsonFile(jsonFileName).Build().GetSection(nameConfig)[key];
            if (lstKeyAppsettingEncrypt.Contains(nameConfig))
            {
                return EncryptHelper.AES_DecryptText(value, EncryptHelper.SecurityKey);
            }
            else
            {
                return value;
            }
        }
        public static string GetByKey(string nameConfig, string key)
        {
            var value = new ConfigurationBuilder().AddJsonFile(jsonFileName).Build().GetSection(nameConfig)[key];
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
