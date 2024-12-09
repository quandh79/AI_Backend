using System;
using System.Collections.Generic;

namespace Management_AI.Models.Main
{
    public class ConnectionManager
    {
        public string username { get; set; } = string.Empty;
        public string extension { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public bool? is_not_ready { get; set; }
        public bool? is_system_set { get; set; }
        public string previous_state { get; set; } = string.Empty;
        public DateTime? first_login_time { get; set; }
        public string first_login_ip { get; set; }
        public DateTime? last_change_state_time { get; set; }
        public string connection_id { get; set; } = string.Empty;
        public DateTime create_time { get; set; }


        public string extension_number { get; set; } = string.Empty;
        public string state_detail { get; set; } = string.Empty;

        public List<string> connection_jtapi_ids { get; set; } = new List<string>();
    }

    public class CallManager
    {
        public string call_state { get; set; } = string.Empty;
        public string call_direct { get; set; } = string.Empty;
        public string exten { get; set; } = string.Empty;
        public string extenSup { get; set; } = string.Empty;
        public bool? isConference { get; set; } = false;
        public bool? isSendMessConference { get; set; } = false;
        public bool? isTransferIn { get; set; } = false;
        public string queue_name { get; set; } = string.Empty;
        public string hotline { get; set; } = string.Empty;

    }
    public class EventCall
    {
        public string jtapi_id { get; set; }
        public string event_name { get; set; }
        public string call_id { get; set; }
        public string call_from { get; set; }
        public string call_to { get; set; }
        public string call_medial { get; set; }

    }
}