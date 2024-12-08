using Common;
using Common.Commons;
using Common.Params.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AuthenticationRepository : BaseRepositorySql<BCC01_RegisterInformation>, IAuthenticationRepository
    {
        private readonly ICommonSettingRepository _commonSettingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenRepository _tokenRepository;
        private readonly string baseUrl = ConfigHelper.Get("LINK_KONG_USER_IC");
        public AuthenticationRepository(
            ICommonSettingRepository commonSettingRepository, ITokenRepository tokenRepository
            ) : base()
        {
            _commonSettingRepository = commonSettingRepository;
            _httpContextAccessor = new HttpContextAccessor();
            _tokenRepository = tokenRepository;
        }
        public async Task<ResponseService<TokenResponseCRM>> GetToken(TokenResquest request)
        {
            TokenResponseCRM result = null;
            // throw new NotImplementedException();
            var user = _db.Set<BCC01_User>().Where(x => x.email == request.userName).FirstOrDefault();
            if (user == null)
            {
                return new ResponseService<TokenResponseCRM>(Constants.LOGIN_FAILED).BadRequest(MessCodes.LOGIN_FAILED);

            }
            //get tenant
            var tenant_by_user = await _db.BCC01_Tenants.Where(x => x.id == user.tenant_id).FirstOrDefaultAsync();
            if (tenant_by_user == null)
            {
                return new ResponseService<TokenResponseCRM>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
            }
           
            // create token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(ConfigHelper.Get("SECRET_KEY"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username", request.userName),
                    new Claim("role_id", user.role_id.ToString()),
                    new Claim("is_administrator",user.is_administrator.ToString()),
                    new Claim("is_rootuser",user.is_rootuser.ToString()),
                    new Claim("is_supervisor",user.is_supervisor.ToString()),
                    new Claim("is_agent",user.is_agent.ToString()),
                    new Claim("asterisk_id",""),
                    new Claim("tenant_id",user.tenant_id.ToString()),
                    new Claim("extension_number",""),
                }),

                Expires = DateTime.UtcNow.AddDays(int.Parse(ConfigHelper.Get("TOKEN_EXPIRATION_TIME"))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Fix
            result = CommonFuncMain.ToObject<TokenResponseCRM>(user);
            result.token = tokenHandler.WriteToken(token);
            result.extension = user.extension_number;
            result.email = user.email;
            return new ResponseService<TokenResponseCRM>(result);
        }
        public virtual async Task<ResponseService<LoginResponse>> Login(LoginRequest request)
        {
            LoginResponse result = null;
            var passwd = HashString.StringToHash(request.password, Constants.HASH_SHA512);
            // check exists username
            try
            {
                var userFound = _db.Set<BCC01_User>().Where(x => x.email == request.email).FirstOrDefault();
                if (userFound != null)
                {
                    int max_time_for_type_wrong_password = 0;
                    int time_block_by_type_wrong_password = 0;
                    PagingParam param = new PagingParam();
                    param.tenant_id = userFound.tenant_id;
                    var commonResult = await _commonSettingRepository.GetAll(param);
                    if (commonResult != null)
                    {
                        var item_number_wrong_pass = commonResult.items.Find(x => x.setting_key.Equals(Constants.MAX_TIME_FOR_TYPE_WRONG_PASSWORD));
                        var item_timer_block = commonResult.items.Find(x => x.setting_key.Equals(Constants.TIME_BLOCK_BY_TYPE_WRONG_PASSWORD));
                        max_time_for_type_wrong_password = item_number_wrong_pass != null ? int.Parse(item_number_wrong_pass.value) : 5;
                        time_block_by_type_wrong_password = item_timer_block != null ? int.Parse(item_timer_block.value) : 5;
                    }
                    else
                    {
                        max_time_for_type_wrong_password = 5;
                        time_block_by_type_wrong_password = 5;
                    }


                    if (userFound.wrong_password_number >= max_time_for_type_wrong_password)
                    {
                        if (userFound.block_time == null || userFound.block_time <= DateTime.Now)
                        {
                            userFound.wrong_password_number = 0;
                            userFound.reason_deactive = "";
                            userFound.block_time = null;
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                            return new ResponseService<LoginResponse>(result, time_block_by_type_wrong_password.ToString()).BadRequest(MessCodes.MAX_WRONG_PASSWORD_NUMBER);
                        }

                    }


                    BCC01_User user = await _db.Set<BCC01_User>().Where(x => x.email == request.email && x.password == passwd).FirstOrDefaultAsync();                   
                    if (user != null)
                    {
                        var tenant_by_user = await _db.BCC01_Tenants.Where(x => x.id == user.tenant_id).FirstOrDefaultAsync();
                        if (tenant_by_user == null)
                        {
                            return new ResponseService<LoginResponse>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                        }
                        
                        user.wrong_password_number = 0;
                        if (request.is_remember_me) //True tạo token thời hạn 17 ngày
                        {
                            // create token 
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.UTF8.GetBytes(ConfigHelper.Get("SECRET_KEY"));

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                    new Claim("username", request.email),
                                    new Claim("role_id", user.role_id.ToString()),
                                    new Claim("is_administrator", user.is_administrator.ToString()),
                                    new Claim("is_rootuser", user.is_rootuser.ToString()),
                                    new Claim("is_supervisor", user.is_supervisor.ToString()),
                                    new Claim("is_agent", user.is_agent.ToString()),
                                    new Claim("asterisk_id", ""),
                                    new Claim("tenant_id", user.tenant_id.ToString()),
                                    new Claim("extension_number", "")
                                }),
                                Expires = DateTime.UtcNow.AddDays(int.Parse(ConfigHelper.Get("TOKEN_EXPIRATION_TIME")) + 7),

                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            result = CommonFuncMain.ToObject<LoginResponse>(user);
                            result.token = tokenHandler.WriteToken(token);
                            result.asterisk_id = null;
                        }
                        else
                        {
                            // create token 
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var secretKey = ConfigHelper.Get("SECRET_KEY");
                            var key = Encoding.UTF8.GetBytes(secretKey);
                            user.extension_number = (user.extension_number != null ? user.extension_number.ToString() : "");

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                        new Claim("username", request.email),
                                        new Claim("role_id", user.role_id.ToString()),
                                        new Claim("is_administrator",user.is_administrator.ToString()),
                                        new Claim("is_rootuser",user.is_rootuser.ToString()),
                                        new Claim("is_supervisor",user.is_supervisor.ToString()),
                                        new Claim("is_agent",user.is_agent.ToString()),
                                        new Claim("asterisk_id",""),
                                        new Claim("tenant_id",user.tenant_id.ToString()),
                                        new Claim("extension_number",user.extension_number)
                                }),
                                Expires = DateTime.UtcNow.AddDays(int.Parse(ConfigHelper.Get("TOKEN_EXPIRATION_TIME"))),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            var tenant = _db.Set<BCC01_Tenants>().Where(x => x.id == user.tenant_id).FirstOrDefault();
                            //Fix
                            result = CommonFuncMain.ToObject<LoginResponse>(user);
                            result.token = tokenHandler.WriteToken(token);
                            result.asterisk_id = null;                           
                        }
                        result.is_active_tenant = tenant_by_user.is_active == true ? true : false;
                        await _db.SaveChangesAsync();
                    }
                    else // user not found
                    {
                        //increase wrong password number
                        userFound.wrong_password_number += 1;
                        if (userFound.wrong_password_number >= max_time_for_type_wrong_password)
                        {
                            userFound.reason_deactive = Constants.BLOCK_BY_INPUT_WRONG_PASS;
                            userFound.block_time = DateTime.Now.AddMinutes(time_block_by_type_wrong_password);
                            await _db.SaveChangesAsync();
                            return new ResponseService<LoginResponse>(result, time_block_by_type_wrong_password.ToString()).BadRequest(MessCodes.MAX_WRONG_PASSWORD_NUMBER);
                        }

                        await _db.SaveChangesAsync();
                        return new ResponseService<LoginResponse>(Constants.LOGIN_FAILED).BadRequest(MessCodes.LOGIN_FAILED);
                    }
                }
                else
                {
                    return new ResponseService<LoginResponse>(Constants.LOGIN_FAILED).BadRequest(MessCodes.LOGIN_FAILED);
                }
            }
            catch (Exception ex)
            {
                return new ResponseService<LoginResponse>(ex.Message);
            }
            return new ResponseService<LoginResponse>(result);
        }
        public virtual async Task<ResponseService<string>> Logout()
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
                if (token != null)
                {
                    // process token 
                    await _tokenRepository.DeactivateAsync(token.Split(' ').Last());
                    return new ResponseService<string>(true, "Logout success !!!", "success");
                }
            }
            catch
            {
                return new ResponseService<string>("Logout fail !!").BadRequest(MessCodes.LOGOUT_FAIL);
            }
            return new ResponseService<string>("Logout fail !!").BadRequest(MessCodes.LOGOUT_FAIL);
        }
        public virtual async Task<string> ReceiveForgotPassword(string register_key)
        {
            var result = "";
            var public_key = CommonFuncMain.GenerateCoupon();
            using (var dbContext = new BCC01_DbContextSql())
            {
                var query = (from f in dbContext.BCC01_ForgotPassword where f.register_key.Equals(register_key) select f);
                if (query.Any())
                {
                    if (string.IsNullOrEmpty(query.FirstOrDefault().public_key))
                    {
                        query.FirstOrDefault().public_key = public_key;
                    }
                    result = public_key;
                }
                await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public virtual async Task<ResponseService<string>> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                var currentDatetime = DateTime.Now.AddMinutes(-10);
                using (var dbContext = new BCC01_DbContextSql())
                {
                    if (await dbContext.Set<BCC01_User>().Where(x => x.email == request.email).AsNoTracking().FirstOrDefaultAsync() != null)
                    {
                        var listItem = await dbContext.Set<BCC01_ForgotPassword>().Where(x => x.email == request.email && x.create_time > currentDatetime).AsNoTracking().ToListAsync();
                        if (!listItem.Any())
                        {
                            var register_key = CommonFuncMain.GenerateCoupon();
                            BCC01_ForgotPassword m01_Forgot = new BCC01_ForgotPassword();
                            m01_Forgot.id = Guid.NewGuid();
                            m01_Forgot.email = request.email;
                            m01_Forgot.register_key = register_key;
                            m01_Forgot.create_time = DateTime.Now;
                            dbContext.Set<BCC01_ForgotPassword>().Add(m01_Forgot);
                            var registerUrl = $"{baseUrl}/api/auth/receive-forgot-password?key={register_key}";
                            await dbContext.SaveChangesAsync();

                            return new ResponseService<string>(true, "Register forgot password success !!!", registerUrl);
                        }
                        else
                        {
                            return new ResponseService<string>("You have submitted a request").BadRequest(MessCodes.YOU_HAVE_SUMITTED_REQUEST);
                        }
                    }
                }
                return new ResponseService<string>("Email does not exists !").BadRequest(MessCodes.EMAIL_DOES_NOT_EXISTS);
            }
            catch (Exception ex)
            {
                return new ResponseService<string>(ex);
            }

        }
        public virtual async Task<ResponseService<string>> UpdateNewPassword(ResetPasswordRequest request)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var currentDatetime = DateTime.Now.AddMinutes(-10);
                    var entity = await _db.Set<BCC01_ForgotPassword>().Where(x => x.public_key == request.public_key && x.create_time > currentDatetime).FirstOrDefaultAsync();
                    if (entity != null)
                    {
                        request.UpdateInfo();
                        var user = _db.Set<BCC01_User>().Find(entity.email);
                        if (user != null) user.password = request.password;
                        _db.BCC01_ForgotPassword.Remove(entity);
                        await _db.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return new ResponseService<string>(true, "Update password success !!!", entity.email);
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseService<string>(ex.Message);
                }
            }

            return new ResponseService<string>("Public key has expired or invalid").BadRequest(MessCodes.PUBLIC_KEY_HAS_EXPIRED);
        }

    }
}
