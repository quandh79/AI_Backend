//using Common;
//using Common.Commons;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Management_AI.Common;
//using Management_AI.Services.Models;
//using Management_AI.Config;

//namespace Management_AI.RestApi
//{
//    public class HotlineAPI
//    {
//        private RequestAPI _requestAPI;
//        private HttpResponseMessage _response;
//        private readonly string PATH_PRE_API = "hotline/";
//        private readonly string secret_key = ConfigManager.Get(Constants.CONF_API_SECRET_KEY);

//        public HotlineAPI()
//        {
//            //Test local
//            //_requestAPI = new RequestAPI("LOCAL_HOST");
//            _requestAPI = new RequestAPI().ToFabio(ConfigManager.Get(Constants.CONF_SOURCE_USER_FABIO));
//            _requestAPI.client.DefaultRequestHeaders.Add("API_SECRET_KEY", secret_key);
//        }

//        public async Task<ResponseService<HotlineInfo>> GetHotline(string hotlineNumber)
//        {
//            ResponseService<HotlineInfo> res = new ResponseService<HotlineInfo>();
//            try
//            {
//                //Test local
//                //_response = await _requestAPI.client.PostAsJsonAsync("api/hotline/get-by-hotline-number", new {item = hotlineNumber});
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "get-by-hotline-number", new { item = hotlineNumber });
//                res = await _response.Content.ReadAsAsync<ResponseService<HotlineInfo>>();
//            }
//            catch (Exception e)
//            {
//                throw;
//            }
//            return res;
//        }
//    }
//}
