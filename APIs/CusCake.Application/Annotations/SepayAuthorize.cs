using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CusCake.Application.Annotations;

public class SepayAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var appSettings = context.HttpContext.RequestServices.GetRequiredService<AppSettings>();

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeaders))
        {
            throw new UnauthorizedAccessException("Invalid request header!");
        }

        var token = authorizationHeaders.FirstOrDefault()?.Split(' ').Last();

        if (string.IsNullOrEmpty(token) || !appSettings.SepayOptions.ApiKey.Equals(token))
        {
            throw new UnauthorizedAccessException("Invalid Api Key!");
        }
    }
}