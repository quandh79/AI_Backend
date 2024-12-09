using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.Repositories;
using Repository.BCC01_EF;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Models.Main;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class DefaultCommonSettingService : BaseService, IDefaultCommonSettingService
    {
        private readonly IDefaultCommonSettingRepository _defaultCommonSettingRepository;
        private readonly ITenantsRepository _tenantsRepository;
        private readonly ICommonSettingRepository _commonSettingRepository;
        public DefaultCommonSettingService(
            ITenantsRepository tenantsRepository,
          IDefaultCommonSettingRepository defaultCommonSettingRepository,
          ICommonSettingRepository commonSettingRepository,
          ILogger logger,
          IMapper mapper) : base(logger, mapper)
        {
            _defaultCommonSettingRepository = defaultCommonSettingRepository;
            _commonSettingRepository = commonSettingRepository;
            _tenantsRepository = tenantsRepository;
        }
        public async Task<ResponseService<ListResult<BCC01_DefaultCommonSetting>>> GetAll(PagingParam param)
        {
            try
            {
                param.flag = false;
                var result = await _defaultCommonSettingRepository.GetAll(param);
                foreach (var item in result.items)
                {
                    if (item.common_type == Constants.COMMON_TYPE_PASSWORD)
                    {
                        item.value = CommonFuncMain.Decrypt(item.value);
                    }
                }

                return new ResponseService<ListResult<BCC01_DefaultCommonSetting>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<BCC01_DefaultCommonSetting>>(ex);
            }
        }

        public async Task<ResponseService<BCC01_DefaultCommonSetting>> Create(DefaultCommonSettingAddRequest request)
        {
            try
            {
                request.AddInfo();

                var checkDuplicateSettingKey = await _defaultCommonSettingRepository.GetSingle(x => x.setting_key == request.setting_key);
                if (checkDuplicateSettingKey != null)
                {
                    return new ResponseService<BCC01_DefaultCommonSetting>(Constants.SETTING_KEY_ALREADY_EXISTS).BadRequest(MessCodes.SETTING_KEY_ALREADY_EXISTS);
                }

                // Encrypt if type is "password"
                if (request.common_type == Constants.COMMON_TYPE_PASSWORD)
                    request.value = CommonFuncMain.Encrypt(request.value);

                var entity = _mapper.Map<DefaultCommonSettingAddRequest, BCC01_DefaultCommonSetting>(request);

                var result = await _defaultCommonSettingRepository.CreateDefaultCommonSettingWithSyncData(entity);

                //if (result.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_URL)
                //{
                //    await syncDataWebhookUrlRedis(false);
                //}
                //if (result.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_MISSCALL)
                //{
                //    await syncDataWebhookMissCallRedis(false);
                //}
                ////LCM
                //if (result.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_URL)
                //{
                //    // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(false, Constants.HASHTAG_WEBHOOK_LCM_URL, Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_URL);
                //}
                ////LCM
                //if (result.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN)
                //{
                //    // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(false, Constants.HASHTAG_WEBHOOK_LCM_TOKEN, Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN);
                //}

                // update cache data CommonSetting
                //var cacheData = await _commonSettingRepository.GetAllCommonSetting();
                //CacheData.Cache_BCC01_CommonSetting = cacheData;

                //ProducerWrapper<List<string>> producer = new ProducerWrapper<List<string>>();
                //await producer.CreateMess(Topic.RELOAD_CACHE_DATA, new List<string>()
                //{
                //    Constants.CACHE_BCC01_COMMONSETTING,
                //});

                return new ResponseService<BCC01_DefaultCommonSetting>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_DefaultCommonSetting>(ex);
            }
        }

        public async Task<ResponseService<BCC01_DefaultCommonSetting>> Update(DefaultCommonSettingAddRequest request)
        {
            try
            {
                request.UpdateInfo();

                var checkExistsSetting = await _defaultCommonSettingRepository.GetSingle(x => x.id == request.id);
                if (checkExistsSetting == null)
                {
                    return new ResponseService<BCC01_DefaultCommonSetting>(Constants.COMMON_SETTING_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                var checkDuplicateSettingKey = await _defaultCommonSettingRepository.GetSingle(x => x.id != request.id && x.setting_key == request.setting_key);
                if (checkDuplicateSettingKey != null)
                {
                    return new ResponseService<BCC01_DefaultCommonSetting>(Constants.SETTING_KEY_ALREADY_EXISTS).BadRequest(MessCodes.SETTING_KEY_ALREADY_EXISTS);
                }

                // Encrypt if type is "password"
                if (request.common_type == Constants.COMMON_TYPE_PASSWORD)
                {
                    request.value = CommonFuncMain.Encrypt(request.value);
                }

                var entity = _mapper.Map<DefaultCommonSettingAddRequest, BCC01_DefaultCommonSetting>(request);
                entity.setting_key = checkExistsSetting.setting_key;
                entity.create_by = checkExistsSetting.create_by;
                entity.create_time = checkExistsSetting.create_time;

                var result = await _defaultCommonSettingRepository.UpdateDefaultCommonSettingWithSyncData(entity);

                //if (result.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_URL)
                //{
                //    await syncDataWebhookUrlRedis(false);
                //}
                //if (result.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_MISSCALL)
                //{
                //    await syncDataWebhookMissCallRedis(false);
                //}
                ////LCM
                //if (checkExistsSetting.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_URL)
                //{
                //    // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(false, Constants.HASHTAG_WEBHOOK_LCM_URL, Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_URL);
                //}
                ////LCM
                //if (checkExistsSetting.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN)
                //{
                //    // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(false, Constants.HASHTAG_WEBHOOK_LCM_TOKEN, Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN);
                //}

                //// update cache data CommonSetting
                ////var cacheData = await _commonSettingRepository.GetAllCommonSetting();
                ////CacheData.Cache_BCC01_CommonSetting = cacheData;

                //ProducerWrapper<List<string>> producer = new ProducerWrapper<List<string>>();
                //await producer.CreateMess(Topic.RELOAD_CACHE_DATA, new List<string>()
                //{
                //    Constants.CACHE_BCC01_COMMONSETTING,
                //});

                return new ResponseService<BCC01_DefaultCommonSetting>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BCC01_DefaultCommonSetting>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                var checkExistsSetting = await _defaultCommonSettingRepository.GetSingle(x => x.id == id);
                if (checkExistsSetting == null)
                {
                    return new ResponseService<bool>(Constants.COMMON_SETTING_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                var result = await _defaultCommonSettingRepository.Delete(id);

                //if (checkExistsSetting.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_URL)
                //{
                //    // await syncDataWebhookUrlRedis(true);
                //    await syncDataWebhookRedis(true, Constants.HASHTAG_WEBHOOK, Constants.KEY_COMMON_SETTING_WEBHOOK_URL);
                //}
                //if (checkExistsSetting.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_MISSCALL)
                //{
                //   // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(true, Constants.HASHTAG_WEBHOOK_MISSCALL, Constants.KEY_COMMON_SETTING_WEBHOOK_MISSCALL);
                //}
                ////LCM
                //if (checkExistsSetting.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_URL)
                //{
                //    // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(true, Constants.HASHTAG_WEBHOOK_LCM_URL, Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_URL);
                //}
                ////LCM
                //if (checkExistsSetting.setting_key == Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN)
                //{
                //    // await syncDataWebhookMissCallRedis(true);
                //    await syncDataWebhookRedis(true, Constants.HASHTAG_WEBHOOK_LCM_TOKEN, Constants.KEY_COMMON_SETTING_WEBHOOK_LCM_TOKEN);
                //}


                // update cache data CommonSetting
                //var cacheData = await _commonSettingRepository.GetAllCommonSetting();
                //CacheData.Cache_BCC01_CommonSetting = cacheData;

                //ProducerWrapper<List<string>> producer = new ProducerWrapper<List<string>>();
                //await producer.CreateMess(Topic.RELOAD_CACHE_DATA, new List<string>()
                //{
                //    Constants.CACHE_BCC01_COMMONSETTING,
                //});


                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        //public async Task syncDataWebhookUrlRedis(bool isDelete)
        //{
        //    if (isDelete)
        //    {
        //        var lstData = await _tenantsRepository.GetAll(new PagingParam()
        //        {
        //            flag = false
        //        });

        //        if (lstData != null && lstData.items != null)
        //        {
        //            foreach (var item in lstData.items)
        //            {
        //                await Redis<string>.RemoveKeyAsync(item.id.ToString().ToLower(), Constants.HASHTAG_WEBHOOK);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        var lstData = await _commonSettingRepository.GetAll(new PagingParam()
        //        {
        //            flag = false,
        //            search_list = new List<SearchParam>()
        //            {
        //                new SearchParam()
        //                {
        //                    name_field = "setting_for",
        //                    value_search = "common",
        //                    conjunction = "AND"
        //                },
        //                new SearchParam()
        //                {
        //                    name_field = "setting_key",
        //                    value_search = Constants.KEY_COMMON_SETTING_WEBHOOK_URL,
        //                    conjunction = "AND"
        //                }
        //            }
        //        });

        //        if (lstData != null && lstData.items != null)
        //        {
        //            foreach (var item in lstData.items)
        //            {
        //                await Redis<string>.SetAsync(item.tenant_id.ToString().ToLower(), item.value, Constants.HASHTAG_WEBHOOK);
        //            }
        //        }
        //    }

        //}
        //public async Task syncDataWebhookMissCallRedis(bool isDelete)
        //{
        //    if (isDelete)
        //    {
        //        var lstData = await _tenantsRepository.GetAll(new PagingParam()
        //        {
        //            flag = false
        //        });

        //        if (lstData != null && lstData.items != null)
        //        {
        //            foreach (var item in lstData.items)
        //            {
        //                await Redis<string>.RemoveKeyAsync(item.id.ToString().ToLower(), Constants.HASHTAG_WEBHOOK_MISSCALL);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        var lstData = await _commonSettingRepository.GetAll(new PagingParam()
        //        {
        //            flag = false,
        //            search_list = new List<SearchParam>()
        //            {
        //                new SearchParam()
        //                {
        //                    name_field = "setting_for",
        //                    value_search = "common",
        //                    conjunction = "AND"
        //                },
        //                new SearchParam()
        //                {
        //                    name_field = "setting_key",
        //                    value_search = Constants.KEY_COMMON_SETTING_WEBHOOK_MISSCALL,
        //                    conjunction = "AND"
        //                }
        //            }
        //        });

        //        if (lstData != null && lstData.items != null)
        //        {
        //            foreach (var item in lstData.items)
        //            {
        //                await Redis<string>.SetAsync(item.tenant_id.ToString().ToLower(), item.value, Constants.HASHTAG_WEBHOOK_MISSCALL);
        //            }
        //        }
        //    }

        //}
        //public async Task syncDataWebhookRedis(bool isDelete, string hashtag, string keyRedis)
        //{
        //    if (isDelete)
        //    {
        //        var lstData = await _tenantsRepository.GetAll(new PagingParam()
        //        {
        //            flag = false
        //        });

        //        if (lstData != null && lstData.items != null)
        //        {
        //            foreach (var item in lstData.items)
        //            {
        //                await Redis<string>.RemoveKeyAsync(item.id.ToString().ToLower(), hashtag);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        var lstData = await _commonSettingRepository.GetAll(new PagingParam()
        //        {
        //            flag = false,
        //            search_list = new List<SearchParam>()
        //            {
        //                new SearchParam()
        //                {
        //                    name_field = "setting_for",
        //                    value_search = "common",
        //                    conjunction = "AND"
        //                },
        //                new SearchParam()
        //                {
        //                    name_field = "setting_key",
        //                    value_search = keyRedis,
        //                    conjunction = "AND"
        //                }
        //            }
        //        });

        //        if (lstData != null && lstData.items != null)
        //        {
        //            foreach (var item in lstData.items)
        //            {
        //                await Redis<string>.SetAsync(item.tenant_id.ToString().ToLower(), item.value, hashtag);
        //            }
        //        }
        //    }

        //}
    }
}
