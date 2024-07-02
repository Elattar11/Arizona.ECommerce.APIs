using Arizona.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Arizona.APIs.Helpers
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLifeInSeconds;

        public CashedAttribute(int timeToLifeInSeconds)
        {
            _timeToLifeInSeconds = timeToLifeInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCashService =  context.HttpContext.RequestServices.GetRequiredService<IResponseCashService>();

            var cashKey = GenerateCashKeyFromRequest(context.HttpContext.Request);

            var response = await responseCashService.GetCashedResponseAsync(cashKey);

            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = result;

                return;
            } //Response is not cashed

            var executedActionContext =  await next.Invoke(); //Will Execute next action filter or action itself

            if (executedActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {
                await responseCashService.CashResponseAsync(cashKey, okObjectResult.Value , TimeSpan.FromSeconds(_timeToLifeInSeconds));
            }

        }

        private string GenerateCashKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();

        }
    }
}
