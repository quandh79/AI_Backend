using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_Queue
    {
        public int id { get; set; }
        public string queue_name { get; set; }
        public string description { get; set; }
        public string agent_order { get; set; }
        public Guid? agentgroup_id { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        public int sub_queue_id { get; set; }
    }
   
}
