using Common;
using Common.Commons;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Management_AI.Config;

namespace Management_AI.Common
{
    public class RequestAPI
    {
        public HttpClient client;
        private string hostFabio = ConfigManager.Get(Constants.CONF_FABIO, Constants.CONF_HOST_FABIO_SERVICE);

        public RequestAPI() { }
        private void DefaultSetting()
        {
            //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            //handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // rest api by manual
        public RequestAPI(string confBaseUrl)
        {
            DefaultSetting();
            client.BaseAddress = new Uri(ConfigManager.Get(confBaseUrl));
        }

        // rest api by consult
        public RequestAPI(ResponseService<string> confBaseUrl, string preDomainApi = "/api/")
        {
            DefaultSetting();
            client.BaseAddress = new Uri(confBaseUrl.data + preDomainApi);
        }

        // rest api by fabio
        public RequestAPI ToFabio(string sourceFabio, string token = "", string preDomainApi = "/api/")
        {
            DefaultSetting();
            client.BaseAddress = new Uri(hostFabio + sourceFabio + preDomainApi);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }
    }
    public class MiddlewareHandler : DelegatingHandler
    {
        public MiddlewareHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
        {
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            //var requestDate = request.Headers.Date;
            // do something with the date ...

            // handle respose
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string mess = string.Format("{0} {1} {2}", response.ReasonPhrase.ToString(), ((int)response.StatusCode).ToString(), response.RequestMessage.RequestUri);
               // await CommonFunc.LogErrorToKafka(mess);
            }

            return response;
        }
    }
}
