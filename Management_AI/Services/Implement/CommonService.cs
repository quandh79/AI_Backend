using Common.Commons;
using Common.Params.Base;
using Newtonsoft.Json;
using Common;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Management_AI.Models.Main;
using Management_AI.Services.Implement.Abstracts;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.Services.Implement
{
    public class CommonService : BaseService, ICommonService
    {
        //public static CallHub _callHub;
        private readonly IUserService _userService;
        public CommonService(
            //CallHub callHub,
            ILogger logger,
            IMapper mapper,
            IUserService userService
            ) : base(logger, mapper)
        {
            //_callHub = callHub;
            _userService = userService;
        }

        //public async Task<ResponseService<bool>> PushClientUpdateAgentState(ResponseMessage<AgentStatusModel> message)
        //{
        //    _logger.LogError($"{nameof(PushClientUpdateAgentState)} PushClientUpdateAgentState: {JsonConvert.SerializeObject(message.data)}");
        //    try
        //    {
        //        if (message.data != null)
        //        {
        //            var param = new PagingParam()
        //            {
        //                page = 0,
        //                limit = 0,
        //                search_list = new List<SearchParam>(),
        //                sorts = new List<SortParam>(),
        //                tenant_id = message.data.tenant_id,
        //            };

        //            //var lstUser = await new UserAPI().GetAllUserStateNotReport(param);
        //            var lstUser = (await _userService.GetAllUser()).Where(x => x.tenant_id == message.data.tenant_id).ToList();

        //            var listConnectionId = new List<string>();
        //            if (lstUser != null && lstUser.Any())
        //            {
        //                if (message.data.flag == true)
        //                {
        //                    lstUser = lstUser.Where(x => x.username != message.data.username).ToList();
        //                }

        //                foreach (var user in lstUser)
        //                {
        //                    var extension = !string.IsNullOrEmpty(user.extension_number) ? user.extension_number : user.username;
        //                    if (await Redis<bool>.CheckExistsAsync(extension, Constants.PREFIX_REDIS_JTAPI))
        //                    {
        //                        var configManager = await Redis<ConnectionManager>.GetAsync(extension, Constants.PREFIX_REDIS_JTAPI);
        //                        listConnectionId.Add(configManager?.connection_id);
        //                        _logger.LogError($"extension_number: {extension} - {user.username} - {configManager?.connection_id}");
        //                    }

        //                }
        //                if (listConnectionId != null && listConnectionId.Any())
        //                {
        //                    try
        //                    {
        //                        if (listConnectionId != null && listConnectionId.Any() && _callHubContext.Clients != null)
        //                            //await _callHub.PushClientUpdateAgentState(listConnectionId, message.data);
        //                            await _callHubContext.Clients.Clients(listConnectionId).SendAsync("PushClientUpdateAgentState", message.data);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        _logger.LogError($"{nameof(PushClientUpdateAgentState)}: {ex.Message}");
        //                    }

        //                    //await _callHub.PushClientUpdateAgentState(listConnectionId, message.data);
        //                }
        //                if (message.data?.is_sup_set == true)
        //                {
        //                    if (await Redis<bool>.CheckExistsAsync(message.data?.extension_number, Constants.PREFIX_REDIS_JTAPI))
        //                    {
        //                        // semd sinalR to fontEnd
        //                        var extension = !string.IsNullOrEmpty(message.data?.extension_number) ? message.data?.extension_number : message.data?.username;
        //                        var configManagerFontEnd = await Redis<ConnectionManager>.GetAsync(extension, Constants.PREFIX_REDIS_JTAPI);
        //                        await _callHubContext.Clients.Client(configManagerFontEnd.connection_id).SendAsync("PushAgentChangeState3rd", message.data);
        //                        _logger.LogError($"extension_number: {extension} - {message.data?.username} - {configManagerFontEnd?.connection_id}");
        //                    }
        //                }
        //            }
        //        }
        //        return await Task.FromResult(new ResponseService<bool>());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"{nameof(PushClientUpdateAgentState)}: {ex.Message}");
        //        return await Task.FromResult(new ResponseService<bool>(ex));
        //    }
        //}
    }
}
