using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.EF;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.CustomModel;
using Repository.Queries;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management_AI.Config;
using Repository.BCC01_EF;
using Repository.Repositories.Abtracts;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;



namespace Management_AI.Services.Implement
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPermissionService _permissionService;
        private readonly IRoleHierarchyRepository _roleHierarchyRepository;
        private ConditionLinq<UserModel> _conditionLinq;
        public ProfileService(IProfileRepository profileRepository, IPermissionService permissionService, IRoleHierarchyRepository roleHierarchyRepository, ILogger logger,
                                 IMapper mapper) : base(logger, mapper)
        {
            _profileRepository = profileRepository;
            _permissionService = permissionService;
            _roleHierarchyRepository = roleHierarchyRepository;
            _conditionLinq = new ConditionLinq<UserModel>();
        }
        #region implement
        public async Task<ResponseService<ListResult<ProfileResponse>>> GetAll(PagingParam param)
        {
            try
            {
                param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ListResult<ProfileResponse> result = _mapper.Map<ListResult<BCC01_Profile>, ListResult<ProfileResponse>>(await _profileRepository.GetAll(param));
                result.items.OrderBy(x => x.modify_time);
                return new ResponseService<ListResult<ProfileResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<ProfileResponse>>(ex);
            }
        }

        public async Task<ResponseService<ProfileResponse>> GetById(object id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ProfileResponse result = _mapper.Map<BCC01_Profile, ProfileResponse>(await _profileRepository.GetById(id));
                return new ResponseService<ProfileResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProfileResponse>(ex);
            }
        }
        public async Task<ResponseService<ListResult<UserModel>>> GetListUserByProfile(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ListResult<UserModel> response = new ListResult<UserModel>();
                param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                var current_user = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
                ListResult<UserModel> res = await _profileRepository.GetUsersByProfile(param);
                var listUser = await _roleHierarchyRepository.GetListUserAgent(current_user, param.tenant_id);
                var usercurrentfound = res.items.Find(x => x.username.Equals(current_user));
                response.items = new List<UserModel>();
                if (usercurrentfound != null)
                    response.items.Add(usercurrentfound);
                foreach (var item in listUser)
                {
                    if (res.items.Any())
                    {
                        var userfound = res.items.Find(x => x.username.Equals(item.username));
                        if (userfound != null)
                            response.items.Add(userfound);
                    }
                }
                response = _conditionLinq.Process(param, response.items);
                return new ResponseService<ListResult<UserModel>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<UserModel>>(ex);
            }
        }

        public async Task<ResponseService<ProfileResponse>> Create(ProfileRequest obj)
        {
            try
            {
                obj.AddInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                BCC01_Profile request = _mapper.Map<ProfileRequest, BCC01_Profile>(obj);
                ProfileResponse result = _mapper.Map<BCC01_Profile, ProfileResponse>(await _profileRepository.Create(request));
                return new ResponseService<ProfileResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProfileResponse>(ex);
            }
        }
        public async Task<ResponseService<ProfileResponse>> Update(ProfileRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                if (!obj.profile_name.Equals(Constants.PROFILE_ADMIN))
                {
                    BCC01_Profile request = _mapper.Map<ProfileRequest, BCC01_Profile>(obj);
                    _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                    ProfileResponse result = _mapper.Map<BCC01_Profile, ProfileResponse>(await _profileRepository.Update(request, obj.id));
                    return new ResponseService<ProfileResponse>(result);
                }
                return new ResponseService<ProfileResponse>("Can't update profile Admin !!").BadRequest(708);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProfileResponse>(ex);
            }
        }

        public Task<ResponseService<bool>> UpdateIsActive(bool isActive, object id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseService<bool>> DeleteTransaction(object obj)
        {
            List<ContainerModel> listId = await _permissionService.GetListPermissionIdByProfileId((Guid)obj);
            _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
            using (var context = new BCC01_DbContextSql())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        BCC01_Profile proitem = context.Set<BCC01_Profile>().Find(obj);
                        if (!proitem.profile_name.Equals(Constants.PROFILE_ADMIN))
                        {
                            context.Set<BCC01_Profile>().Remove(proitem);
                            foreach (ContainerModel item in listId)
                            {
                                BCC01_Permission peritem = context.Set<BCC01_Permission>().Find(item.id);
                                context.Set<BCC01_Permission>().Remove(peritem);
                            }
                            var mproEntitys = context.Set<BCC01_MapProfileUser>().Where(x => x.profile_id == (Guid)obj).ToList();
                            mproEntitys.ForEach(x => context.Set<BCC01_MapProfileUser>().Remove(x));
                        }
                        else
                        {
                            return new ResponseService<bool>("Can't delete profile Admin !!").BadRequest(708);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex);
                        return new ResponseService<bool>(ex);
                    }
                }
            }
            return new ResponseService<bool>(true);
        }
        public async Task<ResponseService<bool>> UpdatePermissionInProfile(List<UpdatePermissonRequest> listrequest)
        {
            _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
            using (var context = new BCC01_DbContextSql())
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (UpdatePermissonRequest perRes in listrequest)
                        {
                            perRes.UpdateInfo();
                            var entitys = context.BCC01_Permission.Where(x => x.object_name.Equals(perRes.object_name) && x.profile_id.Equals(perRes.profile_id) && x.tenant_id.Equals(perRes.tenant_id)).ToList();
                            foreach (var item in entitys)
                            {
                                item.is_show = perRes.is_show;
                                item.is_allow_access = perRes.is_allow_access;
                                item.is_allow_create = perRes.is_allow_create;
                                item.is_allow_delete = perRes.is_allow_delete;
                                item.is_allow_edit = perRes.is_allow_edit;
                                item.modify_by = perRes.modify_by;
                                item.modify_time = perRes.modify_time;
                            }
                            await context.SaveChangesAsync();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex.Message);
                        return new ResponseService<bool>(ex);
                    }
                }
            }

            return new ResponseService<bool>(true);
        }
        public async Task<ResponseService<bool>> DeleteUserInProfile(DeleteUserInProfile request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                bool result = await _profileRepository.DeleteUserInProfile(request);
                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        #endregion
    }
}
