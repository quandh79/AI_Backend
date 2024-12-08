using Repository.BCC01_EF;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Abtracts
{
    public interface IRoleHierarchyRepository : IBaseRepositorySql<BCC01_RoleHierarchy>
    {
        Task<List<RoleModel>> RoleRecursive(Guid tenant_id);
        Task<RoleHierarchyResponse> GetRoleByUser(string username);
        Task<List<RoleModel>> GetListRoleAgent(string username);
        Task<bool> RemoveAndAutoConnectRoleLevel(Guid id);
        Task<bool> DeleteAndTransferRole(DeleteRoleRequest request);
        Task<List<UserRoleResponse>> GetListUserAgent(string username, Guid tenant_id);
        Task<List<UserRoleResponse>> GetUserReportTo(Guid role_parent_id);
        Task<List<UserRoleResponse>> GetListUserEqualByRoleId(Guid role_id, Guid tenant_id);
        Task<List<BCC01_RoleHierarchy>> GetAllRoleByTenantId(Guid tenant_id);
    }
}
