using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class ZCheckCallLog
    {
        public Guid id { get; set; }
        public string unique_id { get; set; }
        public Guid? customer_id { get; set; }
        public int? queue_id { get; set; }
        public int? queue_component_id { get; set; }
        public Guid? hotline_id { get; set; }
        public string extension_number { get; set; }
        public string call_direct { get; set; }
        public string call_status { get; set; }
        public string terminate_code { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? joinqueue_time { get; set; }
        public DateTime? connect_time { get; set; }
        public DateTime? end_time { get; set; }
        public int? ring_duration { get; set; }
        public int? talk_duration { get; set; }
        public int? total_duration { get; set; }
        public bool? is_ivr { get; set; }
        public bool? is_joinqueue { get; set; }
        public string recording_url_file { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public DateTime? ringing_time { get; set; }
        public bool? is_waitingcall { get; set; }
        public string bridge_unique_id { get; set; }
        public string transfer_unique_id { get; set; }
        public string phone_number { get; set; }
        public string unique_id_origin { get; set; }
        public string channel { get; set; }
        public int? roundrobin_index { get; set; }
        public string sip_call_id { get; set; }
        public bool? is_selected { get; set; }
        public DateTime? write_time { get; set; }
        public string step_update { get; set; }
    }
}
