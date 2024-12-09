using System;
using System.Collections.Generic;

namespace Management_AI.Models.Main
{
    public class AddMapTeamUserModel
    {
        public Guid teamId { get; set; }
        public string teamname { get; set; }
        public List<string> usernames { get; set; }
        public Guid? tenantId { get; set; }
    }
    public class DeleteMapTeamUserModel
    {
        public Guid teamId { get; set; }
        public string username { get; set; }
        public Guid? tenantId { get; set; }

    }
}
