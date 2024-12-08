using Common.Commons;
using Repository.BCC01_EF;
using Repository.CustomModel;
using Repository.Model;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ITenantsRepository : IBaseRepositorySql<BCC01_Tenants>
    {
        Task<ResponseService<bool>> DeleteAll(object id, string token);
        Task<ResponseService<bool>> UpdateIsActiveTenant(bool isActive, object id);
        Task<ResponseService<TenantCustomResponseModel>> CreateTenant(BCC01_RegisterInformation register_information_entity, BCC01_DbContextSql dbContext, string phone_cucm);
        Task<bool> CheckExistsEmail(Guid id, string email, BCC01_DbContextSql dbContext);
        Task<BCC01_Tenants> CheckExistByEmail(string email);
    }
}