using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_Permission
    {
        public Guid id { get; set; }
        public Guid profile_id { get; set; }
        public Guid permissionobject_id { get; set; }
        public string object_name { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        public bool is_show { get; set; }
        public bool is_active { get; set; }
        public string description { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        [ForeignKey("permissionobject_id")]
        public virtual BCC01_PermissionObject BCC01_PermissionObject { get; set; }
        [ForeignKey("profile_id")]
        public virtual BCC01_Profile BCC01_Profile { get; set; }
    }
}
