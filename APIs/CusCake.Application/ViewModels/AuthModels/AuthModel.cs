using System.ComponentModel.DataAnnotations;

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
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}


public class RevokeModel
{
    [Required(ErrorMessage = "Old token is required")]
    public string OldToken { get; set; } = default!;
}
