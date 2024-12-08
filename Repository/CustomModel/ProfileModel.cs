namespace Repository.CustomModel
{
    public class ProfileModel
    {
        public System.Guid id { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
    }
    public class DeleteUserInProfile
    {
        public string username { get; set; }
        public System.Guid profile_id { get; set; }
    }
}
