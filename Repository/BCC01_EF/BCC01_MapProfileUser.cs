using Repository.BCC01_EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_MapProfileUser
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public Guid profile_id { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }

        [ForeignKey("profile_id")]
        public virtual BCC01_Profile BCC01_Profile { get; set; }
        [ForeignKey("username")]
        public virtual BCC01_User BCC01_User { get; set; }
    }
}
