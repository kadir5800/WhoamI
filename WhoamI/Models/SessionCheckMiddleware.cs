using System.Security.Policy;

namespace WhoamI_Web.Models
{
    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;
       
        public SessionCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string _areaName = context.Request.Path.Value
       .Trim('/')
       .Split('/', StringSplitOptions.RemoveEmptyEntries)
       .FirstOrDefault();
            if (!string.IsNullOrEmpty(_areaName))
            {
                _areaName = _areaName.ToLower();

                if (_areaName == "dashboard")
                {
                    string allowedPagePath = $"/{_areaName}/Login";

                    if (context.Request.Path == allowedPagePath)
                    {
                        await _next(context);
                        return;
                    }

                    if (context.Session.GetInt32("isConnected") != 1)
                    {
                        context.Response.Redirect(allowedPagePath);
                        return;
                    }
                    await _next(context);
                }
            }
           

            await _next(context);
        }

    }
}
