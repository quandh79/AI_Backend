using System;
using System.Collections.Generic;

namespace Repository.CustomModel
{
    public class RoleModel
    {
        public Guid id { get; set; }
        public string role_name { get; set; }
        public Guid role_parent_id { get; set; }
        public string description { get; set; }
        public bool is_viewdata_all_level { get; set; }
        public bool is_editdata_all_level { get; set; }
        public bool is_removedata_all_level { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
        public List<RoleModel> childRoles { get; set; } = new List<RoleModel>();
    }
}
