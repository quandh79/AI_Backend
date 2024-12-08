using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC03_EF
{
    public partial class BCC03_CallTransferLog
    {
        public Guid id { get; set; }

        public string first_call_direct { get; set; }

        public string transfer_source_unique_id { get; set; }
        public string transfer_to_unique_id { get; set; }
        public string transfer_source_ext { get; set; }
        public string transfer_to_ext { get; set; }

        public Guid? transfer_source_id { get; set; }
        public Guid? transfer_to_id { get; set; }

        public bool? is_transfer_out { get; set; }
        public bool? is_transfer_in { get; set; }

        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public bool? is_conference { get; set; }
    }
}
