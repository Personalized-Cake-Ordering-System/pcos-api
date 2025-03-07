using CusCake.Application.GlobalExceptionHandling;
using CusCake.Application.Services.IServices;
using CusCake.Application;
using CusCake.WebApi.Middlewares;
using CusCake.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scrutor;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using CusCake.Application.ViewModels;
namespace CusCake.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddScoped<IClaimsService, ClaimService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy
                    => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                }
            );



            List<Assembly> assemblies = new List<Assembly>
                     {
                         CusCake.Application.AssemblyReference.Assembly,
                         CusCake.Infrastructures.AssemblyReference.Assembly
                     };
            services.Scan(scan => scan
             .FromAssemblies(CusCake.Infrastructures.AssemblyReference.Assembly,
             CusCake.Application.AssemblyReference.Assembly,
             AssemblyReference.Assembly)
             .AddClasses(classes => classes.InNamespaces("CusCake.Application", "CusCake.Infrastructures", "CusCake.WebApi"))
             .UsingRegistrationStrategy(RegistrationStrategy.Skip)
             .AsMatchingInterface()
             .WithScopedLifetime());


            services.AddValidatorsFromAssemblies(assemblies: assemblies);
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.JWTOptions.Issuer,
                        ValidAudience = appSettings.JWTOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWTOptions.SecretKey)),
                        ClockSkew = TimeSpan.FromSeconds(1)
                    };
                });


            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            //services.AddValidatorsFromAssemblies(assemblies: assemblies);
            services.AddSingleton<GlobalErrorHandlingMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                        .ToList();

                    var response = ResponseModel<object, object>.Fail(errors: errors);
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
