using Repository.Repositories;
using Repository.Repositories.Abtracts;
using Repository.Utility;
using Management_AI.Services.Implement;
using Management_AI.Services.Implement.Abstracts;

namespace Management_AI.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependency(this IServiceCollection services)
        {
            //repositories
            //

            //services

            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<ICallLogRepository, CallLogRepository>();
            services.AddTransient<IMapAgentGroupRepository, MapAgentGroupRepository>();
            services.AddTransient<IStoreProcedureExcute, StoreProcedureExcute>();
            services.AddTransient<ITenantsService, TenantsService>();
            services.AddTransient<ITenantsRepository, TenantsRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICommonSettingRepository, CommonSettingRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IRoleHierarchyRepository, RoleHierarchyRepository>();
            services.AddTransient<IRoleHierarchyService, RoleHierarchyService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<ITenantExtensionRepository, TenantExtensionRespository>();
            services.AddTransient<ICommonSettingService, CommonSettingService>();
            services.AddTransient<IDefaultCommonSettingService, DefaultCommonSettingService>();
            services.AddTransient<IDefaultCommonSettingRepository, DefaultCommonSettingRepository>();
            services.AddTransient<ICallTransferLogRepository, CallTransferLogRepository>();

            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IMapTeamUserRepository, MapTeamUserRepository>();
            services.AddTransient<IEventCallRepository, EventCallRepository>();

            services.AddTransient<IMapTeamUserService, MapTeamUserService>();
            services.AddTransient<IMapUserReportToRepository, MapUserReportToRepository>();

            services.AddTransient<IMapProfileUserRepository, MapProfileUserRepository>();
           // services.AddTransient<IMapProfileUserService, MapProfileUserService>();


        }
    }
}
