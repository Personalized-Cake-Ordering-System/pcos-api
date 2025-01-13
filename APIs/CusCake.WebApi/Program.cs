using CusCake.Application;
using CusCake.Application.GlobalExceptionHandling;
using CusCake.Infrastructures;
using CusCake.WebApi;
using CusCake.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppSettings>() ?? throw new Exception("Null configuration");
builder.Services.AddSingleton(configuration);
builder.Services.AddWebAPIService(configuration);
builder.Services.AddInfrastructuresService(configuration);

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

