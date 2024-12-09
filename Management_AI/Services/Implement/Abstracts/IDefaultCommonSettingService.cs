using Common.Commons;
using System.Threading.Tasks;
using System;
using Repository.EF;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.BCC01_EF;
using Management_AI.Models.Main;

namespace Management_AI.Services.Implement.Abstracts
{
    public interface IDefaultCommonSettingService
    {
        Task<ResponseService<ListResult<BCC01_DefaultCommonSetting>>> GetAll(PagingParam param);
        Task<ResponseService<BCC01_DefaultCommonSetting>> Create(DefaultCommonSettingAddRequest request);
        Task<ResponseService<BCC01_DefaultCommonSetting>> Update(DefaultCommonSettingAddRequest request);
        Task<ResponseService<bool>> Delete(Guid id);

        //Task syncDataWebhookUrlRedis(bool isDelete);
        //Task syncDataWebhookMissCallRedis(bool isDelete);
    }
}
