using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Arzly.Api.Infrastructure.Health;

public sealed class ExternalServicesConfigurationHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public ExternalServicesConfigurationHealthCheck(IConfiguration configuration) =>
        _configuration = configuration;

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var errors = new List<string>();
        Require(errors, "CloudflareR2:AccessKey");
        Require(errors, "CloudflareR2:SecretKey");
        Require(errors, "CloudflareR2:BucketName");
        RequireAbsoluteUri(errors, "CloudflareR2:ServiceURL");
        RequireAbsoluteUri(errors, "CloudflareR2:PublicUrlBase");
        Require(errors, "GoogleMaps:ApiKey");
        Require(errors, "Email:Host");
        Require(errors, "Email:Username");
        Require(errors, "Email:Password");
        Require(errors, "Authentication:Google:ClientId");
        Require(errors, "Authentication:Google:ClientSecret");

        if (!int.TryParse(_configuration["Email:Port"], out var emailPort) || emailPort is < 1 or > 65535)
            errors.Add("Email:Port must be between 1 and 65535");

        return Task.FromResult(errors.Count == 0
            ? HealthCheckResult.Healthy("External-service configuration is present and valid")
            : HealthCheckResult.Unhealthy(string.Join("; ", errors)));
    }

    private void Require(List<string> errors, string key)
    {
        if (string.IsNullOrWhiteSpace(_configuration[key]))
            errors.Add($"{key} is missing");
    }

    private void RequireAbsoluteUri(List<string> errors, string key)
    {
        var value = _configuration[key];
        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri) || uri.Scheme is not ("http" or "https"))
            errors.Add($"{key} must be an absolute HTTP(S) URL");
    }
}
