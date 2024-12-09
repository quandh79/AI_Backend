using Common;
using Common.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Management_AI.Common;
using Management_AI.Config;
using Management_AI.CustomAttributes;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Management_AI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> GetToken([FromBody] TokenResquest request)
        {
            ResponseService<TokenResponseCRM> response = await _authenticationService.GetToken(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<TokenResponseCRM>().Error(response);
            }
        }
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {           
            ResponseService<string> response = await _authenticationService.ForgotPassword(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<string>().Error(response);
            }
        }
        [HttpGet]
        [Route("receive-forgot-password")]
        public async Task<IActionResult> ReceiveForgotPassword([FromQuery] string key)
        {
            ResponseService<string> response = await _authenticationService.ReceiveForgotPassword(key);
            if (response.status)
            {
                var res = new HttpResponseMessage(HttpStatusCode.Moved);
                res.Headers.Location = new Uri($"{ConfigManager.Get(Constants.CONF_LINK_FORGOT_PASSWORD_IC)}?public_key={response.data}");
                return StatusCode(StatusCodes.Status301MovedPermanently);
            }
            else
            {
                ResponseService<String> res = new ResponseService<string>("Key invalid !!");
                res.BadRequest(MessCodes.KEY_INVALID);
                return BadRequest(res);
            }
        }
        [HttpPost]
        [Route("update-password")]
        public async Task<IActionResult> UpdateNewPassword([FromBody] ResetPasswordRequest request)
        {            
            ResponseService<string> response = await _authenticationService.UpdateNewPassword(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<string>().Error(response);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            ResponseService<LoginResponse> response = await _authenticationService.Login(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<LoginResponse>().Error(response);
            }
        }
        [HttpPost]
        [Authorized]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            ResponseService<string> response = await _authenticationService.Logout();
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<string>().Error(response);
            }
        }
        [HttpPost]
        [Route("check-authentication")]
        public IActionResult CheckAuthentication()
        {
            ResponseService<TokenResponse> response = _authenticationService.CheckAuthentication();
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(response);
            }
        }
        [HttpGet]
        [Route("log-status-login-logout")]
        [Authorized]
        public async Task<IActionResult> LogStatusLoginLogout([FromQuery] string Email, bool isLogin)
        {
            ResponseService<bool> response = await _authenticationService.LogStatusIsLogin(Email, isLogin);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<bool>().Error(response);
            }
        }
        [HttpPost]
        [Route("verify-code-login")]
        public async Task<IActionResult> VerifyCodeLogin([FromBody] VerifyCodeLoginRequest request)
        {
            ResponseService<LoginResponse> response = await _authenticationService.VerifyCodeLogin(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<LoginResponse>().Error(response);
            }
        }
    }
}
