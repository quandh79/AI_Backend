//using Common;
//using Common.Commons;
//using Common.Params.Base;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Management_AI.Common;
//using Management_AI.Common.ResponAPI3rd;
//using Management_AI.Config;
//using Management_AI.Models.EventResponse;
//using Management_AI.Models.Main;
//using Management_AI.Services.Models;
//using Repository.CustomModel;

//namespace Management_AI.RestApi
//{
//    public class UserAPI
//    {
//        private RequestAPI _requestAPI;
//        private HttpResponseMessage _response;
//        private readonly string PATH_PRE_API = "user/";
//        private readonly string secret_key = ConfigManager.Get(Constants.CONF_API_SECRET_KEY);

//        public UserAPI()
//        {
//            _requestAPI = new RequestAPI().ToFabio(ConfigManager.Get(Constants.CONF_SOURCE_USER_FABIO));
//            _requestAPI.client.DefaultRequestHeaders.Add("API_SECRET_KEY", secret_key);
//        }

//        public async Task<ResponseService<UserInfo>> GetUserByExtension(string extensionNumber)
//        {
//            ResponseService<UserInfo> res = new ResponseService<UserInfo>();
//            try
//            {
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get_user_by_extension", extensionNumber);
//                res = await _response.Content.ReadAsAsync<ResponseService<UserInfo>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }

//        public async Task<ResponseService<ListResult<UserState>>> GetAllUserStateNotReport(PagingParam param)
//        {
//            ResponseService<ListResult<UserState>> res = new ResponseService<ListResult<UserState>> ();
//            try
//            {
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-all-user-state-not-report-to", param);
//                res = await _response.Content.ReadAsAsync<ResponseService<ListResult<UserState>>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
        
//        }
//        public async Task<ResponseService<ListResult<ExResponse>>> GetAllExtensionReady(ExtensionReadyResquest param)
//        {
//            ResponseService<ListResult<ExResponse>> res = new ResponseService<ListResult<ExResponse>> ();
//            try
//            {
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-all-extension-ready", param);
//                res = await _response.Content.ReadAsAsync<ResponseService<ListResult<ExResponse>>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }
//        public async Task<ResponseService<GetTokenResponse>> GetToken(GetTokenResquest param)
//        {

//            ResponseService<GetTokenResponse> res = new ResponseService<GetTokenResponse>();
//            try
//            {
//                //Test local api/user/get-all-user-state-not-report-to
//                //_response = await _requestAPI.client.PostAsJsonAsync("api/user/get_user_by_extension", extensionNumber);
//                _response = await _requestAPI.client.PostAsJsonAsync("auth/GetToken", param);//PATH_PRE_API + "GetToken"
//                res = await _response.Content.ReadAsAsync<ResponseService<GetTokenResponse>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }
//        public async Task<ResponseService<AgentStateReponse>> ChangeAgentState(ActionCallResquest param)
//        {

//            ResponseService<AgentStateReponse> res = new ResponseService<AgentStateReponse>();
//            try
//            {
//                var changestateRequest = new ChangeStateResquest { extension = param.extension, stateID = param.stateID};
//                _response = await _requestAPI.client.PostAsJsonAsync("agent-states/ChangeStateAgent", changestateRequest);// "agent-states/ChangeStateAgent"
//                res = await _response.Content.ReadAsAsync<ResponseService<AgentStateReponse>>();
//                //ban xuong extension
//                //if (response.status)
//                //{
//                //    res.code = "200";
//                //    res.message = "Success";
//                //} else
//                //{
//                //    res.code = "410";
//                //    res.message = "Unknown error";
//                //}
//            }
//            catch (Exception e)
//            {
//                return new ResponseService<AgentStateReponse>(e);
//            }
//            return res;
//        }
//        public async Task<ResponseService<ListResult<StateList>>> GetListReasonCodes()
//        {
//            // get to
//            var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
//            var user = SessionStore.Get<string>(Constants.KEY_SESSION_EMAIL);
//            var request = new GetReasonResquest
//            {
//                tenant_id = tenant_id,
//                username = user
//            };
//            ResponseService<ListResult<StateList>> res = new ResponseService<ListResult<StateList>>();
//            try
//            {
//               // var changestateRequest = new ChangeStateResquest { extension = param.extension, stateID = param.stateID };
//                _response = await _requestAPI.client.PostAsJsonAsync("agent-states/GetListReasonCodes", request);// "agent-states/ChangeStateAgent"
//                res = await _response.Content.ReadAsAsync<ResponseService<ListResult<StateList>>>();
//            }
//            catch (Exception e)
//            {
//               return new ResponseService<ListResult<StateList>>(e);
//            }
//            return res;
//        }
//        public async Task<ResponseService<UserState>> GetReasonCodes()
//        {
//            // get to
//            var tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
//            var user = SessionStore.Get<string>(Constants.KEY_SESSION_EMAIL);
//            var request = new GetReasonResquest
//            {
//                tenant_id = tenant_id,
//                username = user
//            };
//            ResponseService<UserState> res = new ResponseService<UserState>();
//            try
//            {
//                // var changestateRequest = new ChangeStateResquest { extension = param.extension, stateID = param.stateID };
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API+"get-user-state", request);// "agent-states/ChangeStateAgent"
//                res = await _response.Content.ReadAsAsync<ResponseService<UserState>>();
//            }
//            catch (Exception e)
//            {
//                return new ResponseService<UserState>(e);
//            }
//            return res;
//        }
//    }
//}
