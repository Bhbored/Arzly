using Arzly.Api.Helpers;
using Arzly.Api.Hubs;
using Microsoft.OpenApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{

    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});
builder.Services.AddSignalR();
builder.Services.RegisterDependencies(builder.Configuration, builder.Environment);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Arzly", Version = "1.0" });

    //options.SwaggerDoc("v2", new OpenApiInfo() { Title = "Arzly", Version = "2.0" });
});

builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
    options.Preload = true;
});
var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseCors("Blazor");

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
    //options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
});
app.UseHttpLogging();
app.MapControllers();
app.MapHub<ChatHub>("/arzly/v1.0/hubs/chat");
app.Run();
