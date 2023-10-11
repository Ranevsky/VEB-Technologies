using ExceptionLibrary.Handlers.AspNetCore.Extensions;
using HealthCheckLibrary.AspNetCore.Routing.Extensions;
using Identity.Presentation;
using LoggerLibrary.AspNetCore.Extensions;
using LoggerLibrary.Serilog.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.UseSerilog();

var configuration = builder.Configuration;
configuration
    .AddEnvironmentVariables()
    .SetBasePath(builder.Environment.ContentRootPath);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseTraceIdLog();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseRouting();

app.UseCors(x => { x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.AddAllMapHealthChecks();
    endpoints.AddDbMapHealthChecks();
});

app.Run();