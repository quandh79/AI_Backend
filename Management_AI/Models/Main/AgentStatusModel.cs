using System;

namespace Management_AI.Models.Main
{
    public class AgentStatusModel
    {
        public string extension_number { get; set; }
        public string state { get; set; }
        public bool is_system_set { get; set; }
        public bool is_not_ready { get; set; }
        public string username { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
        public bool? flag { get; set; } // true: không gửi cho chính user
        public bool? is_sup_set { get; set; }


        public Guid? state_id { get; set; }
        public string state_name { get; set; }
        public string state_key { get; set; }

        public string avatar { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string agentgroup_id { get; set; }

    }
}
