namespace Management_AI.Models.Main
{
    public class ProfileResponse
    {
        public Guid id { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
    public class ProfileRequest : BaseModelSQL
    {
        public string profile_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
    }
}
