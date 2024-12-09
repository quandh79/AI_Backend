//using Common;
//using Common.Commons;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Management_AI.Services.Models;
//using Management_AI.Config;
//using Management_AI.Common;

//namespace Management_AI.RestApi
//{
//    public class CallLogAPI
//    {
//        private RequestAPI _requestAPI;
//        private HttpResponseMessage _response;
//        private readonly string PATH_PRE_API = "call-logs/";
//        private readonly string secret_key = ConfigManager.Get(Constants.CONF_API_SECRET_KEY);

//        public CallLogAPI()
//        {
//            //Test local
//            _requestAPI = new RequestAPI("LOCAL_HOST");
//            //  _requestAPI = new RequestAPI().ToFabio(ConfigManager.Get(Constants.CONF_SOURCE_DATA_STORAGE_FABIO));
//            _requestAPI.client.DefaultRequestHeaders.Add("API_SECRET_KEY", secret_key);
//        }

//        public async Task<ResponseService<CallLogInfo>> GetCallLogByUniqueId(string uniqueId)
//        {
//            ResponseService<CallLogInfo> res = new ResponseService<CallLogInfo>();
//            try
//            {
//                //Test local
//                //_response = await _requestAPI.client.PostAsJsonAsync("api/hotline/get-by-hotline-number", new {item = hotlineNumber});
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-by-unique-id", new { item = uniqueId });
//                res = await _response.Content.ReadAsAsync<ResponseService<CallLogInfo>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }

//        public async Task<ResponseService<CallLogInfo>> GetCallTalkingByExtension(string extension)
//        {
//            ResponseService<CallLogInfo> res = new ResponseService<CallLogInfo>();
//            try
//            {
//                //Test local
//                //_response = await _requestAPI.client.PostAsJsonAsync("api/hotline/get-by-hotline-number", new {item = hotlineNumber});
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-call-talking-by-extension", new { item = extension });
//                res = await _response.Content.ReadAsAsync<ResponseService<CallLogInfo>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }
//        public async Task<ResponseService<CallLogInfo>> GetCallIdNotOverYet(ParamGetCallLogByExtensionModel model)
//        {
//            ResponseService<CallLogInfo> res = new ResponseService<CallLogInfo>();
//            try
//            {
//                //Test local
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-callid-not-over-yet", model);
//                res = await _response.Content.ReadAsAsync<ResponseService<CallLogInfo>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }
//    }
//}
