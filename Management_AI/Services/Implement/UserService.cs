using AutoMapper;
using Common;
using Common.Commons;
using Microsoft.Extensions.Caching.Memory;
using Repository.Repositories;
using Repository.CustomModel;
using Newtonsoft.Json;
using Common.Params.Base;
using Repository.Repositories.Abtracts;
using Repository.BCC01_EF;
using Management_AI.Models.Main;
using Management_AI.Models.Common;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Config;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class UserService : BaseService, IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly ITenantsService _tenantsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IMemoryCache _cache;
        private readonly ITenantExtensionRepository _tenantExtensionRepository;
        private readonly IMapUserReportToRepository _mapUserReportToRepository;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ITenantExtensionRepository tenantExtensionRepository,
            ILogger logger,
            ITenantsService tenantsService,
            IMapUserReportToRepository mapUserReportToRepository
           ) : base(logger, mapper)
        {
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _cache = ConfigContainerDJ.CreateInstance<IMemoryCache>();
            _tenantsService = tenantsService;
            _tenantExtensionRepository = tenantExtensionRepository;
            _mapUserReportToRepository = mapUserReportToRepository;
        }
        public async Task<ResponseService<ListResult<UserCustomResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                var current_user = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
                ListResult<UserCustomResponse> result = await _userRepository.GetListUser(param, current_user);
                return new ResponseService<ListResult<UserCustomResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<UserCustomResponse>>(ex);
            }
        }
        public async Task<IEnumerable<BCC01_User>> GetAllUser()
        {
            //if (_cache.TryGetValue(Constants.CACHE_BCC01_USER, out IEnumerable<BCC01_User> lstUser))
            //{
            //    if (lstUser.Any())
            //    {
            //        return lstUser;
            //    }
            //}
            var lstUser = await _userRepository.GetAllUser();

            //var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(600))
            //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //        .SetPriority(CacheItemPriority.Normal)
            //        .SetSize(1024);

            //_cache.Remove(Constants.CACHE_BCC01_USER);
            //_cache.Set(Constants.CACHE_BCC01_USER, lstUser, cacheEntryOptions);

            return lstUser;
        }
        public async Task<ResponseService<UserCustomResponse>> Create(UserRequest obj, bool isLdap = false)
        {
            try
            {
                if (isLdap)
                {
                    obj?.AddInfoLdap();
                }
                else
                {
                    obj.AddInfo();
                }
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                UserCustomResponse result = null;
                if (obj.tenant_id == null)
                {
                    obj.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                }
                //var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                // var tenant = await _tenantsService.GetById(obj.tenant_id);

                //var checkNumberEmployeeCreateByTenant = await _userRepository.CheckNumberEmployeeCreate(obj.tenant_id);
                //if (!checkNumberEmployeeCreateByTenant)
                //{
                //    return new ResponseService<UserCustomResponse>(Constants.EMPLOYEE_CREATION_LIMIT).BadRequest(MessCodes.EMPLOYEE_CREATION_LIMIT);
                //}
                var checkExistsUsername = await _userRepository.GetSingle(x => x.username == obj.username || !string.IsNullOrEmpty(obj.extension_number) && x.extension_number == obj.extension_number);
                if (checkExistsUsername != null)
                {
                    return new ResponseService<UserCustomResponse>(Constants.USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS).BadRequest(MessCodes.USERNAME_IS_EXISTS);
                }
                if (!string.IsNullOrEmpty(obj.extension_number))
                {
                    var user = await _userRepository.GetSingle(x => x.extension_number == obj.extension_number);
                    if (user != null)
                    {
                        return new ResponseService<UserCustomResponse>(Constants.USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS).BadRequest(MessCodes.USERNAME_IS_EXISTS);
                    }
                }
                obj.password = HashString.StringToHash(obj.password, Constants.HASH_SHA512);
                BCC01_User user_entity = _mapper.Map<UserRequest, BCC01_User>(obj);
                // Add agent to database
                var addAgentIntoDatabase = await _userRepository.CreateAndMapUserToProfile(user_entity);

                //save agent ReportTo cho multi Sup
                if (obj.lstReportTo != null && obj.lstReportTo.Any())
                {
                    var lstUserResportTo = new List<BCC01_MapUserReportTo>();
                    foreach (var temp in obj.lstReportTo)
                    {
                        var userReportTo = new BCC01_MapUserReportTo()
                        {
                            id = new Guid(),
                            tenant_id = obj.tenant_id,
                            username = obj.username,
                            username_sup = temp,
                            create_by = obj.create_by,
                            create_time = obj.create_time,

                        };
                        lstUserResportTo.Add(userReportTo);
                    }
                    await _mapUserReportToRepository.AddRange(lstUserResportTo);
                }

                result = _mapper.Map<BCC01_User, UserCustomResponse>(user_entity);
                result.role_name = obj.role_name;
                //Chú ý: Sẽ Có TH ko thể insert table BCC01_MapUserReportTo => xảy ra lỗi ko đồng nhất view và db
                result.lstReportTo = result.lstReportTo;
                _logger.LogError($"{nameof(Create)} user result: {JsonConvert.SerializeObject(result)}");
                return new ResponseService<UserCustomResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }
        public async Task<ResponseService<UserCustomResponse>> Update(UserUpdateRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.UpdateInfo();

                var checkAgent = await _userRepository.GetSingle(x => x.username == obj.username && x.tenant_id == obj.tenant_id);
                if (checkAgent == null)
                {
                    return new ResponseService<UserCustomResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                var checkRole = await _userRepository.GetRoleByUser(obj.role_id, obj.tenant_id);
                if (checkRole == null)
                {
                    return new ResponseService<UserCustomResponse>(Constants.ROLE_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }
                if (!string.IsNullOrEmpty(obj.extension_number))
                {
                    var user = await _userRepository.GetSingle(x => x.extension_number == obj.extension_number);

                    if (user != null && user.email != obj.email)
                    {
                        return new ResponseService<UserCustomResponse>(Constants.USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS).BadRequest(MessCodes.USERNAME_IS_EXISTS);
                    }
                }
                // User entity
                BCC01_User user_entity = _mapper.Map<UserUpdateRequest, BCC01_User>(obj);
                /*
                    TH: API Update from SSO khong update truong extension
                 */
                if (obj.flag_SSO != null && obj.flag_SSO.Value)
                {
                    user_entity.extension_number = checkAgent.extension_number;
                }
                else
                {
                    user_entity.extension_number = obj.extension_number;
                }
                user_entity.extension_password = checkAgent.extension_password;
                user_entity.create_time = checkAgent.create_time;
                user_entity.create_by = checkAgent.create_by;
                user_entity.avatar = checkAgent.avatar;

                if (!string.IsNullOrEmpty(obj.password_new))
                {
                    user_entity.password = HashString.StringToHash(obj.password_new, Constants.HASH_SHA512);
                }
                else
                {
                    user_entity.password = checkAgent.password;
                }

                var updateOnDatabase = await _userRepository.Update(user_entity, user_entity.username);

                //update table MapUserReportTo
                var lstResporto = await _mapUserReportToRepository.GetAll(new PagingParam()
                {
                    limit = 0,
                    page = 0,
                    search_list = new List<SearchParam>()
                    {
                        new SearchParam
                        {
                            conjunction = "AND",
                            name_field = "username",
                            value_search = obj.username
                        }
                    },
                    tenant_id = obj.tenant_id
                });
                // Lấy ra All UserReportTo rồi xóa đi, add lại cái mới theo thông tin mới
                await _mapUserReportToRepository.RemoveRange(lstResporto.items);
                //add New
                if (obj.lstReportTo != null && obj.lstReportTo.Any())
                {
                    var lstUserResportTo = new List<BCC01_MapUserReportTo>();
                    foreach (var temp in obj.lstReportTo)
                    {
                        var userReportTo = new BCC01_MapUserReportTo()
                        {
                            id = new Guid(),
                            tenant_id = obj.tenant_id,
                            username = obj.username,
                            username_sup = temp,
                            create_by = obj.create_by,
                            create_time = obj.create_time,
                        };
                        lstUserResportTo.Add(userReportTo);
                    }
                    await _mapUserReportToRepository.AddRange(lstUserResportTo);
                }
                UserCustomResponse response = _mapper.Map<BCC01_User, UserCustomResponse>(updateOnDatabase);
                response.role_name = checkRole.role_name;
                response.role_parent_id = checkRole.role_parent_id;
                response.lstReportTo = obj.lstReportTo;
                return new ResponseService<UserCustomResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }
        public async Task<ResponseService<bool>> Delete(string username)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));

                var checkAgent = await _userRepository.GetSingle(x => x.username == username && x.tenant_id == tenant_id);
                if (checkAgent == null)
                {
                    return new ResponseService<bool>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                var deleteFromDatabase = await _userRepository.RemoveAgentDataAndAssignReportTo(checkAgent);
                await _mapUserReportToRepository.RemoveAll(username);
                if (!deleteFromDatabase.status)
                {
                    return new ResponseService<bool>(Constants.REMOVE_DATA_FAILED).BadRequest(MessCodes.DATA_INVALID);
                }
                return new ResponseService<bool>(deleteFromDatabase.status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        public async Task<ResponseService<UserCustomResponse>> GetById(string username)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                var result = await _userRepository.GetFullById(username, tenant_id);
                return new ResponseService<UserCustomResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }
        public async Task<ResponseService<BCC01_User>> GetByExtension(string extension)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                var result = await _userRepository.GetFullByExtension(extension, tenant_id);
                return new ResponseService<BCC01_User>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_User>(ex);
            }
        }
        public async Task<ResponseService<ListResult<UserCustomResponse>>> GetListUserRole(UsernameRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                if (string.IsNullOrEmpty(request.username))
                    request.username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
                if (request.tenant_id.Equals(Guid.Empty))
                    request.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                return await _userRepository.GetListUserRole(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<UserCustomResponse>>(ex);
            }
        }
        public async Task<ResponseService<UserState>> GetUserState(UsernameRequest request)
        {
            UserState result = null;
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                if (request.tenant_id.Equals(Guid.Empty))
                {
                    request.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                }
                var user_found = await _userRepository.GetById(request.username);
                var tenantResponse = _tenantExtensionRepository.GetTenantExtension(request.tenant_id);

                if (user_found == null)
                {
                    _logger.LogError($"GetUserState - {request.username}: user is null");
                    return new ResponseService<UserState>(result);
                }

                result = new UserState(user_found.username, user_found.extension_number, user_found.fullname, user_found.avatar, user_found.phone, user_found.email, user_found.is_administrator, user_found.is_rootuser, Guid.Empty, user_found.role_id.ToString(), user_found.report_to, Constants.OFFLINE, Constants.NOT_READY, user_found.is_supervisor, user_found.is_agent, DateTime.Now.AddDays(2), tenantResponse?.prefix_extension);
                return new ResponseService<UserState>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserState>(ex);
            }
        }

        public async Task<BCC01_User> GetUserByExtesnion(string ext)
        {
            return await _userRepository.GetSingle(x => x.extension_number == ext);

        }

        public async Task<ResponseService<UserModel>> CheckExistByEmail(ItemModel<string> items)
        {

            var bcc01_user = await _userRepository.GetSingle(x => x.email == items.item);
            if (bcc01_user == null)
            {
                return new ResponseService<UserModel>(Constants.USER_NOT_FOUND);
            }
            var res = _mapper.Map<BCC01_User, UserModel>(bcc01_user);
            //get list ReportTo
            var lstResporto = await _mapUserReportToRepository.GetAll(new PagingParam()
            {
                limit = 0,
                page = 0,
                search_list = new List<SearchParam>()
                {
                    new SearchParam
                    {
                        conjunction = "AND",
                        name_field = "username",
                        value_search = items.item
                    }
                },
                tenant_id = items.tenant_id
            });
            if (lstResporto != null)
            {
                res.lstReportTo = lstResporto.items.Select(x => x.username).ToList();
            }
            return new ResponseService<UserModel>(res);
        }
    }
}
