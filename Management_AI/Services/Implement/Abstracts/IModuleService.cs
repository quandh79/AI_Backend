using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management_AI.Models.Main;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IModuleService
    {
        Task<ResponseService<ModuleResponse>> Create(ModuleRequest obj);
        Task<ResponseService<bool>> Delete(object id);
        Task<ResponseService<ListResult<ModuleResponse>>> GetAll(PagingParam param);
        Task<ResponseService<ModuleResponse>> GetById(object id);
        Task<ResponseService<ModuleResponse>> Update(ModuleRequest obj);
        Task<ResponseService<List<ModuleCustomResponse>>> GetAllWithIsShow();
        Task<ResponseService<bool>> AsyncDefaultModule();

    }
}