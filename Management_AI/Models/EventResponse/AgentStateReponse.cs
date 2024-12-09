using System;

namespace Management_AI.Models.EventResponse
{
    public class AgentStateReponse
    {
        public string extension_number { get; set; }
        public Guid state_id { get; set; }
        public string state_name { get; set; }
        public string state_key { get; set; }
        public bool? flag { get; set; } // true: không gửi cho chính user
    }
}
