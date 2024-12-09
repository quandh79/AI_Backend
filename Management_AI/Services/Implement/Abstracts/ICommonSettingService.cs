using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.BCC01_EF;
using Management_AI.Models.Main;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface ICommonSettingService
    {
        Task<ResponseService<ListResult<BCC01_CommonSetting>>> GetAll(PagingParam param);
        Task<ResponseService<BCC01_CommonSetting>> Update(CommonSettingAddRequest request);
        Task<ResponseService<BCC01_CommonSetting>> GetByKey(string key);
        Task<IEnumerable<BCC01_CommonSetting>> GetAllCommonSetting();
    }
}
