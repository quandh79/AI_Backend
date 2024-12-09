using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.EF;
using Repository.Repositories.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.BCC01_EF;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class RoleHierarchyService : BaseService, IRoleHierarchyService
    {
        private readonly IRoleHierarchyRepository _roleHierarchyRepository;
        public RoleHierarchyService(IRoleHierarchyRepository roleHierarchyRepository, ILogger logger,
                                 IMapper mapper) : base(logger, mapper)
        {
            _roleHierarchyRepository = roleHierarchyRepository;
        }
        public async Task<ResponseService<List<RoleModel>>> RoleRecursive()
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                var result = await _roleHierarchyRepository.RoleRecursive(tenant_id);
                return new ResponseService<List<RoleModel>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ResponseService<List<RoleModel>>(ex);
            }
        }
        public async Task<ResponseService<ListResult<RoleHierarchyResponse>>> GetAll(PagingParam param)
        {
            try
            {
                ListResult<RoleHierarchyResponse> result = new ListResult<RoleHierarchyResponse>();
                var is_admin = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN);
                var current_user = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
                param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var res = _mapper.Map<ListResult<BCC01_RoleHierarchy>, ListResult<RoleHierarchyResponse>>(await _roleHierarchyRepository.GetAll(param));
                if (is_admin.ToLower().Equals("true"))
                {
                    result = res;
                    if (res.items.Any())
                    {
                        res.items.ForEach(x =>
                        {
                            if (x.role_parent_id.ToString().ToLower().Equals(Constants.ROOT_ROLE.ToLower()))
                                x.is_report_to_root = true;
                        });
                    }
                }
                else
                {
                    result.items = new List<RoleHierarchyResponse>();
                    var currentrole = await _roleHierarchyRepository.GetRoleByUser(current_user);
                    result.items.Add(currentrole);
                    var agentroles = await _roleHierarchyRepository.GetListRoleAgent(current_user);
                    foreach (var item in agentroles)
                    {
                        var itemFound = res.items.Find(x => x.id.Equals(item.id));
                        result.items.Add(itemFound);
                    }
                    result.total = result.items.Count;
                }
                return new ResponseService<ListResult<RoleHierarchyResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<RoleHierarchyResponse>>(ex);
            }
        }
        public async Task<ResponseService<BCC01_RoleHierarchy>> GetById(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_RoleHierarchy result = await _roleHierarchyRepository.GetById(id);
                return new ResponseService<BCC01_RoleHierarchy>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_RoleHierarchy>(ex);
            }
        }
        public async Task<ResponseService<BCC01_RoleHierarchy>> Create(RoleHierarchyRequest obj)
        {
            try
            {
                obj.AddInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_RoleHierarchy request = _mapper.Map<RoleHierarchyRequest, BCC01_RoleHierarchy>(obj);
                BCC01_RoleHierarchy result = await _roleHierarchyRepository.Create(request);
                return new ResponseService<BCC01_RoleHierarchy>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_RoleHierarchy>(ex);
            }
        }
        public async Task<ResponseService<BCC01_RoleHierarchy>> Update(RoleHierarchyRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_RoleHierarchy request = _mapper.Map<RoleHierarchyRequest, BCC01_RoleHierarchy>(obj);
                BCC01_RoleHierarchy result = await _roleHierarchyRepository.Update(request, obj.id);
                return new ResponseService<BCC01_RoleHierarchy>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_RoleHierarchy>(ex);
            }
        }
        public async Task<ResponseService<bool>> Delete(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                bool result = await _roleHierarchyRepository.RemoveAndAutoConnectRoleLevel((Guid)id);
                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        public async Task<ResponseService<bool>> DeleteAndTransferRole(DeleteRoleRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                bool result = await _roleHierarchyRepository.DeleteAndTransferRole(request);
                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        public async Task<ResponseService<List<UserRoleResponse>>> GetListUserReportTo(Guid role_parent_id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<UserRoleResponse> result = await _roleHierarchyRepository.GetUserReportTo(role_parent_id);
                return new ResponseService<List<UserRoleResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<List<UserRoleResponse>>(ex);
            }
        }
        public async Task<ResponseService<List<UserRoleResponse>>> GetListUserEqualByRoleId(Guid role_id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                List<UserRoleResponse> result = await _roleHierarchyRepository.GetListUserEqualByRoleId(role_id, tenant_id);
                return new ResponseService<List<UserRoleResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<List<UserRoleResponse>>(ex);
            }
        }

        public async Task<ResponseService<ListResult<RoleHierarchyResponse>>> GetAllByTenantId(Guid tenant_id)
        {
            var roles = await _roleHierarchyRepository.GetAllRoleByTenantId(tenant_id);

            var res = _mapper.Map<ListResult<BCC01_RoleHierarchy>, ListResult<RoleHierarchyResponse>>(new ListResult<BCC01_RoleHierarchy>(roles, roles.Count));
            return new ResponseService<ListResult<RoleHierarchyResponse>>(res);
        }
    }
}
