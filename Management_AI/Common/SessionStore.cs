using Management_AI.Config;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Management_AI.Common
{
    public static class SessionStore
    {
        private static IHttpContextAccessor httpContextAccessor = ConfigContainerDJ.CreateInstance<IHttpContextAccessor>();
        public static void Set<T>(string key, T data)
        {
            string serializedData = JsonConvert.SerializeObject(data);
            httpContextAccessor.HttpContext.Session.SetString(key, serializedData);
        }

        public static T Get<T>(string key)
        {
            if (httpContextAccessor.HttpContext.Session == null) return default;
            var data = httpContextAccessor.HttpContext.Session.GetString(key);
            if (null != data)
                return JsonConvert.DeserializeObject<T>(data);
            return default;
        }
    }
}
