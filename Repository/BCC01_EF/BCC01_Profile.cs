using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_Profile
    {
        public BCC01_Profile()
        {
            BCC01_MapProfileUser = new HashSet<BCC01_MapProfileUser>();
            BCC01_Permission = new HashSet<BCC01_Permission>();
        }

        public Guid id { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }

        [ForeignKey("id")]
        public virtual ICollection<BCC01_MapProfileUser> BCC01_MapProfileUser { get; set; }
        [ForeignKey("id")]
        public virtual ICollection<BCC01_Permission> BCC01_Permission { get; set; }
    }
}
