using System;

namespace Management_AI.Services.Models
{
    public class HotlineInfo
    {
        public Guid id { get; set; }
        public string hotline_name { get; set; }
        public string hotline_number { get; set; }
        public bool is_inbound { get; set; }
        public string destination_type { get; set; }
        public string destination_value { get; set; }
        public string description { get; set; }
        public bool is_outbound { get; set; }
        public bool is_outbound_all_agent { get; set; }
        public Guid outbound_agent_group_id { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}
