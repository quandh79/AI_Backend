using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_RoleHierarchy
    {
        public Guid id { get; set; }
        public string role_name { get; set; }
        public Guid role_parent_id { get; set; }
        public string description { get; set; }
        public bool is_viewdata_all_level { get; set; }
        public bool is_editdata_all_level { get; set; }
        public bool is_removedata_all_level { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}
