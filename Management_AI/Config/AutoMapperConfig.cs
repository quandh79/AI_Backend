using AutoMapper;
using Common.Commons;
using Repository.EF;
using Repository.CustomModel;
using Repository.BCC01_EF;
using Management_AI.Models.Main;

namespace Management_AI.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ModuleRequest, BCC01_Module>().ReverseMap();
            CreateMap<BCC01_Module, ModuleResponse>().ReverseMap();
            CreateMap<ListResult<BCC01_Module>, ListResult<ModuleResponse>>().ReverseMap();
            CreateMap<PermissionRequest, BCC01_Permission>().ReverseMap();
            CreateMap<BCC01_Permission, PermissionResponse>().ReverseMap();
            CreateMap<ListResult<BCC01_Permission>, ListResult<PermissionResponse>>().ReverseMap();
            CreateMap<ProfileRequest, BCC01_Profile>().ReverseMap();
            CreateMap<BCC01_Profile, ProfileResponse>().ReverseMap();
            CreateMap<ListResult<BCC01_Profile>, ListResult<ProfileResponse>>().ReverseMap();
            CreateMap<MapProfileUserRequest, BCC01_MapProfileUser>().ReverseMap();

            CreateMap<UserRequest, BCC01_User>().ReverseMap();
            CreateMap<BCC01_User, UserRequest>().ReverseMap();
            CreateMap<UserModel, BCC01_User>().ReverseMap();
            CreateMap<UserUpdateRequest, BCC01_User>().ReverseMap();
            CreateMap<BCC01_DefaultModule, ModuleModel>().ReverseMap();
            CreateMap<BCC01_User, UserCustomResponse>().ReverseMap();
            CreateMap<ListResult<BCC01_User>, ListResult<UserCustomResponse>>().ReverseMap();
            CreateMap<RoleHierarchyRequest, BCC01_RoleHierarchy>().ReverseMap();
            CreateMap<BCC01_RoleHierarchy, RoleHierarchyResponse>().ReverseMap();
            CreateMap<ListResult<BCC01_RoleHierarchy>, ListResult<RoleHierarchyResponse>>().ReverseMap();
            CreateMap<BCC01_Tenants, TenantResponse>().ReverseMap();
            CreateMap<ListResult<BCC01_Tenants>, ListResult<TenantResponse>>().ReverseMap();
            CreateMap<ResponseService<BCC01_Tenants>, ResponseService<TenantResponse>>().ReverseMap();
            CreateMap<TenantCustomResponseModel, TenantResponse>().ReverseMap();
            CreateMap<ResponseService<TenantCustomResponseModel>, ResponseService<TenantResponse>>().ReverseMap();
            CreateMap<CommonSettingAddRequest, BCC01_CommonSetting>().ReverseMap();
            CreateMap<DefaultCommonSettingAddRequest, BCC01_DefaultCommonSetting>().ReverseMap();
            CreateMap<BCC01_Tenants, BCC01_TenantsResponse>().ReverseMap();
            CreateMap<ResponseService<BCC01_TenantsResponse>, ResponseService<TenantResponse>>().ReverseMap();
            //Teams
            CreateMap<BCC01_Teams, TeamModel>().ReverseMap();
            CreateMap<TeamModel, BCC01_Teams>().ReverseMap();
            CreateMap<TenantResponse, TenantSSOReponse>().ReverseMap();
        }
    }
}
