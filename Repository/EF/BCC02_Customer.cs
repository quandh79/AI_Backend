using System;
using System.Collections.Generic;
using Repository.EF;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_Customer
    {
        public BCC02_Customer()
        {
            BCC02_CallLog = new HashSet<BCC02_CallLog>();
            BCC02_CustomerExtension = new HashSet<BCC02_CustomerExtension>();
            BCC02_CustomerNote = new HashSet<BCC02_CustomerNote>();
            BCC02_Ticket = new HashSet<BCC02_Ticket>();
        }

        public Guid id { get; set; }
        public string customer_name { get; set; }
        public int? gender { get; set; }
        public DateTime? birth_date { get; set; }
        public string avatar { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string citizen_id { get; set; }
        public string channel_type { get; set; }
        public bool? is_block { get; set; }
        public DateTime? block_from { get; set; }
        public DateTime? block_to { get; set; }
        public bool? is_warning { get; set; }
        public bool? is_vip { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
        public Guid? organization_id { get; set; }

        public virtual ICollection<BCC02_CallLog> BCC02_CallLog { get; set; }
        public virtual ICollection<BCC02_CustomerExtension> BCC02_CustomerExtension { get; set; }
        public virtual ICollection<BCC02_CustomerNote> BCC02_CustomerNote { get; set; }
        public virtual ICollection<BCC02_Ticket> BCC02_Ticket { get; set; }
    }
}
