﻿using Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC03_EF
{
    public class BCC03_CallLog
    {
        public Guid id { get; set; }
        public string unique_id { get; set; }
       // public Guid? customer_id { get; set; }
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
       // public string recording_url_file { get; set; }
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
        //public int? roundrobin_index { get; set; }
        //public string sip_call_id { get; set; }
        //public bool? is_selected { get; set; }
        //public string step_update { get; set; }
        //public string channel_webrtc { get; set; }
        //public Guid? callbackconfig_id { get; set; }
        //public Guid? callback_id { get; set; }
        //public string queue_name { get; set; }
        public DateTime? joinIvr_time { get; set; }
        public DateTime? endIvr_time { get; set; }
        public int? duration_ivr { get; set; }
        public DateTime? endqueue_time { get; set; }
        public int? duration_queue { get; set; }
        public string call_trace { get; set; }
        //public string button_key { get; set; }
        public bool? is_transfer_out { get; set; }
        public bool? is_transfer_in { get; set; }
        public bool? is_conference { get; set; }
        public Guid? transfer_source_id { get; set; }
        public Guid? transfer_to_id { get; set; }

        public DateTime? inbound_init_time { get; set; }
        public int? customer_type { get; set; }

        public bool? is_cucm_waiting_call { get; set; }
        public string ip_asterisk { get; set; }
       // public string autocall { get; set; }
       // public int? call_type { get; set; }
      //  public string campaign_task_id { get; set; }
       // public string extension_lcm { get; set; }
       // public string campaign_id { get; set; }
       // public string tenant_id_lcm { get; set; }
       // public string agent_task_id { get; set; }
       // public string context { get; set; }
        //public int? routing_type { get; set; }
       // public bool? is_auto_call { get; set; }

        public string hotline_number { get; set; }

       // public bool? is_forward_call { get; set; }
      //  public string phone_forward { get; set; }
        public string audio_path { get; set; }
        public string audio_id { get; set; }
        public string cti_port { get; set; }
        public string audio_type { get; set; }
        public bool? audio_upload { get; set; }
        public int? audio_job { get; set; }
        public string agent_name { get; set; }

        // trace event hold cho VTB
        public int? hold_duration { get; set; }
        public int? hold_count { get; set; }
        public DateTime? hold_start_time { get; set; }

        public string first_call_direct { get; set; }
        public string transfer_source_unique_id { get; set; }
        public string transfer_to_unique_id { get; set; }

        public string transfer_source_ext { get; set; }
        public string transfer_to_ext { get; set; }

    }
}