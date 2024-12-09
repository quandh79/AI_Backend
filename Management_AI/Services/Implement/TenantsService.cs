using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Repositories;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class TenantsService : BaseService, ITenantsService
    {
        private readonly ITenantsRepository _tenantsRepository;
        private readonly IUserRepository _userRepository;
        public TenantsService(ITenantsRepository tenantsRepository, IUserRepository userRepository, ILogger logger, IMapper mapper) : base(logger, mapper)
        {
            _tenantsRepository = tenantsRepository;
            _userRepository = userRepository;
        }
        public async Task<ResponseService<TenantResponse>> Create(RegisterRequest request)
        {
            using (var dbContext = new BCC01_DbContextSql())
            {
                using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var password_jtapi = request.password;
                        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                        request.AddInfo();
                        ResponseService<TenantResponse> result = null;

                        var checkEmail = await _tenantsRepository.CheckExistsEmail(request.id, request.email, dbContext);
                        if (checkEmail)
                        {
                            return new ResponseService<TenantResponse>(Constants.EMAIL_IS_ALREADY_EXISTS).BadRequest(MessCodes.EMAIL_IS_EXISTS);
                        }
                        var registerEntity = CommonFuncMain.ToObject<BCC01_RegisterInformation>(request);
                        // Create tenant
                        var createTenant = await _tenantsRepository.CreateTenant(registerEntity, dbContext, "");//response.data
                        if (!createTenant.status)
                        {
                            return new ResponseService<TenantResponse>(createTenant.message).BadRequest(MessCodes.CREATE_TENANT_FAILED);
                        }
                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        result = _mapper.Map<ResponseService<TenantCustomResponseModel>, ResponseService<TenantResponse>>(createTenant);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex);
                        await transaction.RollbackAsync();
                        return new ResponseService<TenantResponse>(ex);
                    }
                }
            }
        }
        public async Task<ResponseService<TenantSSOReponse>> CreateSSO(RegisterRequest request)
        {
            var resCreateTenant = await Create(request);
            if (resCreateTenant.status)
            {
                var res = _mapper.Map<TenantResponse, TenantSSOReponse>(resCreateTenant.data);
                new ResponseService<TenantSSOReponse>(res);
            }
            return new ResponseService<TenantSSOReponse>(resCreateTenant.message).BadRequest(MessCodes.CREATE_TENANT_FAILED);
        }
        public async Task<ResponseService<BCC01_TenantsResponse>> Update(BCC01_Tenants obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                var checkTenant = await _tenantsRepository.GetSingle(x => x.id == obj.id);
                if (checkTenant == null)
                {
                    return new ResponseService<BCC01_TenantsResponse>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }
                obj.modify_time = DateTime.Now;

                // update
                BCC01_Tenants result = await _tenantsRepository.Update(obj, obj.id);
                var response = _mapper.Map<BCC01_Tenants, BCC01_TenantsResponse>(result);

                var user = await _userRepository.GetSingle(x => x.email == obj.email);
                if (user != null)
                {
                    response.role_id = user.role_id.ToString();
                }
                return new ResponseService<BCC01_TenantsResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_TenantsResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            using (var dbContext = new BCC01_DbContextSql())
            {
                using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var checkExistsTenant = await dbContext.BCC01_Tenants.Where(x => x.id == id).FirstOrDefaultAsync();
                        if (checkExistsTenant == null)
                        {
                            return new ResponseService<bool>(Constants.TENANT_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                        }
                        DeleteTenantData(dbContext, checkExistsTenant);

                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return new ResponseService<bool>();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex);
                        await transaction.RollbackAsync();
                        return new ResponseService<bool>(ex);
                    }
                }
            }
        }
        public async Task<ResponseService<ListResult<TenantResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.flag = false;
                var listTenant = await _tenantsRepository.GetAll(param);
                ListResult<TenantResponse> result = _mapper.Map<ListResult<BCC01_Tenants>, ListResult<TenantResponse>>(listTenant);
                return new ResponseService<ListResult<TenantResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<TenantResponse>>(ex);
            }
        }
        public async Task<ResponseService<BCC01_Tenants>> GetById(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_Tenants result = await _tenantsRepository.GetById(id);
                return new ResponseService<BCC01_Tenants>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_Tenants>(ex);
            }
        }
        public async Task<ResponseService<bool>> UpdateIsActive(bool isActive, object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                return await _tenantsRepository.UpdateIsActiveTenant(isActive, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        private void DeleteTenantData(BCC01_DbContextSql dbContext, BCC01_Tenants tenant)
        {
            // Remove tenant
            dbContext.BCC01_Tenants.Remove(tenant);

            // Remove user
            var listUser = dbContext.BCC01_User.Where(x => x.tenant_id == tenant.id).ToList();
            listUser.ForEach(x => dbContext.BCC01_User.Remove(x));

            // Remove role
            var listRole = dbContext.BCC01_RoleHierarchy.Where(x => x.tenant_id == tenant.id).ToList();
            listRole.ForEach(x => dbContext.BCC01_RoleHierarchy.Remove(x));

            // Remove profile 
            var listProfile = dbContext.BCC01_Profile.Where(x => x.tenant_id == tenant.id).ToList();
            listProfile.ForEach(x => dbContext.BCC01_Profile.Remove(x));

            // Remove map profile user
            var listMapProfileUser = dbContext.BCC01_MapProfileUser.Where(x => x.tenant_id == tenant.id).ToList();
            listMapProfileUser.ForEach(x => dbContext.BCC01_MapProfileUser.Remove(x));

            // Remove permission 
            var listPermission = dbContext.BCC01_Permission.Where(x => x.tenant_id == tenant.id).ToList();
            listPermission.ForEach(x => dbContext.BCC01_Permission.Remove(x));

            // Remove module 
            var listModule = dbContext.BCC01_Module.Where(x => x.tenant_id == tenant.id).ToList();
            listModule.ForEach(x => dbContext.BCC01_Module.Remove(x));

            // Remove permission object
            var listPermissionObject = dbContext.BCC01_PermissionObject.Where(x => x.tenant_id == tenant.id).ToList();
            listPermissionObject.ForEach(x => dbContext.BCC01_PermissionObject.Remove(x));

            // Remove callflow component
            var listCallFlowComponent = dbContext.BCC01_CallFlowComponent.Where(x => x.tenant_id == tenant.id).ToList();
            listCallFlowComponent.ForEach(x => dbContext.BCC01_CallFlowComponent.Remove(x));

            // Remove map agent group
            var listMapAgentGroup = dbContext.BCC01_MapAgentGroup.Where(x => x.tenant_id == tenant.id).ToList();
            listMapAgentGroup.ForEach(x => dbContext.BCC01_MapAgentGroup.Remove(x));

            // Remove queue
            var listQueue = dbContext.BCC01_Queue.Where(x => x.tenant_id == tenant.id).ToList();
            listQueue.ForEach(x => dbContext.BCC01_Queue.Remove(x));

            //remove MapUserReportTo
            var lstmapReportTo = dbContext.BCC01_MapUserReportTo.Where(x => x.tenant_id == tenant.id).ToList();
            lstmapReportTo.ForEach(x => dbContext.BCC01_MapUserReportTo.Remove(x));

            //remove MapUserTeamUser
            var lstmapTeamUser = dbContext.BCC01_MapTeamUser.Where(x => x.tenant_id == tenant.id).ToList();
            lstmapTeamUser.ForEach(x => dbContext.BCC01_MapTeamUser.Remove(x));

            //remove Team
            var lstmapTeam = dbContext.BCC01_Teams.Where(x => x.tenant_id == tenant.id).ToList();
            lstmapTeam.ForEach(x => dbContext.BCC01_Teams.Remove(x));
        }

        public async Task<ResponseService<TenantResponse>> CheckExitTenantByEmail(string email)
        {
            var bcc01_teant = await _tenantsRepository.CheckExistByEmail(email);
            if (bcc01_teant == null)
            {
                ResponseService<TenantResponse> resultError = new ResponseService<TenantResponse>(Constants.TENANT_NOT_FOUND);
                /// resultError.status_code = System.Net.HttpStatusCode.InternalServerError;
                return resultError;
            }
            var res = _mapper.Map<BCC01_Tenants, TenantResponse>(bcc01_teant);
            return new ResponseService<TenantResponse>(res);
        }
    }
}
