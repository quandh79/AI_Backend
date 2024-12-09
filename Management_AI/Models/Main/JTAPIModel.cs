using System;

namespace Management_AI.Models.Main
{
    public class CallEventModel
    {
        public string CallFromNumber { get; set; }
        public string CallID { get; set; }
        public string Hotline { get; set; }
    }
    public class AnswerModel : CallEventModel
    {
        public string CallType { get; set; }
        public string CallToNumber { get; set; }
        public string Extension { get; set; }
    }
    public class OutboundNewCallModel : CallEventModel
    {
        public string CallToNumber { get; set; }
        public string Extension { get; set; }
        public bool? IsTranIn { get; set; }
    }
    public class InboundNewCallModel : CallEventModel
    {
        public string CallToNumber { get; set; }
        public string Extension { get; set; }
        public bool? IsTranIn { get; set; }
    }
    public class RingingModel : CallEventModel
    {
        public string CallToNumber { get; set; }
    }
    public class HoldCallModel : CallEventModel
    {
        public string CallType { get; set; }
        public string CallToNumber { get; set; }
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string connected_line_num { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }

    public class UnHoldCallModel : CallEventModel
    {
        public string CallType { get; set; }
        public string CallToNumber { get; set; }
        public string unique_id { get; set; }
        public string caller_id_num { get; set; }
        public string connected_line_num { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }


    public class HangupModel : CallEventModel
    {
        public string CallType { get; set; }
        public int? CauseCode { get; set; }
        public string CauseText { get; set; }
        public string CallToNumber { get; set; }
        public string Extension { get; set; }
    }

    //public class BlindTransferModel 
    //{
    //    public string phone_number { get; set; }
    //    public string extension { get; set; }
    //    public string call_source_id { get; set; }

    //}


    public class SetCallIsTransferDataEvent
    {
        public string call_id_source { get; set; }
        public string call_id_to { get; set; }
        public string extension_source { get; set; }
        public string extension_to { get; set; }

    }
    public class SetCallIsConferenceDataEvent
    {
        public string call_id { get; set; }
        public string extension { get; set; }
    }
}
