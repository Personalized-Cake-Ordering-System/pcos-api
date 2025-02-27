
using System.ComponentModel.DataAnnotations;
using CusCake.Application.Validators;
using FluentValidation;

namespace CusCake.Application.ViewModels.BakeryModel;

public class BakeryUpdateModel : BakeryCreateModel
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid Id { get; set; }
}

public class BakeryUpdateModelValidator : AbstractValidator<BakeryUpdateModel>
{
    public BakeryUpdateModelValidator()
    {
        RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Avatar)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Avatar must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.Avatar != null);

        RuleFor(x => x.FrontCardImage)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Front card image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.FrontCardImage != null);

        RuleFor(x => x.BackCardImage)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Back card image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.BackCardImage != null);

        RuleForEach(x => x.ShopImages)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Each shop image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.ShopImages != null && x.ShopImages.Count != 0);
    }

}