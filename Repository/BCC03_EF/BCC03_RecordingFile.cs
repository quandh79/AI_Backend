using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC03_EF
{
    public partial class BCC03_RecordingFile
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
        public string lst_path { get; set; }
        public int? duration_hold { get; set; }
        public int? count_hold { get; set; }
        public string call_from { get; set; }
        public string call_to { get; set; }
        public string transfer_source { get; set; }
        public string transfer_to { get; set; }
        public string conference_uniqueId { get; set; }
        public Guid? transferLog_id { get; set; }
    }
}
