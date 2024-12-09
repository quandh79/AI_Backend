using Common.Commons;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Management_AI.Common
{
    public class ResponseFail<T> : IActionResult
    {
        private HttpStatusCode statusCode { get; set; }
        private string message { get; set; }
        private Exception exception { get; set; }
        private ResponseService<T> resService { get; set; }
        public ResponseFail() { }

        public ResponseFail(HttpStatusCode statusCode, string errorMessage) : this(statusCode, errorMessage, null)
        { }

        public ResponseFail(HttpStatusCode statusCode, string errorMessage, Exception exception)
        {
            this.statusCode = statusCode;
            message = errorMessage;
            this.exception = exception;
        }
        public ResponseFail<T> Error(ResponseService<T> resService)
        {
            statusCode = resService.status_code;
            message = resService.message;
            this.resService = resService;
            return this;
        }
        public ResponseFail<T> BadRequest(ResponseService<T> resService)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = resService.message;
            this.resService = resService;
            return this;
        }
        public ResponseFail<T> Unauthorized(ResponseService<T> resService)
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = resService.message;
            this.resService = resService;
            return this;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)statusCode;
            var mes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resService));
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.Body.WriteAsync(mes);
        }
    }
}
