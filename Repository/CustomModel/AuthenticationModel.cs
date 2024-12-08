using Common;
using Common.Commons;
using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.CustomModel
{
    public class RegisterRequest : BaseRegister
    {
        [Required]
        public string tenant_name { get; set; }
        public string province_id { get; set; } = "";
        public string address { get; set; }
        [Required]
        [Phone]
        public string phone { get; set; }
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Input valid email address!")]
        public string email { get; set; }
        public string business_type_id { get; set; } = "";
        public int num_employees { get; set; }
        [Required]
        public int license { get; set; }
        public bool is_other { get; set; }
        public string business_type_name { get; set; }
        public string customer_type { get; set; }
        public bool is_create_extension { get; set; }
        public string tenant_id_vgw { get; set; }
        [Required]
        public int file_saving_time { get; set; }
        public string type_saving_time { get; set; }
    }
    public class BaseRegister
    {
        public System.Guid id { get; set; }
        public string register_key { get; set; }
        [Required]
        [RegularExpression(@"^.*(?=.{8})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must be  least  8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string password { get; set; }
        public bool is_trial { get; set; }
        public DateTime? expire_time { get; set; }
        public bool is_active { get; set; }
        public System.DateTime create_time { get; set; }
        public string create_by { get; set; }
        public System.DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public void AddInfo()
        {
            DateTime currenttime = DateTime.Now;
            id = Guid.NewGuid();
            register_key = CommonFuncMain.GenerateCoupon();
            password = HashString.StringToHash(password, Constants.HASH_SHA512);
            is_active = true;
            create_time = currenttime;
            create_by = "Auto";
            modify_time = currenttime;
            modify_by = "Auto";
        }

    }
    public class ForgotPasswordRequest
    {
        [Required]
        public string email { get; set; }
    }
    public class ResetPasswordRequest
    {
        [Required]
        public string public_key { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Error compare password")]
        public string comparepassword { get; set; }

        public void UpdateInfo()
        {
            password = HashString.StringToHash(password, Constants.HASH_SHA512);
            comparepassword = HashString.StringToHash(comparepassword, Constants.HASH_SHA512);
        }
    }
    public class UpdatePasswordRequest
    {
        [Required]
        public string oldpassword { get; set; }
        [Required]
        [RegularExpression(@"^.*(?=.{8})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must be  least  8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Error compare password")]
        public string comparepassword { get; set; }
        public string code_verify { get; set; }

        public void UpdateInfo()
        {
            oldpassword = HashString.StringToHash(oldpassword, Constants.HASH_SHA512);
            password = HashString.StringToHash(password, Constants.HASH_SHA512);
            comparepassword = HashString.StringToHash(comparepassword, Constants.HASH_SHA512);
        }
    }
    public class ReceivePasswordModel
    {
        public string email { get; set; }
    }
    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool is_remember_me { get; set; }
    }
    public class VerifyCodeLoginRequest
    {
        [Required]
        public string email { get; set; }
        public string verify_code { get; set; }
    }

    public class TokenResquest {
        [Required]
        public string userName { get; set; }//email
        public string extension { get; set; }
        [Required]
        public string accessKey { get; set; }
    }
    public class TokenResponseCRM
    {
       
        public string email { get; set; }
        public string extension { get; set; }
        public string token { get; set; }
    }
    public class LoginResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Guid role_id { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool is_active { get; set; }
        public Guid tenant_id { get; set; }
        public Guid? asterisk_id { get; set; }
        public string token { get; set; }
        public string reason_deactive { get; set; }
        public string extension_number { get; set; }
        public Nullable<System.DateTime> block_time { get; set; }

        public bool? enable_verify { get; set; }
        public bool is_active_tenant { get; set; }
    }
 
    public class UpdateReasonDeactiveRequest
    {
        public string username { get; set; }
        public string reason_deactive { get; set; }
        public bool is_active { get; set; }
    }

    public class ResponChangePass
    {
        public bool? enable_verify { get; set; }
        public bool? status { get; set; }

    }
}
