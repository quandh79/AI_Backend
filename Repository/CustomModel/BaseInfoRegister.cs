using System;

namespace Repository.CustomModel
{
    public class BaseInfoRegister
    {
        public BaseInfoRegister(string description, DateTime create_time, DateTime modify_time, string create_by, string modify_by, Guid tenant_id, string phone, string email, string password)
        {
            this.description = description;
            this.create_time = create_time;
            this.create_by = create_by;
            this.modify_time = modify_time;
            this.modify_by = modify_by;
            this.tenant_id = tenant_id;
            this.email = email;
            this.password = password;
            this.phone = phone;

        }
        public string description { get; set; } = "auto";
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public System.Guid tenant_id { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
