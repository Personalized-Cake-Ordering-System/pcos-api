using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.CakeExtraModels;


public class CakeExtraCreateModel
{

    public string ExtraName { get; set; } = default!;

    public double ExtraPrice { get; set; } = 0;

    public string ExtraType { get; set; } = default!;

    public string? ExtraDescription { get; set; }

    public string? ExtraColor { get; set; }

    public bool IsDefault { get; set; } = false;

    public Guid? ExtraImageId { get; set; }

}

public class CakeExtraCreateModelValidator : AbstractValidator<CakeExtraCreateModel>
{
    public CakeExtraCreateModelValidator()
    {
        RuleFor(x => x.ExtraName)
            .NotEmpty().WithMessage("Extra name is required.")
            .MaximumLength(100).WithMessage("Extra name cannot exceed 100 characters.");

        RuleFor(x => x.ExtraPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Extra price must be greater than or equal to 0.");

        RuleFor(x => x.ExtraType)
            .NotEmpty().WithMessage("Extra type is required.")
            .MaximumLength(50).WithMessage("Extra type cannot exceed 50 characters.");

        RuleFor(x => x.ExtraDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.ExtraDescription != null);


        RuleFor(x => x.ExtraImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("ExtraImageId must different empty Guid.")
            .When(x => x.ExtraImageId != null);
    }
}

public class CakeExtraUpdateModel : CakeExtraCreateModel
{
}