using System;

namespace Management_AI.Models.EventResponse
{
    public class NewstateResponse
    {
        public string unique_id { get; set; }
        public string unique_id_origin { get; set; }
        public string exten { get; set; }
        public string caller_id_num { get; set; }
        //public string connected_line_name { get; set; }
        public string connected_line_num { get; set; }
        public string channel { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid? hotline_id { get; set; }
        public string hotline_number { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }

        public string connected_line_name { get; set; }

        public string extension_number { get; set; }
        public string dest_caller_id_num { get; set; }
        public bool? is_tran_in { get; set; }
    }
}
