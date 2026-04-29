using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Arzly.Api.Filters.ExceptionFilters
{
    public class HandleExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<HandleExceptionFilter> _logger;
        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {

            _logger.LogInformation("{Filter}.{Method} - Before",
                GetType().Name, nameof(OnExceptionAsync));
            var env = context.HttpContext.RequestServices.GetService<IHostEnvironment>();

            _logger.LogError("Exception: {ExceptionType} - {Message}",
                context.Exception.GetType().Name,
                context.Exception.Message);

            var statusCode = context.Exception switch
            {
                ArgumentException or ArgumentNullException or FormatException => 400,
                UnauthorizedAccessException => 401,
                KeyNotFoundException or FileNotFoundException => 404,
                InvalidOperationException => 400,
                TimeoutException or OperationCanceledException => 408,
                NotImplementedException => 501,
                _ => 500
            };
            //might change when hosting
            var response = new
            {
                success = false,
                message = env?.IsDevelopment() == true
                    ? context.Exception.Message
                    : "An error occurred"
            };

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = statusCode;

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
            _logger.LogInformation("{Filter}.{Method} - After",
               GetType().Name, nameof(OnExceptionAsync));
            context.ExceptionHandled = true;//no short-circuit
            await Task.CompletedTask;
        }

    }
}
