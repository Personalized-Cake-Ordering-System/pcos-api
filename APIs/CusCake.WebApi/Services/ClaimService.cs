using CusCake.Application.Services.IServices;
using System.Security.Claims;

namespace CusCake.WebApi.Services;

public class ClaimService : IClaimsService
{
    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
        GetCurrentUser = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);
    }
    public Guid GetCurrentUser { get; }
}
