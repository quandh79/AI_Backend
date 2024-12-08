using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_RecordingLog
    {
        public Guid id { get; set; }
        public Guid? call_log_id { get; set; }
        public string unique_id { get; set; }

        public string extension_number { get; set; }
        public string phone_number { get; set; }
        public string full_filename { get; set; }
        public string filename { get; set; }
        public string file_format { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public int? duration { get; set; }
        public string call_type { get; set; }
        public string agent_name { get; set; }
        public Guid? customer_id { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public string errormessage { get; set; }
    }
}
