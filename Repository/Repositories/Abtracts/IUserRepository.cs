using Common.Commons;
using Common.Params.Base;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IUserRepository : IBaseRepositorySql<BCC01_User>
    {
        Task<List<BCC01_User>> GetAllUser();
        Task<bool> CheckNumberEmployeeCreate(Guid tenant_id);
        Task<ResponseService<bool>> CreateAndMapUserToProfile(BCC01_User user_entity);
        Task<BCC01_RoleHierarchy> GetRoleByUser(string role_id, Guid tenant_id);
        Task<ResponseService<bool>> RemoveAgentDataAndAssignReportTo(BCC01_User current_user);
        Task<ListResult<UserCustomResponse>> GetListUser(PagingParam param, string current_user);
        Task<UserCustomResponse> GetFullById(string username, Guid tenant_id);
        Task<BCC01_User> GetFullByExtension(string extension, Guid tenant_id);
        Task<ResponseService<ListResult<UserCustomResponse>>> GetListUserRole(UsernameRequest request);
    }
}