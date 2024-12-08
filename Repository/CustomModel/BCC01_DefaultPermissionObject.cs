using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.EF
{
    public partial class BCC01_DefaultPermissionObject
    {
        public Guid id { get; set; }
        public string object_name { get; set; }
        public Guid module_id { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }

        public bool? is_allow_agent { get; set; }
        public bool? is_allow_sup { get; set; }
        public bool? is_allow_admin { get; set; }
    }
}
