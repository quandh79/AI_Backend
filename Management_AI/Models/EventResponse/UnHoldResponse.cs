using System;

namespace Management_AI.Models.EventResponse
{
    public class UnHoldResponse
    {
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string connected_line_num { get; set; }
        public string context { get; set; }
        public Guid tenant_id { get; set; }
        public string username { get; set; }
        public DateTime create_time { get; set; }
    }
}
