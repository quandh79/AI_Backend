using Repository.BCC01_EF;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.CustomModel
{
    public class UserModel
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public Guid role_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool is_active { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
        public string report_to { get; set; }
        public System.Guid profile_id { get; set; }
        public System.Guid skill_id { get; set; }
        public string language { get; set; }
        public Nullable<bool> is_supervisor { get; set; }
        public Nullable<bool> is_agent { get; set; }
        public int type_extension { get; set; }
        public List<string> lstReportTo { get; set; }

    }
    public class UserCustomResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string role_name { get; set; }
        public Guid role_id { get; set; }
        public Guid role_parent_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public Nullable<bool> is_supervisor { get; set; }
        public Nullable<bool> is_agent { get; set; }
        public bool is_active { get; set; }
        public string report_to { get; set; }
        public string avatar { get; set; }
        public string reason_deactive { get; set; }
        public string language { get; set; }
        public Nullable<System.DateTime> block_time { get; set; }
        public int wrong_password_number { get; set; }
        public string extension_number { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        public int? type_extension { get; set; }
        public string username_jabber { get; set; }
        public bool? is_sendMailInfoJabber { get; set; }
        public bool? is_user_ldap { get; set; }
        public string user_window { get; set; }
        public string errorCreateCUCM { get; set; }
        /// <summary>
        ///  Report to multi Suppervider
        /// </summary>
        public List<string> lstReportTo { get; set; }
        public string dtv { get; set; }
    }
    public class UsernameRequest
    {
        public string username { get; set; }
        public System.Guid tenant_id { get; set; }
    }
    public class UserRoleResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public Guid role_id { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string report_to { get; set; }
        public string avatar { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public Nullable<bool> is_supervisor { get; set; }
        public Nullable<bool> is_agent { get; set; }

    }
}
