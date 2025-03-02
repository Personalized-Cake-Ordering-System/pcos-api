using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.CakeDecorationModels;


public class CakeDecorationCreateModel
{

    public string DecorationName { get; set; } = default!;

    public double DecorationPrice { get; set; } = 0;

    public string DecorationType { get; set; } = default!;

    public string? DecorationDescription { get; set; }

    public string? DecorationColor { get; set; }

    public bool IsDefault { get; set; } = false;

    public IFormFile? Image { get; set; }

}

public class CakeDecorationCreateModelValidator : AbstractValidator<CakeDecorationCreateModel>
{
    public CakeDecorationCreateModelValidator()
    {
        RuleFor(x => x.DecorationName)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Extra name cannot exceed 100 characters.");

        RuleFor(x => x.DecorationPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

        RuleFor(x => x.DecorationType)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(50).WithMessage("Type cannot exceed 50 characters.");

        RuleFor(x => x.DecorationDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.DecorationDescription != null);


        RuleFor(x => x.Image)
            .Must(ValidationUtils.BeAValidImage!)
            .WithMessage("Image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.Image != null);
    }
}

public class CakeDecorationUpdateModel : CakeDecorationCreateModel
{
    public Guid Id { get; set; }
}