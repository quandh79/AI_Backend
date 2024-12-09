using Repository.CustomModel;
using Repository.EF;
using System;
using System.Collections.Generic;

namespace Management_AI.Models.Main
{
    public class TenantsModel : BaseModelSQL
    {
        public TenantsModel()
        {
            AddInfo();
        }
        /// <summary>
        /// Tên của tenant đăng ký sử dụng
        /// </summary>
        /// <example>Phan Truong Phi</example>
        public string tenant_name { get; set; }
        /// <summary>
        /// mã tỉnh thành
        /// </summary>
        /// <example>74</example>
        public string province_id { get; set; }
        /// <summary>
        /// Địa Chỉ
        /// </summary>
        /// <example>Trieu Phong - Quang Tri</example>
        public string address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// <example>0858552245</example>
        public string phone { get; set; }
        /// <summary>
        /// mã tỉnh thành
        /// </summary>
        /// <example>74</example>
        public string email { get; set; }
        /// <summary>
        /// loại hình kinh doanh
        /// </summary>
        /// <example>Contact Center</example>
        public string business_type_id { get; set; }
        /// <summary>
        /// số lượng user sử dụng
        /// </summary>
        /// <example>74</example>
        public int num_employees { get; set; }
        /// <summary>
        /// số lượng license được cấp
        /// </summary>
        /// <example>74</example>
        public int license { get; set; }
        /// <summary>
        /// có phải là dùng thử hay không
        /// </summary>
        /// <example>false</example>
        public bool is_trial { get; set; }
        /// <summary>
        /// thời gian hết hạn dùng thử
        /// </summary>
        /// <example></example>
        public DateTime? expire_time { get; set; }
        /// <summary>
        /// đang kích hoạt hay không
        /// </summary>
        /// <example>true</example>
        public bool is_active { get; set; }
    }
    public class TenantResponse
    {
        public Guid id { get; set; }
        public string tenant_name { get; set; }
        public string province_name { get; set; }
        public string province_id { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string business_type_name { get; set; }
        public string business_type_id { get; set; }
        public int num_employees { get; set; }
        public int license { get; set; }
        public bool is_trial { get; set; }
        public string customer_type { get; set; }
        public DateTime? expire_time { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid? asterisk_id { get; set; }
        public int? prefix_extension { get; set; }
        public string role_id { get; set; }
        public string tenant_id_vgw { get; set; }
        public int file_saving_time { get; set; }
        public string type_saving_time { get; set; }
    }
    public class TenantSSOReponse : TenantResponse
    {
        public Guid? userid { get; set; }
    }
}
