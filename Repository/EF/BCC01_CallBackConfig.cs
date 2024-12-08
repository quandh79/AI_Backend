using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC01_CallBackConfig
    {
        public Guid id { get; set; }
        public int? queue_id { get; set; }
        public int? condition_calltimes { get; set; }
        public string condition_time_frame { get; set; }
        public int? condition_time_duration { get; set; }
        public DateTime? condition_starttime { get; set; }
        public DateTime? condition_endtime { get; set; }
        public string distribution_type { get; set; }
        public Guid? distribution_agentgroup_id { get; set; }
        public bool? is_callandclose { get; set; }
        public bool? is_allowclose { get; set; }
        public bool? is_active { get; set; }
        public string description { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
    }
}
