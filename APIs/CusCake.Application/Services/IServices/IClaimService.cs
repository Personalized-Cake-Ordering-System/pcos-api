namespace CusCake.Application.Services.IServices
{
    public interface IClaimsService
    {
        public Guid GetCurrentUser { get; }
        public string GetCurrentUserRole { get; }
    }
}
