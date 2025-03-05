
using System.Text.Json.Serialization;
using FluentValidation;

namespace CusCake.Application.ViewModels.CustomerModels;

public class CustomerCreateModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = default!;
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

}


public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>
{
    public CustomerCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(30).WithMessage("Name cannot exceed 30 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password name is required.")
            .MaximumLength(30)
            .MinimumLength(8)
            .WithMessage("Password cannot exceed 30 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+84|0)(3[2-9]|5[2-9]|7[0|6-9]|8[1-9]|9[0-9])\d{7}$")
            .WithMessage("Phone number must be valid (9-15 digits, optional + prefix).");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(50).WithMessage("Password cannot exceed 50 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
    }
}