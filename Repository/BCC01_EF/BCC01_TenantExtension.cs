using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC01_EF
{
    public partial class BCC01_TenantExtension
    {
        public Guid Id { get; set; }
        public int? prefix_extension { get; set; }
        public string max_extension { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime? update_time { get; set; }
        public Guid tenant_id { get; set; }
        public string username_jtapi { get; set; }
        public string password_jtapi { get; set; }

    }
}
