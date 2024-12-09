using System;

namespace Management_AI.Models.Main
{
    public class TeamModel : BaseModelSQL
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }

    }
    public class UserInTeamModel
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public Guid teamid { get; set; }
        public string email { get; set; }
        public Guid tenant_id { get; set; }
        public Guid role_id { get; set; }
    }
}
