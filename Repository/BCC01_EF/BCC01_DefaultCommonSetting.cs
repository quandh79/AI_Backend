using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_DefaultCommonSetting
    {
        public Guid id { get; set; }
        public string setting_key { get; set; }
        public string value { get; set; }
        public string description { get; set; }
        public string common_type { get; set; }
        public bool only_root { get; set; }
        public string setting_for { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}
