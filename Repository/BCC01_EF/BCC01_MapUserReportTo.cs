using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.BCC01_EF
{
    public class BCC01_MapUserReportTo
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string username_sup { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
        public string update_by { get; set; }
        public DateTime? update_time { get; set; }
        public Guid tenant_id { get; set; }
    }
}
