using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC03_EF
{
    public class BCC03_EventCall
    {
        public Guid id { get; set; }
        public string jtapi_id { get; set; }
        public string event_name { get; set; }
        public string call_id { get; set; }
        public string call_from { get; set; }
        public string call_to { get; set; }
        public string call_medial { get; set; }
        public int? duration_hold { get; set; }
        public DateTime? create_time { get; set; }
        public int? call_stt { get; set; }
    }
}
