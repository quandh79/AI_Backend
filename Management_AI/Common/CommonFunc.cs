using Common;
using Common.Commons;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Management_AI.Models.Common;
using Management_AI.Models.Main;
using Management_AI.Config;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Common
{
    public class CommonFunc
    {
        private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static bool ValidateToken(string token)
        {
            try
            {
                // AuthenAPI _authenAPI = new AuthenAPI(token);
                _logger = new Logger();
                ResponseService<TokenResponse> response = CheckAuthentication(token);
                if (response.status)
                {
                    SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, response.data.tenant_id);
                    SessionStore.Set(Constants.KEY_SESSION_EMAIL, response.data.username);
                    SessionStore.Set(Constants.KEY_SESSION_USER_ID, response.data.username);
                    SessionStore.Set(Constants.KEY_SESSION_EXTENSION_NUMBER, response.data.extension_number);
                    SessionStore.Set(Constants.KEY_SESSION_TOKEN, token);
                    SessionStore.Set(Constants.KEY_SESSION_IS_ADMIN, response.data.is_administrator);
                    SessionStore.Set(Constants.KEY_SESSION_IS_ROOT, response.data.is_rootuser);

                    return true;
                }
                else
                {
                    _logger.LogError(response.message);
                    return false;
                }
                //BaseResponse<InfoAuthenModel> response = await _authenAPI.ValidateToken();
                //if (response.success)
                //{
                //    SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, response.data.tenant_id);
                //    SessionStore.Set(Constants.KEY_SESSION_EMAIL, response.data.username);
                //    SessionStore.Set(Constants.KEY_SESSION_TOKEN, token);

                //    return true;
                //}
                //else
                //{
                //    _logger.LogError(response.mess);
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }
        public static ResponseService<TokenResponse> CheckAuthentication(string beartoken)
        {
            try
            {
                //string beartoken = _contextAccessor.HttpContext.Request.Headers["authorization"];
                if (beartoken != null)
                {
                    TokenResponse tokenRes = new TokenResponse();
                    var token = beartoken.Split(' ').Last();
                    var principal = GetPrincipalFromToken(token);
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

                                //tokenRes.is_rootuser = bool.Parse(S(clamModel?.is_rootuser)==null?"false");
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
        public static GetPrincipalModel GetPrincipalFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenS = tokenHandler.ReadJwtToken(token);
                var SignatureAlgorithm = tokenS.SignatureAlgorithm;
                var exp = tokenS.Claims.First(claim => claim.Type == "exp").Value;
                switch (SignatureAlgorithm)
                {
                    case "RS256":
                        // handle SSO token
                        var tokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = false,
                            IssuerSigningKeys = ConfigAuth.openIdConfig.SigningKeys //Key giải mã token  
                        };
                        SecurityToken securityToken;
                        long currentDate = ConvertToTimestamp(DateTime.UtcNow);
                        if (long.Parse(exp) >= currentDate)
                        {
                            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                            return new GetPrincipalModel(principal, "RS256");
                        }
                        break;
                    case "HS256":
                        // handle local token 
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.Get("SECRET_KEY")));
                        var tokenValidationParameters_local = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateLifetime = false
                        };
                        var tokenHandler_local = new JwtSecurityTokenHandler();
                        SecurityToken securityToken_local;
                        var tokenS_local = tokenHandler.ReadToken(token) as JwtSecurityToken;
                        var exp_local = tokenS_local.Claims.First(claim => claim.Type == "exp").Value;
                        long currentDate_local = ConvertToTimestamp(DateTime.UtcNow);
                        if (long.Parse(exp_local) >= currentDate_local)
                        {
                            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters_local, out securityToken_local);
                            return new GetPrincipalModel(principal, "HS256");
                        }
                        break;
                }
                return new GetPrincipalModel(null, "");
            }
            catch
            {
                return new GetPrincipalModel(null, "");
            }
        }

        public static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }
        //public static async Task LogErrorToKafka(string topic, Exception ex)
        //{
        //    //send log kafka from service handle kafka
        //    LogSystemErrorModel logMess = new LogSystemErrorModel(ex);
        //    ProducerWrapper<LogSystemErrorModel> _producer = new ProducerWrapper<LogSystemErrorModel>();
        //    await _producer.CreateMess(Topic.LOG_ERROR_SYSTEM, logMess, null, topic);
        //}
        //public static async Task LogErrorToKafka(string mess)
        //{
        //    //send log kafka from rest api
        //    LogSystemErrorModel logMess = new LogSystemErrorModel(mess);
        //    ProducerWrapper<LogSystemErrorModel> _producer = new ProducerWrapper<LogSystemErrorModel>();
        //    await _producer.CreateMess(Topic.LOG_ERROR_SYSTEM, logMess);
        //}

        public static string GetMethodName(StackTrace stackTrace)
        {
            var method = stackTrace.GetFrame(0).GetMethod();

            string _methodName = method.DeclaringType.FullName;

            if (_methodName.Contains(">") || _methodName.Contains("<"))
            {
                _methodName = _methodName.Split('<', '>')[1];
            }
            else
            {
                _methodName = method.Name;
            }

            return _methodName;
        }

    }
}
