using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.CakePartModels;


public class CakePartCreateModel
{

    public string PartName { get; set; } = default!;

    public double PartPrice { get; set; } = 0;

    public string PartType { get; set; } = default!;

    public string? PartDescription { get; set; }

    public bool IsDefault { get; set; } = false;

    public IFormFile? Image { get; set; }

}

public class CakePartCreateModelValidator : AbstractValidator<CakePartCreateModel>
{
    public CakePartCreateModelValidator()
    {
        RuleFor(x => x.PartName)
            .NotEmpty().WithMessage("Part name is required.")
            .MaximumLength(100).WithMessage("Part name cannot exceed 100 characters.");

        RuleFor(x => x.PartPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Part price must be greater than or equal to 0.");

        RuleFor(x => x.PartType)
            .NotEmpty().WithMessage("Part type is required.")
            .MaximumLength(50).WithMessage("Part type cannot exceed 50 characters.");

        RuleFor(x => x.PartDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.PartDescription != null);


        RuleFor(x => x.Image)
            .Must(ValidationUtils.BeAValidImage!)
            .WithMessage("Image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.Image != null);
    }
}

public class CakePartUpdateModel : CakePartCreateModel
{
    public Guid Id { get; set; }
}