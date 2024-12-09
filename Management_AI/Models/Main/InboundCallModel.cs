using System;

namespace Management_AI.Models.Main
{
    public class InboundCallModel
    {
    }

    public class InboundInitCallModel
    {
        public string unique_id { get; set; }
        public string unique_id_origin { get; set; }
        public string exten { get; set; }
        public string caller_id_num { get; set; }
        public string connected_line_name { get; set; }
        public string channel { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid? hotline_id { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }

        public string extension_number { get; set; }
        public string dest_caller_id_num { get; set; }
        public bool? is_tran_in { get; set; }
        public string hotline_number { get; set; }
    }

    public class InboundDTMFModel
    {
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string channel { get; set; }
        public string digit { get; set; }
        public string context { get; set; }
        public DateTime create_time { get; set; }
    }

    public class InboundJoinQueueCallModel
    {
        public string unique_id { get; set; }
        public int queue_id { get; set; }
        public int queue_component_id { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
        public string call_trace { get; set; }
    }

    public class InboundRingingCallModel
    {
        public string unique_id { get; set; }
        //public string unique_id_origin { get; set; }
        public string exten { get; set; }
        public string caller_id_num { get; set; }
        //public string connected_line_name { get; set; }
        public string connected_line_num { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }

    public class NewCallInboundModel
    {
        public string DialedNumber { get; set; }
        public string SIPChanel { get; set; }
        public string ANI { get; set; }
        public string Uniqueid { get; set; }
        public string Context { get; set; }
        public Guid CompanyUID { get; set; }
        public string IPAsterisk { get; set; }
        public string TypeCus { get; set; }
        public DateTime CreateTime { get; set; }
    }


    public class InboundInsertInIVRStatusModel
    {
        public string DialedNumber { get; set; }
        public string SIPChanel { get; set; }
        public string ANI { get; set; }
        public string Uniqueid { get; set; }
        public string Context { get; set; }
        public Guid CompanyUID { get; set; }
        public string IPAsterisk { get; set; }
        public string TypeCus { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class InboundInsertEndCallStatusModel
    {
        public string Uniqueid { get; set; }
        public string CallTrace { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CompanyUID { get; set; }
    }
}
