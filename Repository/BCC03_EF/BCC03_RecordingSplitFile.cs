using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC03_EF
{
    public partial class BCC03_RecordingSplitFile
    {
        public Guid id { get; set; }
        public Guid? recording_id { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public string path { get; set; }
    }
}
