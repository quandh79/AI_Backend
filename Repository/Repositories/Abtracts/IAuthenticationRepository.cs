using Common.Commons;
using Repository.CustomModel;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<ResponseService<TokenResponseCRM>> GetToken(TokenResquest request);
        Task<ResponseService<LoginResponse>> Login(LoginRequest request);
        Task<ResponseService<string>> Logout();
        Task<string> ReceiveForgotPassword(string register_key);
        Task<ResponseService<string>> ForgotPassword(ForgotPasswordRequest request);
        Task<ResponseService<string>> UpdateNewPassword(ResetPasswordRequest request);
    }
}