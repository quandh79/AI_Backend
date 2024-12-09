//using Common.Commons;
//using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Caching.StackExchangeRedis;
//using Newtonsoft.Json;
//using StackExchange.Redis;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reflection.Metadata;
//using Common;
//using Management_AI.Config;

//namespace Management_AI.Common
//{
//    public class Redis<T>
//    {
//        public static readonly Lazy<ConnectionMultiplexer> LazyConnection;
//        public static RedisCache _redisconnection;
//        private static List<string> endPointsRedis = ConfigManager.Get("HOST_REDIS").Split(",").ToList();
//        private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();
//        private static ConfigurationOptions configurationOptions;

//        static Redis()
//        {
//            configurationOptions = new ConfigurationOptions
//            {
//                AbortOnConnectFail = false,
//                Password = ConfigManager.Get("PASS_REDIS")
//            };

//            endPointsRedis.ForEach(host => configurationOptions.EndPoints.Add(host));
//            LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
//            {
//                var conn = ConnectionMultiplexer.Connect(configurationOptions);
//                conn.ConnectionFailed += OnConnectionFailed;
//                conn.ConnectionRestored += OnConnectionRestored;
//                return conn;
//            });
//            var options = new RedisCacheOptions();
//            options.ConfigurationOptions = configurationOptions;
//            _redisconnection = new RedisCache(options);
//        }

//        public static ConnectionMultiplexer Connection => LazyConnection.Value;

//        public static IDatabase RedisCache => Connection.GetDatabase();

//        public async static Task<bool> SetAsync(string key, T value, string prefix)
//        {
//            try
//            {
//                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

//                var options = new DistributedCacheEntryOptions();
//                if (prefix == Constants.PREFIX_REDIS_JTAPI)
//                {
//                    options = new DistributedCacheEntryOptions();
//                }
//                else if (prefix == Constants.PREFIX_REDIS_CALL)
//                {
//                    options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
//                }
//                else
//                {
//                    options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));
//                }

//                await _redisconnection.SetAsync(prefix + key, data, options);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return false;
//            }
//            return true;
//        }

//        public async static Task<bool> SetAsync(string key, T value)
//        {
//            try
//            {
//                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
//                var options = new DistributedCacheEntryOptions()
//                .SetSlidingExpiration(TimeSpan.FromDays(10));
//                await _redisconnection.SetAsync(key, data, options);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return false;
//            }
//            return true;
//        }
//        public async static Task<T> GetAsync(string key, string prefix)
//        {
//            T result = default;
//            try
//            {
//                byte[] data = await _redisconnection.GetAsync(prefix + key);
//                if (data == null) return result;
//                string dataConvert = Encoding.UTF8.GetString(data);
//                result = JsonConvert.DeserializeObject<T>(dataConvert);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return result;
//            }
//            return result;
//        }
//        public async static Task<T> GetAsync(string key)
//        {
//            T result = default;
//            try
//            {
//                byte[] data = await _redisconnection.GetAsync(key);
//                if (data == null) return result;
//                string dataConvert = Encoding.UTF8.GetString(data);
//                result = JsonConvert.DeserializeObject<T>(dataConvert);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return result;
//            }
//            return result;
//        }
//        public async static Task<bool> CheckExistsAsync(string key) => await RedisCache.KeyExistsAsync(key);
//        public async static Task<bool> CheckExistsAsync(string key, string prefix) => await RedisCache.KeyExistsAsync(prefix + key);
//        public async static Task<bool> RemoveKeyAsync(string key) => await RedisCache.KeyDeleteAsync(key);
//        public async static Task<bool> RemoveKeyAsync(string key, string prefix) => await RedisCache.KeyDeleteAsync(prefix + key);

//        public static async Task<Dictionary<string, T>> GetAll(List<string> agents = null)
//        {
//            Dictionary<string, T> dicKeyValue = new Dictionary<string, T>();
//            string[] keysArr = GetKeysServer();

//            if (agents != null) keysArr = keysArr.Where(key => agents.Any(agent => agent == key)).ToArray();

//            foreach (var key in keysArr)
//            {
//                dicKeyValue.Add(key, await GetFromDB(key));
//            }

//            return dicKeyValue;
//        }

//        #region logic handle
//        private static async Task<T> GetFromDB(string key)
//        {
//            T result = default;
//            try
//            {
//                byte[] data = await _redisconnection.GetAsync(key);
//                string dataConvert = Encoding.UTF8.GetString(data);
//                result = JsonConvert.DeserializeObject<T>(dataConvert);
//                return result;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return result;
//            }

//        }
//        private static string[] GetKeysServer()
//        {

//            foreach (var endpoint in configurationOptions.EndPoints)
//            {
//                try
//                {
//                    var keys = Connection.GetServer(endpoint).Keys(pattern: "*");
//                    string[] keysArr = keys.Select(key => (string)key).ToArray();
//                    return keysArr;
//                }
//                catch (Exception ex)
//                {
//                    continue;
//                }
//            }
//            return null;
//        }
//        private static void OnConnectionFailed(object sender, ConnectionFailedEventArgs args)
//        {
//            lock (configurationOptions)
//            {
//                if (configurationOptions.EndPoints.Contains(args.EndPoint))
//                {
//                    configurationOptions.EndPoints.Remove(args.EndPoint);
//                }
//            }
//        }

//        private static void OnConnectionRestored(object sender, ConnectionFailedEventArgs args)
//        {
//            lock (configurationOptions)
//            {
//                if (!configurationOptions.EndPoints.Contains(args.EndPoint))
//                {
//                    configurationOptions.EndPoints.Add(args.EndPoint);
//                }
//            }
//        }

//        #endregion
//    }
//}
