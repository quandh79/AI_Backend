using System;

namespace Management_AI.Models.EventResponse
{
    public class QueueCallerJoinResponse
    {
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string channel { get; set; }
        public string queue { get; set; }
        public string context { get; set; }
        public Guid tenant_id { get; set; }
        public string voiceportal_host_ip { get; set; }
        public string voiceportal_username { get; set; }
        public string voiceportal_password { get; set; }
        public DateTime create_time { get; set; }
    }
}
