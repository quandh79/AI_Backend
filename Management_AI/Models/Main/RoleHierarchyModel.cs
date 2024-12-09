using System;

namespace Management_AI.Models.Main
{
    public class RoleHierarchyRequest : BaseModelSQL
    {
        public string role_name { get; set; }
        public Guid role_parent_id { get; set; }
        public string description { get; set; }
        public bool is_viewdata_all_level { get; set; }
        public bool is_editdata_all_level { get; set; }
        public bool is_removedata_all_level { get; set; }
    }
}
