using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.BCC01_EF
{
    public partial class BCC01_MapAgentGroup
    {
        public Guid id { get; set; }
        public Guid agentgroup_id { get; set; }
        public string username { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }

        [ForeignKey("agentgroup_id")]
        public virtual BCC01_AgentGroup BCC01_AgentGroup { get; set; }
        [ForeignKey("username")]
        public virtual BCC01_User BCC01_User { get; set; }
    }
}
