using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_AgentStateLog
    {
        public Guid id { get; set; }
        public string extension_number { get; set; }
        public Guid? state_id { get; set; }
        public string state_name { get; set; }
        public int? duration { get; set; }
        public bool? is_system_set { get; set; }
        public bool? is_notready { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public string state_key { get; set; }
        public string set_by { get; set; }
    }
}
