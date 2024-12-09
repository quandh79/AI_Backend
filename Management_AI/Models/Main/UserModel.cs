using Common.Params.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Management_AI.Models.Main
{
    public class UserState
    {
        public UserState(string username, string extension_number, string fullname, string avatar, string phone, string email, bool is_administrator, bool is_rootuser, Guid state_id, string role_id, string report_to, string state, string state_detail, bool? is_supervisor, bool? is_agent, DateTime last_change_state_time, int? prefix_extension = 0, string state_name = "", string agentgroup_id = null)
        {
            this.username = username;
            this.extension_number = extension_number;
            this.fullname = fullname;
            this.avatar = avatar;
            this.phone = phone;
            this.email = email;
            this.role_id = role_id;
            this.report_to = report_to;
            this.is_administrator = is_administrator;
            this.is_rootuser = is_rootuser;
            this.state_id = state_id;
            this.state = state;
            this.state_name = state_name;
            this.state_detail = state_detail;
            this.is_supervisor = is_supervisor == null ? false : is_supervisor;
            this.is_agent = is_agent == null ? false : is_agent;
            this.last_change_state_time = last_change_state_time;
            this.prefix_extension = prefix_extension;
            this.agentgroup_id = agentgroup_id;
        }
        public string username { get; set; }
        public string extension_number { get; set; }
        public string fullname { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string state_name { get; set; }
        public Guid state_id { get; set; }
        public string state { get; set; }
        public string state_detail { get; set; }
        public string role_id { get; set; }
        public string report_to { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool? is_supervisor { get; set; }
        public bool? is_agent { get; set; }
        public DateTime last_change_state_time { get; set; }
        public int? prefix_extension { get; set; }
        public string agentgroup_id { get; set; }
    }
    public class ExtensionReadyResquest
    {
        public Guid tenant_id { get; set; }
        public string username { get; set; }
    }
    public class UserRequest : BaseModelSQL
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string fullname { get; set; }
        public string phone { get; set; }
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Input valid email address!")]
        public string email { get; set; }
        [Required]
        //  [RegularExpression(@"^.*(?=.{8})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must be  least  8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string password { get; set; }
        public string description { get; set; }
        [Required]
        public string role_name { get; set; }
        [Required]
        public string role_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_active { get; set; }
        public string report_to { get; set; }
        public string avatar { get; set; }
        public string language { get; set; }
        public string extension_number { get; set; }
        public string extension_password { get; set; }
        public bool? is_supervisor { get; set; }
        public bool? is_agent { get; set; }
        public bool is_create_extension { get; set; }
        public int type_extension { get; set; }
        public bool? is_user_ldap { get; set; }
        public string user_window { get; set; }
        /// <summary>
        /// TH Agent reportTo cho nhiều Sup
        /// </summary>
        public List<string> lstReportTo { get; set; }
        public Guid? teamId { get; set; }
        public string dtv { get; set; }

    }
    public class UserUpdateRequest : BaseModelSQL
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Input valid email address!")]
        public string email { get; set; }
        public string description { get; set; }
        [Required]
        public string role_name { get; set; }
        [Required]
        public string role_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_active { get; set; }
        public string report_to { get; set; }
        public string avatar { get; set; }
        public string language { get; set; }
        public bool? is_supervisor { get; set; }
        public bool? is_agent { get; set; }
        public string extension_number { get; set; }
        public int? type_extension { get; set; } // 0: ko sd extension, 1: sinh ra ex mới, 2: sử dụng extension từ client gửi lên
        public string extension_password { get; set; }
        public bool? is_user_ldap { get; set; }
        public string password_new { get; set; }
        public string user_window { get; set; }

        /// <summary>
        /// TH  Agent reportTo cho nhiều Sup
        /// </summary>
        public List<string> lstReportTo { get; set; }
        public Guid? teamId { get; set; }
        public string dtv { get; set; }
        public bool? flag_SSO { get; set; }

    }
    public class AssignRequest
    {
        public string current_user { get; set; }
        public string assign_user { get; set; }
    }
}
