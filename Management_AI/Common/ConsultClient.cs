//using Common;
//using Common.Commons;
//using Common.Params.Base;
//using Consul;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Management_AI.KafkaComponents;
//using Management_AI.Config;

//namespace Management_AI.Common
//{
//    public class ConsultClient
//    {
//        private static ConsulClient client = new ConsulClient();
//        public static string projectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

//        public static string port = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_PORT_SERVICE);
//        public static string protocolService = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_PROTOCOL_SERVICE);
//        public static string sourceFabioService = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_SOURCE_FABIO_SERVICE);
//        public static string stateSource = ConfigManager.Get(Constants.CONF_STATE_SOURCE);
//        private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();

//        public static string address = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_ADDRESS_SERVICE);

//        public static async Task<ResponseService<bool>> RegisterService(bool rootRegister = false)
//        {
//            try
//            {
//                _logger.LogInfo(CommonFunc.GetMethodName(new System.Diagnostics.StackTrace()));

//                if (stateSource == Constants.STATE_SOURCE_PRODUCTION && !rootRegister) return new ResponseService<bool>("This is state production!").BadRequest();
//                List<string> listAddressConsul = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_ADDRESS_DISCOVERY_SERVICE).Split(',').ToList();

//                foreach (string addressConsul in listAddressConsul)
//                {
//                    client.Config.Address = new Uri(addressConsul);
//                    var registration = new AgentServiceRegistration()
//                    {
//                        ID = $"{projectName}-{address}:{port}",
//                        Name = projectName,
//                        Address = address,
//                        Port = int.Parse(port),
//                        Tags = new[] { $"urlprefix-/{sourceFabioService} strip=/{sourceFabioService}" },
//                        Check = new AgentServiceCheck()
//                        {
//                            HTTP = $"{protocolService}://{address}:{port}/api/consult/health-check",
//                            Timeout = TimeSpan.FromSeconds(100),
//                            Interval = TimeSpan.FromSeconds(10)
//                        }
//                    };
//                    await client.Agent.ServiceRegister(registration);
//                }

//                return new ResponseService<bool>(true);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return new ResponseService<bool>(ex);
//            }
//        }
//        public static async Task<ResponseService<bool>> UnRegisterService(string id_service = null)
//        {
//            try
//            {
//                _logger.LogInfo(CommonFunc.GetMethodName(new System.Diagnostics.StackTrace()));

//                if (stateSource == Constants.STATE_SOURCE_PRODUCTION) return new ResponseService<bool>("This is state production!").BadRequest();

//                List<string> listAddressConsul = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_ADDRESS_DISCOVERY_SERVICE).Split(',').ToList();

//                foreach (string addressConsul in listAddressConsul)
//                {
//                    client.Config.Address = new Uri(addressConsul);
//                    id_service = string.IsNullOrEmpty(id_service) ? $"{projectName}-{address}:{port}" : id_service;
//                    await client.Agent.ServiceDeregister(id_service);
//                }

//                return new ResponseService<bool>(true);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex);
//                return new ResponseService<bool>(ex);
//            }
//        }
//        public static async Task<ResponseService<bool>> SendTopic(TopicParam param)
//        {
//            try
//            {
//                _logger.LogInfo(CommonFunc.GetMethodName(new System.Diagnostics.StackTrace()));

//                if (stateSource == Constants.STATE_SOURCE_PRODUCTION) return new ResponseService<bool>("This is state production!").BadRequest();

//                ProducerWrapper<object> _producer = new ProducerWrapper<object>();
//                await _producer.CreateMess(param.topic, param.data);

//                return new ResponseService<bool>(true);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ResponseService<bool>(ex);
//            }
//        }

//        public static async Task<ResponseService<string>> GetInfoServiceConsult(string nameService)
//        {
//            try
//            {
//                _logger.LogDebug(CommonFunc.GetMethodName(new System.Diagnostics.StackTrace()));

//                client.Config.Address = new Uri(ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_ADDRESS_DISCOVERY_SERVICE));
//                var services = await client.Agent.Services();
//                foreach (var service in services.Response)
//                {
//                    if (service.Value.Service.Equals(nameService))
//                    {
//                        string baseUrl = $"{service.Value.Address}:{service.Value.Port}";
//                        return new ResponseService<string> { data = baseUrl, status = true };
//                    }
//                }

//                return new ResponseService<string>("No info service");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.Message);
//                return new ResponseService<string>(ex);
//            }
//        }
//    }
//}
