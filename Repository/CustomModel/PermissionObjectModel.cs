namespace Repository.CustomModel
{
    public class PermissionObjectCustom
    {
        public System.Guid id { get; set; }
        public string object_name { get; set; }
        public System.Guid module_id { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
        public string module_name { get; set; }
    }
    public class PermissionObjectModel
    {
        public System.Guid id { get; set; }
        public string object_name { get; set; }
        public System.Guid module_id { get; set; }
        public string description { get; set; }
    }
}
