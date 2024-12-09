using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management_AI.Models.Request
{
    public class MetadataRequest
    {
        public string call_id { get; set; }
        public string phone_number { get; set; }
        public string extension_number { get; set; }
        public string agent_name { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string connect_time { get; set; }
        public string total_duration { get; set; }
        public string call_status { get; set; }
        public string call_direct { get; set; }
    }
}
