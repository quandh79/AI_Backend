using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Management_AI.Config;
using System.Collections.Generic;
using Repository.BCC01_EF;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class ModuleService : BaseService, IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        public ModuleService(
            IModuleRepository moduleRepository,
            ILogger logger,
            IMapper mapper) : base(logger, mapper)
        {
            _moduleRepository = moduleRepository;
        }
        #region implement

        public async Task<ResponseService<ListResult<ModuleResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                var is_admin = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN) != null ? SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN).ToLower() : "false";
                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                var modules = await _moduleRepository.GetAll(param);
                ListResult<ModuleResponse> result = _mapper.Map<ListResult<BCC01_Module>, ListResult<ModuleResponse>>(modules);

                if (result.items.Count > 0 && !is_admin.Equals("true"))
                {
                    result.items.ForEach(x =>
                    {
                        if (Constants.MODULE_OBJECTS.Contains(x.module_name))
                            x.is_active = false;
                    });
                }

                result.items = result.items.OrderBy(x => x.position).ToList();
                return new ResponseService<ListResult<ModuleResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<ModuleResponse>>(ex);
            }
        }

        public async Task<ResponseService<List<ModuleCustomResponse>>> GetAllWithIsShow()
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
                var is_admin = SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN).ToLower() == "true" ? true : false;
                var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                var modules = await _moduleRepository.GetAllModule(username, is_admin, tenant_id);

                return new ResponseService<List<ModuleCustomResponse>>(modules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<List<ModuleCustomResponse>>(ex);
            }
        }

        public async Task<ResponseService<ModuleResponse>> GetById(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ModuleResponse result = _mapper.Map<BCC01_Module, ModuleResponse>(await _moduleRepository.GetById(id));
                return new ResponseService<ModuleResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ModuleResponse>(ex);
            }
        }
        public async Task<ResponseService<ModuleResponse>> Create(ModuleRequest obj)
        {
            try
            {
                obj.AddInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_Module request = _mapper.Map<ModuleRequest, BCC01_Module>(obj);
                var moduleRes = await _moduleRepository.Create(request);
                ModuleResponse result = null;
                if (moduleRes != null)
                {
                    result = _mapper.Map<BCC01_Module, ModuleResponse>(moduleRes);
                    return new ResponseService<ModuleResponse>(result);
                }
                return new ResponseService<ModuleResponse>("Module is exists !!").BadRequest(MessCodes.MODULE_IS_EXISTS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ModuleResponse>(ex);
            }
        }
        public async Task<ResponseService<ModuleResponse>> Update(ModuleRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_Module request = _mapper.Map<ModuleRequest, BCC01_Module>(obj);
                ModuleResponse result = _mapper.Map<BCC01_Module, ModuleResponse>(await _moduleRepository.Update(request, obj.id));
                return new ResponseService<ModuleResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ModuleResponse>(ex);
            }
        }
        public async Task<ResponseService<bool>> Delete(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                bool result = await _moduleRepository.Delete(id);
                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        #endregion
        public async Task<ResponseService<bool>> AsyncDefaultModule()
        {
            await _moduleRepository.AsyncDefaultModule();
            return new ResponseService<bool>(true);
        }


    }
}
