using System;

namespace Management_AI.Services.Models
{
    public class QueueInfo
    {
        public int id { get; set; }
        public string queue_name { get; set; }
        public string description { get; set; }
        public string agent_order { get; set; }
        public Guid? agentgroup_id { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        public int sub_queue_id { get; set; }
        public int max_wait_time { get; set; }
    }
}
