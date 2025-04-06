using CusCake.Application;
using CusCake.Application.GlobalExceptionHandling;
using CusCake.Application.SignalR;
using CusCake.Infrastructures;
using CusCake.WebApi;
using CusCake.WebApi.Middlewares;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppSettings>() ?? throw new Exception("Null configuration");
builder.Services.AddSingleton(configuration);
builder.Services.AddWebAPIService(configuration);
builder.Services.AddInfrastructuresService(configuration);
builder.Logging.AddConsole();  // Ghi log vào Console (mặc định)
builder.Logging.AddDebug();    // Ghi log vào Debug

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard("/hangfire");

app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<SignalRConnection>("/signalR-hub");

app.MapControllers();

app.Run();

