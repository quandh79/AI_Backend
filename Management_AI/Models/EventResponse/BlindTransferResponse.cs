using System;

namespace Management_AI.Models.EventResponse
{
    public class BlindTransferResponse
    {
        public string result { get; set; }
        public string transferer_caller_id_num { get; set; }
        public string transferee_caller_id_num { get; set; }
        public string extension { get; set; }
        public string context { get; set; }
        public string transferer_unique_id { get; set; }
        public string transferee_unique_id { get; set; }
        public string username { get; set; }
        public string transferee { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }

    //public class TransferCallJTAPIModel
    //{
    //    public string extension { get; set; }
    //    public string phone_number { get; set; }
    //    public string transferee { get; set; }

    //    public string context { get; set; }
    //    public string username { get; set; }
    //    public string call_source_id { get; set; }
    //    public Guid tenant_id { get; set; }
    //    public DateTime create_time { get; set; }
    //}


    public class SetCallIsTransferModel
    {
        public string username { get; set; }

        public string call_id_source { get; set; }
        public string call_id_to { get; set; }
        public string extension_source { get; set; }
        public string extension_to { get; set; }

        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }

    public class SetCallIsConferenceModel
    {
        public string username { get; set; }

        public string call_id { get; set; }
        public string extension { get; set; }

        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }

}
