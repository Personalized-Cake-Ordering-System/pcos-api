using System.ComponentModel.DataAnnotations;

namespace CusCake.Application.ViewModels.AdminModels;

public class AdminCreateModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = default!;
}