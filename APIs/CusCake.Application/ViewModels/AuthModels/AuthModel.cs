using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CusCake.Domain.Entities;

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

public class AuthViewModel
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

    [JsonPropertyName("role")]
    public string Role { get; set; } = default!;

    [JsonPropertyName("entity_id")]
    public Guid EntityId { get; set; } = default!;
    [JsonPropertyName("entity")]
    public object Entity { get; set; } = default!;
}

public class AuthCreateModel
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
    public Guid EntityId { get; set; } = default!;

}

public class AuthUpdateModel
{
    public Guid EntityId { get; set; } = default!;
    public string Password { get; set; } = default!;

}