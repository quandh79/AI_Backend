using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC01_EF
{
    public partial class BCC01_AgentGroup
    {
        public Guid id { get; set; }
        public string agentgroup_name { get; set; }
        public string description { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}
