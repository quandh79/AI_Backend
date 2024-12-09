using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.EF;
using Repository.CustomModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management_AI.Config;
using System.Linq;
using Repository.BCC01_EF;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class PermissionService : BaseService, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository, ILogger logger,
                                 IMapper mapper) : base(logger, mapper)
        {
            _permissionRepository = permissionRepository;
        }
        #region implement
        public async Task<ResponseService<ListResult<PermissionResponse>>> GetAll(PagingParam param)
        {
            try
            {
                param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ListResult<PermissionResponse> result = _mapper.Map<ListResult<BCC01_Permission>, ListResult<PermissionResponse>>(await _permissionRepository.GetAll(param));
                return new ResponseService<ListResult<PermissionResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<PermissionResponse>>(ex);
            }
        }

        public async Task<ResponseService<PermissionResponse>> GetById(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                PermissionResponse result = _mapper.Map<BCC01_Permission, PermissionResponse>(await _permissionRepository.GetById(id));
                return new ResponseService<PermissionResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionResponse>(ex);
            }
        }
        public async Task<ResponseService<BCC01_Permission>> Create(PermissionRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_Permission request = _mapper.Map<PermissionRequest, BCC01_Permission>(obj);
                BCC01_Permission result = await _permissionRepository.Create(request);
                return new ResponseService<BCC01_Permission>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_Permission>(ex);
            }
        }
        public async Task<ResponseService<BCC01_Permission>> Update(PermissionRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_Permission request = _mapper.Map<PermissionRequest, BCC01_Permission>(obj);
                BCC01_Permission result = await _permissionRepository.Update(request, obj.id);
                return new ResponseService<BCC01_Permission>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_Permission>(ex);
            }
        }
        public async Task<ResponseService<bool>> Delete(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                bool result = await _permissionRepository.Delete(id);
                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        public async Task<List<ContainerModel>> GetListPermissionIdByProfileId(Guid profile_id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<ContainerModel> result = await _permissionRepository.GetListPermissionIdByProfileId(profile_id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new List<ContainerModel>();
            }
        }

        public async Task<ResponseService<ListResult<PermissionResponse>>> GetListPermissionByProfileId(PagingParam param)
        {
            try
            {
                param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ListResult<PermissionResponse> result = _mapper.Map<ListResult<BCC01_Permission>, ListResult<PermissionResponse>>(await _permissionRepository.GetPermissionByProfile(param));
                return new ResponseService<ListResult<PermissionResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<PermissionResponse>>(ex);
            }
        }
        public async Task<List<PermissionResShort>> GetListPermissionByUser(string username)
        {
            try
            {
                var is_root = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT).ToLower() : "false";
                var is_admin = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN).ToLower() : "false";
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                List<PermissionResShort> result = CommonFuncMain.ToObjectList<PermissionResShort>(await _permissionRepository.GetListPermissionByUser(username, is_root, is_admin));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new List<PermissionResShort>();
            }
        }
        public async Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                PermissionResShort result = CommonFuncMain.ToObject<PermissionResShort>(await _permissionRepository.GetPermissionAObjectByUser(request));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new PermissionResShort();
            }
        }
        public async Task<object> GetStatusPermissionTypeAObjectByUser(PermissionAObjectByTypeRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _permissionRepository.GetStatusPermissionTypeAObjectByUser(request);
                if (result != null) return (bool)result;
                return "Error paramater!!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return ex.Message;
            }
        }
        public async Task<object> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request)
        {
            try
            {
                var is_root = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT).ToLower() : "false";
                var is_admin = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN).ToLower() : "false";
                if (is_root.Equals("true"))
                {
                    return true;
                }
                else
                {
                    if (is_admin.Equals("true"))
                    {
                        if (Constants.ROOT_PERMISSIONS.Contains(request.permission_name))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                        var result = await _permissionRepository.GetStatusPermissionByTypeAndName(request);
                        if (result != null) return (bool)result;
                        return "Error paramater!!";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return ex.Message;
            }
        }
        #endregion
    }
}
