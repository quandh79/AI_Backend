using System;

namespace Management_AI.Models.Main
{
    public class InternalCallModel
    {
    }

    public class InternalInitCallModel
    {
        public string caller_id_num { get; set; }
        public string extension_number { get; set; }
        public string dest_caller_id_num { get; set; }
        public string unique_id { get; set; }
        public string context { get; set; }
        public string username { get; set; }
        public Guid tenant_id { get; set; }
        public DateTime create_time { get; set; }
    }
}
