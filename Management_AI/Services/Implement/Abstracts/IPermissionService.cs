using Common.Commons;
using Common.Params.Base;
using Repository.EF;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.BCC01_EF;
using Management_AI.Models.Main;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IPermissionService
    {
        Task<ResponseService<BCC01_Permission>> Create(PermissionRequest obj);
        Task<ResponseService<bool>> Delete(object id);
        Task<ResponseService<ListResult<PermissionResponse>>> GetAll(PagingParam param);
        Task<ResponseService<PermissionResponse>> GetById(object id);
        Task<ResponseService<ListResult<PermissionResponse>>> GetListPermissionByProfileId(PagingParam param);
        Task<List<PermissionResShort>> GetListPermissionByUser(string username);
        Task<List<ContainerModel>> GetListPermissionIdByProfileId(Guid profile_id);
        Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request);
        Task<object> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request);
        Task<object> GetStatusPermissionTypeAObjectByUser(PermissionAObjectByTypeRequest request);
        Task<ResponseService<BCC01_Permission>> Update(PermissionRequest obj);
    }
}