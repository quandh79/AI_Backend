using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_ForgotPassword
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string register_key { get; set; }
        public string public_key { get; set; }
        public DateTime create_time { get; set; }
    }
}
