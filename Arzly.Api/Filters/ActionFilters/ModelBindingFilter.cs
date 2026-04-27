using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Arzly.Api.Filters.ActionFilters
{
    public class ModelBindingFilter : IAsyncActionFilter
    {
        private readonly ILogger<ModelBindingFilter> _logger;
        private readonly Type _controllerType;
        private readonly string _parameterName;

        public ModelBindingFilter(ILogger<ModelBindingFilter> logger, Type controllerType, string parameterName)
        {
            _logger = logger;
            _controllerType = controllerType;
            _parameterName = parameterName;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("{FilterName}.{Method} method - before",
                nameof(ModelBindingFilter), nameof(OnActionExecutionAsync));

            if (context.Controller.GetType() == _controllerType)
            {
                if (!context.ModelState.IsValid)
                {
                    string error = string.Join("\n", context.ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));

                    var requestObject = context.ActionArguments[_parameterName];

                    context.Result = ((ControllerBase)context.Controller).BadRequest(
                        $"Couldn't process {_parameterName}: {requestObject} because of the following: \n {error}");

                    return;
                }
            }

            await next();

            _logger.LogInformation("{FilterName}.{Method} method - after",
                nameof(ModelBindingFilter), nameof(OnActionExecutionAsync));
        }
    }
}
