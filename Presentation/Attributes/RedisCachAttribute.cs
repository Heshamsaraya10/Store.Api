using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System.Text;

namespace Presentation.Attributes
{
    public class RedisCachAttribute(int durationInSecounds = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachservice = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CachService;

            var key = GenerateCachKey(context.HttpContext.Request);

            var value = await cachservice.GetAsync(key);

            if (value is not null)
            {
                context.Result = new ContentResult
                {
                    Content = value,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var ActionExecutedContext = await next.Invoke();

            if (ActionExecutedContext.Result is OkObjectResult result)
                await cachservice.SetAsync(key, result.Value, TimeSpan.FromSeconds(durationInSecounds));
        }

        private static string GenerateCachKey(HttpRequest httpRequest)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(httpRequest.Path).Append("? ");

            foreach (var item in httpRequest.Query.OrderBy(i => i.Key))
                 builder.Append($"{item.Key} = {item.Value}");


            return builder.ToString().TrimEnd('&');

        }
    }
}
