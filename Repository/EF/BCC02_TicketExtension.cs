using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_TicketExtension
    {
        public Guid id { get; set; }
        public Guid? ticket_id { get; set; }
        public string field_id { get; set; }
        public string value { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }

        public virtual BCC02_Ticket ticket_ { get; set; }
    }
}
