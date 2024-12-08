using System;

namespace Repository.CustomModel
{
    public class PermissionModel
    {
        public System.Guid id { get; set; }
        public System.Guid profile_id { get; set; }
        public System.Guid permissionobject_id { get; set; }
        public bool is_show { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        public string object_name { get; set; }
        public string module_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
    }
    public class PermissionResShort
    {
        public System.Guid id { get; set; }
        public bool is_show { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        public string object_name { get; set; }
        public string module_name { get; set; }
        public bool is_active { get; set; }
    }
    public class PermissionAObjectRequest
    {
        public string username { get; set; }
        public Guid permissionobject_id { get; set; }
    }
    public class PermissionAObjectByTypeRequest
    {
        public string username { get; set; }
        public Guid permissionobject_id { get; set; }
        public string permission_type { get; set; }
    }
    public class GetPermissionByTypeAndName
    {
        public string username { get; set; }
        public string permission_name { get; set; }
        public string permission_type { get; set; }
    }
}
