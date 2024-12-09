using AutoMapper;
using Common;
using Common.Commons;
using Management_AI.Common;
using Management_AI.Config;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository.CustomModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public readonly IAuthenticationRepository _authenticationRepository;
        public readonly IUserRepository _userRepository;
        public readonly ITenantsRepository _tenantsRepository;
        private IHttpContextAccessor _contextAccessor = ConfigContainerDJ.CreateInstance<IHttpContextAccessor>();
        public AuthenticationService(
            IAuthenticationRepository authenticationRepository,
            ILogger logger,
            IUserRepository userRepository,
            ITenantsRepository tenantsRepository,
            IMapper mapper) : base(logger, mapper)
        {
            _authenticationRepository = authenticationRepository;
            _userRepository = userRepository;
            _tenantsRepository = tenantsRepository;
        }
        public async Task<ResponseService<TokenResponseCRM>> GetToken(TokenResquest request)
        {
            // throw new NotImplementedException();
            if (request.accessKey != ConfigHelper.Get("API_SECRET_KEY"))
            {
                return new ResponseService<TokenResponseCRM>(Constants.SECRET_KEY_ERROR);
            }
            var result = await _authenticationRepository.GetToken(request);
            return result;
        }
        public virtual async Task<ResponseService<string>> UpdateNewPassword(ResetPasswordRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _authenticationRepository.UpdateNewPassword(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<string>(ex);
            }
        }
        public virtual async Task<ResponseService<LoginResponse>> Login(LoginRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _authenticationRepository.Login(request);

                if (result.data != null)
                {
                    //check is active
                    if (result.data.is_active == true && result.data.is_active_tenant == true)
                    {
                        //Check block time
                        if (result.data.block_time == null || result.data.block_time <= DateTime.Now)
                        {
                            SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, result.data.tenant_id.ToString());
                            SessionStore.Set(Constants.KEY_SESSION_USER_ID, result.data.email);
                            SessionStore.Set(Constants.KEY_SESSION_ASTERISK_ID, "");
                            SessionStore.Set(Constants.KEY_SESSION_TOKEN, result.data.token);
                            SessionStore.Set(Constants.KEY_SESSION_IS_ADMIN, result.data.is_administrator.ToString());
                            SessionStore.Set(Constants.KEY_SESSION_EXTENSION_NUMBER, "");
                            return new ResponseService<LoginResponse>(result.data);
                        }
                        else
                        {
                            int error_code = 0;
                            string error_mess = "";
                            switch (result.data.reason_deactive)
                            {
                                case "long_time_not_change_password":
                                    error_code = Constants.INT_LONG_TIME_NOT_CHANGE_PASSWORD;
                                    error_mess = Constants.LONG_TIME_NOT_CHANGE_PASSWORD;
                                    break;
                                case "block_by_input_wrong_pass":
                                    error_code = Constants.INT_BLOCK_BY_INPUT_WRONG_PASS;
                                    error_mess = Constants.BLOCK_BY_INPUT_WRONG_PASS;
                                    break;
                            }
                            return new ResponseService<LoginResponse>(error_mess).BadRequest(error_code);
                        }
                    }
                    else
                    {
                        return new ResponseService<LoginResponse>(Constants.ACCOUNT_LOCKED).BadRequest(MessCodes.ACCOUNT_LOCKED);
                    }
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<LoginResponse>(ex);
            }
        }
        public virtual async Task<ResponseService<string>> Logout()
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _authenticationRepository.Logout();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<string>(ex);
            }
        }
        public virtual async Task<ResponseService<string>> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _authenticationRepository.ForgotPassword(request);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<string>(ex);
            }
        }
        public virtual async Task<ResponseService<string>> ReceiveForgotPassword(string register_key)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _authenticationRepository.ReceiveForgotPassword(register_key);
                if (string.IsNullOrEmpty(result))
                    return new ResponseService<string>(result);
                return new ResponseService<string>(true, "", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<string>(ex);
            }
        }
        public ResponseService<TokenResponse> CheckAuthentication()
        {
            try
            {
                string beartoken = _contextAccessor.HttpContext.Request.Headers["authorization"];
                if (beartoken != null)
                {
                    TokenResponse tokenRes = new TokenResponse();
                    var token = beartoken.Split(' ').Last();
                    var principal = CommonFunc.GetPrincipalFromToken(token);
                    if (principal.claimsPrincipal != null)
                    {
                        switch (principal.signatureAlgorithm)
                        {
                            case "RS256":
                                var clamModel = new ClamModel();
                                var strValue = principal.claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == Constants.BASESR_CLAIMS)?.Value ?? "";
                                clamModel = JsonConvert.DeserializeObject<ClamModel>(strValue);
                                if (clamModel != null)
                                {
                                    tokenRes.username = clamModel.user_name;
                                    tokenRes.tenant_id = clamModel.tenant_id;
                                    tokenRes.asterisk_id = clamModel.asterisk_id;
                                    tokenRes.extension_number = clamModel.extension_number;

                                    tokenRes.is_administrator = bool.Parse(string.IsNullOrEmpty(clamModel.is_administrator) ? "false" : clamModel.is_administrator);
                                    tokenRes.is_rootuser = bool.Parse(string.IsNullOrEmpty(clamModel.is_rootuser) ? "false" : clamModel.is_rootuser);
                                }

                                //tokenRes.is_rootuser = bool.Parse((clamModel?.is_rootuser)==null?"false");
                                break;
                            case "HS256":

                                ClaimsIdentity identity = null;
                                identity = (ClaimsIdentity)principal.claimsPrincipal.Identity;
                                tokenRes.username = principal.claimsPrincipal.Claims.First(claim => claim.Type == "username").Value;
                                tokenRes.tenant_id = principal.claimsPrincipal.Claims.First(claim => claim.Type == "tenant_id").Value;
                                tokenRes.asterisk_id = principal.claimsPrincipal.Claims.First(claim => claim.Type == "asterisk_id").Value;
                                tokenRes.is_administrator = bool.Parse(principal.claimsPrincipal.Claims.First(claim => claim.Type == "is_administrator").Value);
                                tokenRes.is_rootuser = bool.Parse(principal.claimsPrincipal.Claims.First(claim => claim.Type == "is_rootuser").Value);
                                tokenRes.extension_number = principal.claimsPrincipal.Claims.First(claim => claim.Type == "extension_number").Value;
                                break;
                        }

                        return new ResponseService<TokenResponse>(tokenRes);
                    }
                }
                return new ResponseService<TokenResponse>(false, "Authen Fail", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<TokenResponse>(ex);
            }
        }
        public async Task<ResponseService<bool>> LogStatusIsLogin(string email, bool islogin)
        {

            var user = await _userRepository.GetSingle(x => x.email == email);
            if (user != null)
            {
                if (!islogin)
                {
                    _contextAccessor.HttpContext.Session.Clear();
                }

                return new ResponseService<bool>(true);
            }
            return new ResponseService<bool>(true);
        }

        public async Task<ResponseService<LoginResponse>> VerifyCodeLogin(VerifyCodeLoginRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var token = await _authenticationRepository.GetToken(new TokenResquest()
                {
                    userName = request.email,
                    accessKey = ConfigManager.Get(Constants.CONF_API_SECRET_KEY)
                });

                var user = await _userRepository.GetSingle(x => x.username == request.email);
                if (user == null)
                {
                    return new ResponseService<LoginResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                if (request.verify_code != user.code_verify || user.time_expired_code_verify < DateTime.Now)
                {
                    return new ResponseService<LoginResponse>(null, Constants.TWO_FA_CONFIG_CODE_VERIFY_NOT_FOUND, MessCodes.VERIFY_FAILED);
                }

                var tenant_by_user = await _tenantsRepository.GetSingle(x => x.id == user.tenant_id);
                if (tenant_by_user == null)
                {
                    return new ResponseService<LoginResponse>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                LoginResponse data = new LoginResponse();
                data = CommonFuncMain.ToObject<LoginResponse>(user);
                data.token = token.data.token;
                data.asterisk_id = tenant_by_user?.id;
                data.enable_verify = false;

                SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, data.tenant_id.ToString());
                SessionStore.Set(Constants.KEY_SESSION_USER_ID, data.email);
                SessionStore.Set(Constants.KEY_SESSION_ASTERISK_ID, data.asterisk_id?.ToString() ?? "");
                SessionStore.Set(Constants.KEY_SESSION_TOKEN, data.token);
                SessionStore.Set(Constants.KEY_SESSION_IS_ADMIN, data.is_administrator.ToString());
                SessionStore.Set(Constants.KEY_SESSION_EXTENSION_NUMBER, data.extension_number.ToString());

                user.code_verify = null;
                user.time_expired_code_verify = null;
                user.last_time_2fa = DateTime.Now;
                await _userRepository.Update(user, user.username);

                return new ResponseService<LoginResponse>(data);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<LoginResponse>(ex);
            }
        }
    }
}
