using System;

namespace Management_AI.Services.Models
{
    public class CallLogInfo
    {
        public Guid id { get; set; }
        public string unique_id { get; set; }
        public Guid? customer_id { get; set; }
        public int? queue_id { get; set; }
        public string queue_code { get; set; }
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
        public string Management_AI_url_file { get; set; }
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
        public bool? is_auto_call { get; set; }
        public string campaign_task_id { get; set; }
        public string extension_lcm { get; set; }
        public string campaign_id { get; set; }
    }
    public class ParamGetCallLogByExtensionModel
    {
        public string extension_number { get; set; }
        public Guid tenant_id { get; set; }
    }
    public partial class CallLog
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
        public string Management_AI_url_file { get; set; }
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
        public string unique_id_asterisk { get; set; }
        public int? roundrobin_index { get; set; }
        public string sip_call_id { get; set; }
        public string queue_name { get; set; }
        public DateTime? joinIvr_time { get; set; }
        public DateTime? endIvr_time { get; set; }
        public int? duration_ivr { get; set; }
        public DateTime? endqueue_time { get; set; }
        public int? duration_queue { get; set; }
        public string call_trace { get; set; }
        public string button_key { get; set; }
        // public virtual BCC02_Customer customer_ { get; set; }

        public bool? is_transfer_out { get; set; }
        public bool? is_transfer_in { get; set; }
        public bool? is_conference { get; set; }
        public Guid? transfer_source_id { get; set; }
        public Guid? transfer_to_id { get; set; }

        public DateTime? inbound_init_time { get; set; }
        public bool? is_cucm_waiting_call { get; set; }
        public string ip_asterisk { get; set; }
        public string hotline_number { get; set; }

        public bool? is_auto_call { get; set; }
        public string campaign_task_id { get; set; }
        public string extension_lcm { get; set; }
        public string campaign_id { get; set; }
    }
    public class AutoCallResponse
    {
        public Guid id { get; set; }
        public string unique_id { get; set; }
        public string unique_id_asterisk { get; set; }
        public string extension_number { get; set; }
        public bool? is_auto_call { get; set; }
        public string campaign_task_id { get; set; }
        public string extension_lcm { get; set; }
        public string campaign_id { get; set; }
        public string phone_number { get; set; }

    }
}
