//using Common;
//using Common.Commons;
//using System.Threading.Tasks;
//using Management_AI.Common;
//using Management_AI.Config;
//using Management_AI.Models.Common;
//using System.Net.Http;
//using System;

//namespace Management_AI.RestApi
//{
//    public class AuthenAPI
//    {
//        private RequestAPI _requestAPI;
//        private HttpResponseMessage _response;
//        private readonly string PATH_PRE_API = "auth/";

//        public AuthenAPI(string token)
//        {
//            _requestAPI = new RequestAPI().ToFabio(ConfigManager.Get(Constants.CONF_SOURCE_USER_FABIO), token);
//        }
//        public async Task<BaseResponse<InfoAuthenModel>> ValidateToken()
//        {
//            BaseResponse<InfoAuthenModel> resAuthen = new BaseResponse<InfoAuthenModel>();
//            try
//            {
//                _response = await _requestAPI.client.PostAsJsonAsync(PATH_PRE_API + "check-authentication", string.Empty);
//                if (_response.IsSuccessStatusCode)
//                {
//                    resAuthen = await _response.Content.ReadAsAsync<BaseResponse<InfoAuthenModel>>();
//                    resAuthen.success = true;
//                }
//                else
//                {
//                    resAuthen.success = false;
//                    resAuthen.mess = "Authen fail!";
//                }
//                resAuthen.statusCode = _response.StatusCode;

//            }
//            catch (Exception ex)
//            {
//                resAuthen.success = false;
//                resAuthen.mess = ex.Message;
//            }
//            return resAuthen;
//        }
//    }
//}
