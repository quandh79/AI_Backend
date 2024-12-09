using System;

namespace Management_AI.Models.EventResponse
{
    public class AttendedTransferResponse
    {
        public bool result { get; set; }
        public string transfer_target_connected_line_num { get; set; }
        public string transfer_target_caller_id_num { get; set; }
        public string transferee_caller_id_num { get; set; }
        public string orig_transferer_context { get; set; }
        public string orig_transferer_unique_id { get; set; }
        public string transferee_unique_id { get; set; }
        public Guid tenant_id { get; set; }
        public string username { get; set; }
        public string transferee { get; set; }
        public DateTime create_time { get; set; }
    }
}
