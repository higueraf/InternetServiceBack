using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;


namespace InternetServiceBack.Filters
{
    public class UserSessionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            MethodsHelper MethodsHep = new MethodsHelper();
            var AuthorizationH = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (AuthorizationH != null)
            {
                var MessageError = MethodsHep.ValidateTokenSesion(AuthorizationH);
                if (MessageError == null)
                {
                    await next();
                } else
                {
                    _ = context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new GenericResponseDto<dynamic>
                    {
                        statusCode = 401,
                        message = MessageError,
                    }));
                }
            } else
            {
                _ = context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new GenericResponseDto<dynamic>
                {
                    statusCode = 401,
                    message = MessageHelper.TokenSesionErrorNotParams,
                }));
            }
            
        }
    }
}
