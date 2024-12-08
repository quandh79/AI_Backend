using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_MapAgentSkill
    {
        public Guid id { get; set; }
        public Guid skill_id { get; set; }
        public string skill_code { get; set; }
        public string username { get; set; }
        public string skill_type { get; set; }
        public int skill_level { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}
