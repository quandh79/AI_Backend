using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_MapQueueAgent
    {
        public Guid id { get; set; }
        public Guid? queuestep_id { get; set; }
        public int? queuestep_position { get; set; }
        public string username { get; set; }
        public double? score { get; set; }
        public string state { get; set; }
        public Guid? tenant_id { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public string extension_number { get; set; }
        public int? queue_id { get; set; }
        public int? roundrobin_index { get; set; }
    }
}
