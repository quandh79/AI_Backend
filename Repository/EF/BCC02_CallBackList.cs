using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_CallBackList
    {
        public Guid id { get; set; }
        public Guid? callbackconfig_id { get; set; }
        public string phone_number { get; set; }
        public int? queue_id { get; set; }
        public string contact_name { get; set; }
        public string assign_to { get; set; }
        public bool? is_called { get; set; }
        public int? calltimes { get; set; }
        public DateTime? last_calltime { get; set; }
        public bool? is_close { get; set; }
        public string note { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
    }
}
