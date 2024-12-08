using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.BCC01_EF;

namespace Repository.Repositories
{
    public interface IPermissionRepository : IBaseRepositorySql<BCC01_Permission>
    {
        Task<List<PermissionResShort>> GetListPermissionByUser(string username, string is_root, string is_admin);
        Task<List<ContainerModel>> GetListPermissionIdByProfileId(Guid profile_id);
        Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request);
        Task<ListResult<BCC01_Permission>> GetPermissionByProfile(PagingParam param);
        Task<object> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request);
        Task<object> GetStatusPermissionTypeAObjectByUser(PermissionAObjectByTypeRequest request);
    }
}