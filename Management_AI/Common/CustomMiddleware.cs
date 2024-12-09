namespace Management_AI.Common
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            // Your HttpContext related task is in here.
            //AsteriskConnect.ConnectAsterisk(context);
            await _requestDelegate(context);
        }
    }
}
