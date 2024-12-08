using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_Ticket
    {
        public BCC02_Ticket()
        {
            BCC02_TicketExtension = new HashSet<BCC02_TicketExtension>();
            BCC02_TicketNote = new HashSet<BCC02_TicketNote>();
        }

        public Guid id { get; set; }
        public string ticket_no { get; set; }
        public string title { get; set; }
        public string channel_type { get; set; }
        public string unique_id { get; set; }
        public Guid? customer_id { get; set; }
        public string content { get; set; }
        public string status { get; set; }
        public string assign_to { get; set; }
        public string priority { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }

        public virtual BCC02_Customer customer_ { get; set; }
        public virtual ICollection<BCC02_TicketExtension> BCC02_TicketExtension { get; set; }
        public virtual ICollection<BCC02_TicketNote> BCC02_TicketNote { get; set; }
    }
}
