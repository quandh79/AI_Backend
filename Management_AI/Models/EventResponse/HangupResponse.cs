using System;

namespace Management_AI.Models.EventResponse
{
    public class HangupResponse
    {
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string extension_number { get; set; }
        public int cause { get; set; }
        public string cause_txt { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }

        public string call_type { get; set; }
    }
}
