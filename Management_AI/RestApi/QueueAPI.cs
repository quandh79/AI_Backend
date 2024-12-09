//using Common;
//using Common.Commons;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Management_AI.Common;
//using Management_AI.Config;
//using Management_AI.Services.Models;

//namespace Management_AI.RestApi
//{
//    public class QueueAPI
//    {
//        private RequestAPI _requestAPI;
//        private HttpResponseMessage _response;
//        private readonly string PATH_PRE_API = "queue/";
//        private readonly string secret_key = ConfigManager.Get(Constants.CONF_API_SECRET_KEY);

//        public QueueAPI()
//        {
//            _requestAPI = new RequestAPI().ToFabio(ConfigManager.Get(Constants.CONF_SOURCE_USER_FABIO));
//            _requestAPI.client.DefaultRequestHeaders.Add("API_SECRET_KEY", secret_key);
//        }

//        public async Task<ResponseService<QueueInfo>> GetQueue(string queueID)
//        {
//            ResponseService<QueueInfo> res = new ResponseService<QueueInfo>();
//            try
//            {
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-queue-by-id", new { item = queueID});
//                res = await _response.Content.ReadAsAsync<ResponseService<QueueInfo>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }
//    }
//}
