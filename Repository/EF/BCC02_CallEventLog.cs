using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_CallEventLog
    {
        public Guid id { get; set; }
        public string unique_id { get; set; }
        public string event_name { get; set; }
        public string call_status { get; set; }
        public string context { get; set; }
        public string data { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public string linked_id { get; set; }
    }
}
