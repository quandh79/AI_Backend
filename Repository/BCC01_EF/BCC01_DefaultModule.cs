using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_DefaultModule
    {
        public Guid id { get; set; }
        public string module_name { get; set; }
        public string display_name { get; set; }
        public int position { get; set; }
        public bool is_active { get; set; }
        public string description { get; set; } 
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
    }
}
