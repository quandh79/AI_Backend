using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Main;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IRoleHierarchyService
    {
        Task<ResponseService<List<RoleModel>>> RoleRecursive();
        Task<ResponseService<ListResult<RoleHierarchyResponse>>> GetAll(PagingParam param);
        Task<ResponseService<BCC01_RoleHierarchy>> GetById(object id);
        Task<ResponseService<BCC01_RoleHierarchy>> Create(RoleHierarchyRequest obj);
        Task<ResponseService<BCC01_RoleHierarchy>> Update(RoleHierarchyRequest obj);
        Task<ResponseService<bool>> Delete(object id);
        Task<ResponseService<bool>> DeleteAndTransferRole(DeleteRoleRequest request);
        Task<ResponseService<List<UserRoleResponse>>> GetListUserReportTo(Guid role_parent_id);
        Task<ResponseService<List<UserRoleResponse>>> GetListUserEqualByRoleId(Guid role_id);
        Task<ResponseService<ListResult<RoleHierarchyResponse>>> GetAllByTenantId(Guid tenant_id);

    }
}
