using System;

namespace Management_AI.Models.EventResponse
{
    public class DTMFResponse
    {
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string channel { get; set; }
        public string context { get; set; }
        public string digit { get; set; }
        public DateTime create_time { get; set; }
    }
}
