using System.ComponentModel.DataAnnotations;
using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.AvailableCakeModels;

public class AvailableCakeCreateModel
{
    public Guid BakeryId { get; set; }
    public double AvailableCakePrice { get; set; } = 0;
    public string AvailableCakeName { get; set; } = default!;
    public string? AvailableCakeDescription { get; set; }
    public string AvailableCakeType { get; set; } = default!;
    public int AvailableCakeQuantity { get; set; } = 0;
    public List<IFormFile> AvailableCakeImage { get; set; } = default!;
}

public class AvailableCakeCreateModelValidator : AbstractValidator<AvailableCakeCreateModel>
{
    public AvailableCakeCreateModelValidator()
    {
        RuleFor(x => x.BakeryId)
            .NotEmpty().WithMessage("Bakery ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Bakery ID must be a valid GUID.");

        RuleFor(x => x.AvailableCakePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

        RuleFor(x => x.AvailableCakeName)
            .NotEmpty().WithMessage("Cake name is required.")
            .MaximumLength(100).WithMessage("Cake name cannot exceed 100 characters.");

        RuleFor(x => x.AvailableCakeDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.AvailableCakeDescription != null);

        RuleFor(x => x.AvailableCakeType)
            .NotEmpty().WithMessage("Cake type is required.")
            .MaximumLength(50).WithMessage("Cake type cannot exceed 50 characters.");

        RuleFor(x => x.AvailableCakeQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0.");

        RuleFor(x => x.AvailableCakeImage)
            .NotNull().WithMessage("Cake images are required.")
            .Must(images => images != null && images.Count != 0).WithMessage("At least one cake image is required.");

        RuleForEach(x => x.AvailableCakeImage)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Each cake image must be a valid image file (jpg, png, jpeg) under 5MB.");
    }
}


public class AvailableCakeUpdateModel : AvailableCakeCreateModel
{
    [Required(ErrorMessage = "Id is require.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "AvailableCakeImageFiles is require.")]
    public List<Guid> AvailableCakeImageFiles { get; set; } = [];
}