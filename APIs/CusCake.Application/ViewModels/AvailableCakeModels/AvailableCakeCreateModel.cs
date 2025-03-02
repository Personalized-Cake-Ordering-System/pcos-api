using System.ComponentModel.DataAnnotations;
using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.AvailableCakeModels;


public class AvailableCakeBaseActionModel
{
    public Guid BakeryId { get; set; }
    public double AvailableCakePrice { get; set; } = 0;
    public string AvailableCakeName { get; set; } = default!;
    public string? AvailableCakeDescription { get; set; }
    public string AvailableCakeType { get; set; } = default!;
    public int AvailableCakeQuantity { get; set; } = 0;
    public List<IFormFile>? AvailableCakeFileImages { get; set; } = default!;

}


public class AvailableCakeBaseActionModelValidator : AbstractValidator<AvailableCakeBaseActionModel>
{
    public AvailableCakeBaseActionModelValidator()
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

        RuleFor(x => x.AvailableCakeFileImages)
            .Null().WithMessage("Images can be null.");


        RuleForEach(x => x.AvailableCakeFileImages)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Each cake image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.AvailableCakeFileImages != null && x.AvailableCakeFileImages.Count != 0);
    }
}

public class AvailableCakeCreateModel : AvailableCakeBaseActionModel
{
    public IFormFile AvailableCakeFileImage { get; set; } = default!;
}

public class AvailableCakeCreateModelValidator : AbstractValidator<AvailableCakeCreateModel>
{
    public AvailableCakeCreateModelValidator()
    {

        Include(new AvailableCakeBaseActionModelValidator());

        RuleFor(x => x.AvailableCakeFileImage)
            .NotNull().WithMessage("Main image is required.")
            .Must(ValidationUtils.BeAValidImage).WithMessage("Main image must be a valid image file (jpg, png, jpeg) under 5MB.");
    }
}


public class AvailableCakeUpdateModel : AvailableCakeBaseActionModel
{
    [Required(ErrorMessage = "Id is require.")]
    public Guid Id { get; set; }
    public IFormFile? AvailableCakeFileImage { get; set; }

    public List<Guid>? DeleteImageFileIds { get; set; } = [];
}

public class AvailableCakeUpdateModelValidator : AbstractValidator<AvailableCakeUpdateModel>
{
    public AvailableCakeUpdateModelValidator()
    {
        Include(new AvailableCakeBaseActionModelValidator());

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(x => x.AvailableCakeFileImage)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Main image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.AvailableCakeFileImage != null);

        RuleFor(x => x.DeleteImageFileIds)
            .Must(ids => ids == null || ids.All(id => id != Guid.Empty))
            .WithMessage("DeleteImageFileIds cannot contain empty GUIDs.");
    }
}