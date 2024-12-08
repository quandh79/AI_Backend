using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_User
    {
        public BCC01_User()
        {
            BCC01_MapProfileUser = new HashSet<BCC01_MapProfileUser>();
            //BCC01_MapUserReportTos = new HashSet<BCC01_MapUserReportTo>();
        }

        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        public Guid role_id { get; set; }
        public string report_to { get; set; }
        public string avatar { get; set; }
        public string reason_deactive { get; set; }
        public DateTime? block_time { get; set; }
        public int wrong_password_number { get; set; }
        public string language { get; set; }
        public bool? is_supervisor { get; set; }
        public bool? is_agent { get; set; }
        public string extension_number { get; set; }
        public string password { get; set; }
        public string extension_password { get; set; }
        public bool? is_user_ldap { get; set; }

        public virtual ICollection<BCC01_MapProfileUser> BCC01_MapProfileUser { get; set; }

        [ForeignKey("role_id")]
        public virtual BCC01_RoleHierarchy BCC01_RoleHierarchy { get; set; }
        [ForeignKey("tenant_id")]
        public virtual BCC01_Tenants BCC01_Tenants { get; set; }
        public int? type_extension { get; set; }
        public DateTime? last_time_2fa { get; set; }
        //public DateTime? next_time_2fa { get; set; }
        public bool? is_first_login { get; set; }
        public string code_verify { get; set; }
        public string code_verify_change_pass { get; set; }
        public DateTime? time_expired_code_verify { get; set; }
        public DateTime? time_expired_code_verify_change_pass { get; set; }
        public string user_window { get; set; }
        //public virtual ICollection<BCC01_MapUserReportTo> BCC01_MapUserReportTos { get; set; }
        public string dtv { get; set; }
    }
}
