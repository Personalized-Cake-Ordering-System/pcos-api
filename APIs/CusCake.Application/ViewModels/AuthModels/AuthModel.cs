using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.AuthModels;

public class AuthRequestModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
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
