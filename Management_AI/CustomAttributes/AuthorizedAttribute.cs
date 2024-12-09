using Common;
using Common.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using static Common.Constants;
using Management_AI.Config;
using Management_AI.Common;
using ILogger = Common.Commons.ILogger;


namespace Management_AI.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizedAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();
        private AUTHOR authorDefault = AUTHOR.TOKEN_OR_KEY;

        public AuthorizedAttribute()
        {
            authorDefault = AUTHOR.TOKEN_OR_KEY;
        }

        public AuthorizedAttribute(AUTHOR authorType)
        {
            authorDefault = authorType;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!await Authorize(context.HttpContext))
            {
                ResponseService<string> response = new ResponseService<string>("User is invalid!");
                context.HttpContext.Response.StatusCode = new UnauthorizedResult().StatusCode;
                var mes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.Body.WriteAsync(mes);
                context.Result = new UnauthorizedResult();
            }

            return;
        }

        private async Task<bool> Authorize(HttpContext actionContext)
        {
            try
            {
                var header = actionContext.Request.Headers;

                var paramAuthor = header[HeaderNames.Authorization].ToString();
                string paramSecretKey = header.ContainsKey(CONF_API_SECRET_KEY) ? header[CONF_API_SECRET_KEY].ToString() : null;
                string token = actionContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                // validate token
                if (authorDefault == AUTHOR.TOKEN)
                {
                    return CommonFunc.ValidateToken(token);
                }
                else if (authorDefault == AUTHOR.SECRET_KEY) // author secret, key
                {
                    if (paramSecretKey == ConfigManager.Get(CONF_API_SECRET_KEY)) return true;
                }
                else if (authorDefault == AUTHOR.TOKEN_OR_KEY)
                {
                    if (paramSecretKey == ConfigManager.Get(CONF_API_SECRET_KEY)) return true;
                    return CommonFunc.ValidateToken(token);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }
    }
}
