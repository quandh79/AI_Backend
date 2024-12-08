using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_Tenants
    {
        public Guid id { get; set; }
        public string tenant_name { get; set; }
        public string province_id { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string business_type_id { get; set; }
        public int num_employees { get; set; }
        public int license { get; set; }
        public bool is_trial { get; set; }
        public DateTime? expire_time { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public string customer_type { get; set; }
        public Guid? asterisk_id { get; set; }
        public string tenant_id_vgw { get; set; }
        public int? file_saving_time { get; set; }
        public string type_saving_time { get; set; }

        //public virtual ICollection<BCC01_User> BCC01_User { get; set; }
    }
    public class BCC01_TenantsResponse : BCC01_Tenants
    {
        public int prefixExtension { get; set; }
        public bool hasExtensionOfUserAdmin { get; set; }
        public string extensionOfUserAdmin { get; set; }
        public string role_id { get; set; }
    }
}
