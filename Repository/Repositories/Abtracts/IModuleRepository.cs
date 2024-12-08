using Repository.CustomModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Repository.BCC01_EF;
using Common.Commons;

namespace Repository.Repositories
{
    public interface IModuleRepository : IBaseRepositorySql<BCC01_Module>
    {
        Task<BCC01_Module> Create(BCC01_Module obj);
        Task<List<ModuleCustomResponse>> GetAllModule(string username, bool is_admin, Guid tenant_id);
        Task<ResponseService<bool>> AsyncDefaultModule();

    }
}