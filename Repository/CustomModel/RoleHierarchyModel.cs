namespace Repository.CustomModel
{
    public class RoleHierarchyResponse
    {
        public System.Guid id { get; set; }
        public string role_name { get; set; }
        public System.Guid role_parent_id { get; set; }
        public string description { get; set; }
        public bool is_viewdata_all_level { get; set; }
        public bool is_editdata_all_level { get; set; }
        public bool is_removedata_all_level { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
        public bool is_report_to_root { get; set; }
    }

    public class DeleteRoleRequest
    {
        public System.Guid role_delete_id { get; set; }
        public System.Guid role_transfer_id { get; set; }
    }
}
