using System;

namespace Repository.CustomModel
{
    public class TenantCustomResponseModel
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
        public string customer_type { get; set; }
        public Guid? asterisk_id { get; set; }
        public string extension_number { get; set; }
        public string extension_password { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }

        public string role_id { get; set; }
        public string tenant_id_vgw { get; set; }
        public int? file_saving_time { get; set; }
        public string type_saving_time { get; set; }
    }

    public class GetAsteriskIsAvailableResponse
    {
        public Guid asterisk_id { get; set; }
        public string message_error { get; set; }
    }

    public class TenantAddAsteriskRequest
    {
        public Guid id { get; set; }
        public string tenant_name { get; set; }
        public string extension_number { get; set; }
        public string extension_password { get; set; }
    }
}
