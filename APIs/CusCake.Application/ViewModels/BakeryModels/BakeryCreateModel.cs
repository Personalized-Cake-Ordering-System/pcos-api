using System.ComponentModel.DataAnnotations;
using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CusCake.Application.ViewModels.BakeryModels;

public class BakeryBaseActionModel
{
    public string BakeryName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string OwnerName { get; set; } = default!;
    public string TaxCode { get; set; } = default!;
    public string IdentityCardNumber { get; set; } = default!;
    public List<IFormFile>? ShopImages { get; set; } = default!;

}


public class BakeryBaseActionModelValidator : AbstractValidator<BakeryBaseActionModel>
{
    public BakeryBaseActionModelValidator()
    {

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password name is required.")
            .MaximumLength(30)
            .MinimumLength(8)
            .WithMessage("Password cannot exceed 30 characters.");

        RuleFor(x => x.BakeryName)
            .NotEmpty().WithMessage("Bakery name is required.")
            .MaximumLength(100).WithMessage("Bakery name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+84|0)(3[2-9]|5[2-9]|7[0|6-9]|8[1-9]|9[0-9])\d{7}$")
            .WithMessage("Phone number must be a valid Vietnamese number (e.g., +84912345678 or 0912345678).");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

        RuleFor(x => x.OwnerName)
            .NotEmpty().WithMessage("Owner name is required.")
            .MaximumLength(100).WithMessage("Owner name cannot exceed 100 characters.");

        RuleFor(x => x.TaxCode)
            .NotEmpty().WithMessage("Tax code is required.")
            .Matches(@"^\d{10}(\d{3})?$")
            .WithMessage("Tax code must be 10 or 13 digits (e.g., 1234567890 or 1234567890123).");

        RuleFor(x => x.IdentityCardNumber)
            .NotEmpty().WithMessage("Identity card number is required.")
            .Matches(@"^\d{9}$|^\d{12}$")
            .WithMessage("Identity card number must be 9 or 12 digits.");

        RuleFor(x => x.ShopImages)
            .Null().WithMessage("Images can be null.");

        RuleForEach(x => x.ShopImages)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Each shop image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.ShopImages != null && x.ShopImages.Count != 0);
    }
}

public class BakeryCreateModel : BakeryBaseActionModel
{

    public IFormFile? Avatar { get; set; } = default!;
    public IFormFile? FrontCardImage { get; set; } = default!;
    public IFormFile? BackCardImage { get; set; } = default!;

}

public class BakeryCreateModelValidator : AbstractValidator<BakeryCreateModel>
{
    public BakeryCreateModelValidator()
    {

        Include(new BakeryBaseActionModelValidator());

        RuleFor(x => x.Avatar)
            .NotNull().WithMessage("Avatar image is required.")
            .Must(ValidationUtils.BeAValidImage).WithMessage("Avatar must be a valid image file (jpg, png, jpeg) under 5MB.");


        RuleFor(x => x.FrontCardImage)
            .NotNull().WithMessage("Front card image is required.")
            .Must(ValidationUtils.BeAValidImage).WithMessage("Front card image must be a valid image file (jpg, png, jpeg) under 5MB.");

        RuleFor(x => x.BackCardImage)
            .NotNull().WithMessage("Back card image is required.")
            .Must(ValidationUtils.BeAValidImage).WithMessage("Back card image must be a valid image file (jpg, png, jpeg) under 5MB.");

    }
}

public class BakeryUpdateModel : BakeryCreateModel
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid Id { get; set; }
    public List<Guid>? DeleteImageFileIds { get; set; } = [];
}

public class BakeryUpdateModelValidator : AbstractValidator<BakeryUpdateModel>
{
    public BakeryUpdateModelValidator()
    {

        Include(new BakeryBaseActionModelValidator());

        RuleFor(x => x.Avatar)
            .Must(ValidationUtils.BeAValidImage)
            .WithMessage("Avatar must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.Avatar != null);

        RuleFor(x => x.FrontCardImage)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Front card image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.FrontCardImage != null)
            .OverridePropertyName("FrontCardImage");

        RuleFor(x => x.BackCardImage)
            .Must(ValidationUtils.BeAValidImage).WithMessage("Back card image must be a valid image file (jpg, png, jpeg) under 5MB.")
            .When(x => x.BackCardImage != null)
            .OverridePropertyName("BackCardImage");
    }

}
