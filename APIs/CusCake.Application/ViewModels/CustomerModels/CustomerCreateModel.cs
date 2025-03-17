
using System.Text.Json.Serialization;
using FluentValidation;

namespace CusCake.Application.ViewModels.CustomerModels;

public class CustomerBaseActionModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = default!;
    [JsonPropertyName("address")]
    public string? Address { get; set; } = default!;

    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; } = default!;

    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; } = default!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;
}

public class CustomerBaseActionModelValidator : AbstractValidator<CustomerBaseActionModel>
{
    public CustomerBaseActionModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(30).WithMessage("Name cannot exceed 30 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password name is required.")
            .MaximumLength(30)
            .MinimumLength(8)
            .WithMessage("Password must be between 8 - 30.");

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

        RuleFor(x => x.Latitude)
        .NotEmpty().WithMessage("Latitude must not null if Address is not null.")
        .When(x => !string.IsNullOrEmpty(x.Address));

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("Longitude must not null if Address is not null.")
            .When(x => !string.IsNullOrEmpty(x.Address));
    }
}
public class CustomerCreateModel : CustomerBaseActionModel
{

    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

}


public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>
{
    public CustomerCreateModelValidator()
    {

        Include(new CustomerBaseActionModelValidator());

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

    }
}

public class CustomerUpdateModel : CustomerBaseActionModel
{
}
