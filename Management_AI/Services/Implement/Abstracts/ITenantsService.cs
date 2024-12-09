using Common.Commons;
using Common.Params.Base;
using Management_AI.Models.Main;
using Repository.BCC01_EF;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface ITenantsService
    {
        Task<ResponseService<TenantResponse>> Create(RegisterRequest request);
        Task<ResponseService<TenantSSOReponse>> CreateSSO(RegisterRequest request);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<TenantResponse>>> GetAll(PagingParam param);
        Task<ResponseService<BCC01_Tenants>> GetById(object id);
        Task<ResponseService<BCC01_TenantsResponse>> Update(BCC01_Tenants obj);
        Task<ResponseService<bool>> UpdateIsActive(bool isActive, object id);
        Task<ResponseService<TenantResponse>> CheckExitTenantByEmail(string email);
    }
}
