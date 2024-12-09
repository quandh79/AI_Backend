using Common.CustomAttributes;
using System;

namespace Management_AI.Models.Main
{
    public class DefaultCommonSettingAddRequest : BaseModelSQL
    {
        public string setting_key { get; set; }
        public string value { get; set; }
        public string description { get; set; }
        [OneOf(new string[] { "string", "number", "date", "datetime", "boolean", "color", "password" })]
        public string common_type { get; set; }
        public bool only_root { get; set; }
        [OneOf(new string[] { "common", "auto_service", "size_file", "email_service" })]
        public string setting_for { get; set; }
    }
}
