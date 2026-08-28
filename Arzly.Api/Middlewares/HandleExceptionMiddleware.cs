namespace Arzly.Api.Middlewares;

public sealed class HandleExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HandleExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public HandleExceptionMiddleware(
        RequestDelegate next,
        ILogger<HandleExceptionMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning(
                    exception,
                    "The response has already started; the exception response cannot be written");
                throw;
            }

            _logger.LogError(
                exception,
                "Exception: {ExceptionType} - {Message}",
                exception.GetType().Name,
                exception.Message);

            context.Response.Clear();
            context.Response.StatusCode = GetStatusCode(exception);
            //might change later
            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = _environment.IsDevelopment()
                    ? exception.Message
                    : "An error occurred",
                correlationId = context.TraceIdentifier
            });
        }
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        ArgumentException or FormatException => StatusCodes.Status400BadRequest,
        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
        KeyNotFoundException or FileNotFoundException => StatusCodes.Status404NotFound,
        InvalidOperationException => StatusCodes.Status400BadRequest,
        TimeoutException or OperationCanceledException => StatusCodes.Status408RequestTimeout,
        NotImplementedException => StatusCodes.Status501NotImplemented,
        _ => StatusCodes.Status500InternalServerError
    };
}
