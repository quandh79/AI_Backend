using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class BCC02_VoucherNoSetting
    {
        public Guid id { get; set; }
        public string entity_type { get; set; }
        public string field_name { get; set; }
        public string prefix { get; set; }
        public int? number_lenght { get; set; }
        public int? value { get; set; }
        public DateTime? create_time { get; set; }
        public string create_by { get; set; }
        public DateTime? modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? tenant_id { get; set; }
    }
}
