using Arzly.Api.Application.Services;
using Arzly.Api.Filters.ExceptionFilters;
using Arzly.Api.Helpers;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{

    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;
});

builder.Services.RegisterDependencies(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers(controller =>
{

    controller.Filters.Add<HandleExceptionFilter>();//global filter

})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;//for built-in modelBinding i did a custom filter for that
    });
builder.Services.AddOpenApi();

var app = builder.Build();


//temp soi don't migrate
//using var scope = app.Services.CreateScope();
//var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//await ListingOwnedSeedHelper.SeedAsync(context);
// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
