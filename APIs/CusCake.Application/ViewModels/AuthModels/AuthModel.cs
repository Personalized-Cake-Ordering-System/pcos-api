using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.AuthModels;

public class AuthRequestModel
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = default!;
}

public class AuthResponseModel
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}

public class RevokeModel
{
    [Required(ErrorMessage = "Old token is required")]
    [JsonPropertyName("old_token")]
    public string OldToken { get; set; } = default!;
}
