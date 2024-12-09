using AutoMapper;
using Common.Commons;
using Repository.Repositories;
using System.Threading.Tasks;
using System;
using Common;
using Common.Params.Base;
using Repository.CustomModel;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Caching.Memory;
using Repository.BCC01_EF;
using Management_AI.Config;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class CommonSettingService : BaseService, ICommonSettingService
    {
        private readonly ICommonSettingRepository _commonSettingRepository;
        private IMemoryCache _cache;
        public CommonSettingService(
            ICommonSettingRepository commonSettingRepository,
            ILogger logger,
            IMapper mapper) : base(logger, mapper)
        {
            _commonSettingRepository = commonSettingRepository;
            _cache = ConfigContainerDJ.CreateInstance<IMemoryCache>();
        }

        public async Task<ResponseService<BCC01_CommonSetting>> Update(CommonSettingAddRequest request)
        {
            try
            {
                request.UpdateInfo();

                var checkExistsSetting = await _commonSettingRepository.GetSingle(x => x.id == request.id && x.tenant_id == request.tenant_id);
                if (checkExistsSetting == null)
                {
                    return new ResponseService<BCC01_CommonSetting>(Constants.COMMON_SETTING_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                if (checkExistsSetting.common_type == Constants.COMMON_TYPE_PASSWORD)
                {
                    request.value = CommonFuncMain.Encrypt(request.value);
                }

                var entity = _mapper.Map<CommonSettingAddRequest, BCC01_CommonSetting>(request);
                entity.setting_key = checkExistsSetting.setting_key;
                entity.setting_for = checkExistsSetting.setting_for;
                entity.common_type = checkExistsSetting.common_type;
                entity.create_time = checkExistsSetting.create_time;
                entity.create_by = checkExistsSetting.create_by;

                var result = await _commonSettingRepository.Update(entity, request.id);
                return new ResponseService<BCC01_CommonSetting>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_CommonSetting>(ex);
            }
        }

        public async Task<ResponseService<ListResult<BCC01_CommonSetting>>> GetAll(PagingParam param)
        {
            try
            {
                param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                var listCommon = await _commonSettingRepository.GetAll(param);

                foreach (var item in listCommon.items)
                {
                    if (item.common_type == Constants.COMMON_TYPE_PASSWORD)
                    {
                        item.value = CommonFuncMain.Decrypt(item.value);
                    }
                }

                var result = new ListResult<BCC01_CommonSetting>(listCommon.items, listCommon.total);

                return new ResponseService<ListResult<BCC01_CommonSetting>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<BCC01_CommonSetting>>(ex);
            }
        }


        public async Task<ResponseService<BCC01_CommonSetting>> GetByKey(string key)
        {
            try
            {
                var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                var result = await _commonSettingRepository.GetSingle(x => x.tenant_id == tenant_id && x.setting_key == key);
                if (result.common_type == Constants.COMMON_TYPE_PASSWORD)
                {
                    result.value = CommonFuncMain.Decrypt(result.value);
                }

                return new ResponseService<BCC01_CommonSetting>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_CommonSetting>(ex);
            }
        }


        public async Task<IEnumerable<BCC01_CommonSetting>> GetAllCommonSetting()
        {
            //if (_cache.TryGetValue(Constants.CACHE_BCC01_COMMONSETTING, out IEnumerable<BCC01_CommonSetting> lstCommon))
            //{
            //    if (lstCommon.Any())
            //    {
            //        return lstCommon;
            //    }
            //}
            var lstCommon = await _commonSettingRepository.GetAllCommonSetting();

            //var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(600))
            //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //        .SetPriority(CacheItemPriority.Normal)
            //        .SetSize(1024);

            //_cache.Remove(Constants.CACHE_BCC01_COMMONSETTING);
            //_cache.Set(Constants.CACHE_BCC01_COMMONSETTING, lstCommon, cacheEntryOptions);

            return lstCommon;
        }
    }
}
