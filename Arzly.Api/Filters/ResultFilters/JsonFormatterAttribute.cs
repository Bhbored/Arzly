using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arzly.Api.Filters.ResultFilters
{
    public class JsonFormatterAttribute : Attribute
    {
        public bool UsePascalCase { get; set; }
    }

    public class ConditionalJsonResultFilter : IAsyncResultFilter  
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var attribute = context.Controller.GetType()
                .GetCustomAttribute<JsonFormatterAttribute>();

            if (context.Result is ObjectResult objectResult && objectResult.Value != null)
            {
                var options = new JsonSerializerOptions();

                if (attribute?.UsePascalCase == true)
                {
                    options.PropertyNamingPolicy = null;
                    options.Converters.Add(new JsonStringEnumConverter());
                }
                else
                {
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                }

                var json = JsonSerializer.Serialize(objectResult.Value, options);
                context.Result = new ContentResult
                {
                    Content = json,
                    ContentType = "application/json",
                    StatusCode = objectResult.StatusCode
                };
            }

            await next();
        }
    }
}