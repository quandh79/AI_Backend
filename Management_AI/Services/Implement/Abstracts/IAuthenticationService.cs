using Common.Commons;
using Management_AI.Models.Main;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IAuthenticationService
    {
        Task<ResponseService<TokenResponseCRM>> GetToken(TokenResquest request);
        Task<ResponseService<string>> UpdateNewPassword(ResetPasswordRequest request);
        Task<ResponseService<LoginResponse>> Login(LoginRequest request);
        Task<ResponseService<string>> Logout();
        Task<ResponseService<string>> ForgotPassword(ForgotPasswordRequest request);
        Task<ResponseService<string>> ReceiveForgotPassword(string register_key);
        ResponseService<TokenResponse> CheckAuthentication();
        Task<ResponseService<bool>> LogStatusIsLogin(string email, bool isLogin);
        Task<ResponseService<LoginResponse>> VerifyCodeLogin(VerifyCodeLoginRequest request);
    }
}
