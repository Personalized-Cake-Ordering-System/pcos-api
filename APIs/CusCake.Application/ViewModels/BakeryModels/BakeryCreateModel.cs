using System.Text.Json.Serialization;
using FluentValidation;

namespace CusCake.Application.ViewModels.BakeryModels;

public class BakeryBaseActionModel
{
    [JsonPropertyName("bakery_name")]
    public string BakeryName { get; set; } = default!;


    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = default!;

    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;

    [JsonPropertyName("latitude")]
    public string Latitude { get; set; } = default!;

    [JsonPropertyName("longitude")]
    public string Longitude { get; set; } = default!;

    [JsonPropertyName("owner_name")]
    public string OwnerName { get; set; } = default!;

    [JsonPropertyName("tax_code")]
    public string TaxCode { get; set; } = default!;

    [JsonPropertyName("identity_card_number")]
    public string IdentityCardNumber { get; set; } = default!;

    [JsonPropertyName("shop_image_file_ids")]
    public List<Guid> ShopImageFileIds { get; set; } = new List<Guid>()!;

    [JsonPropertyName("avatar_file_id")]
    public Guid AvatarFileId { get; set; }

    [JsonPropertyName("front_card_file_id")]
    public Guid FrontCardFileId { get; set; }

    [JsonPropertyName("back_card_file_id")]
    public Guid BackCardFileId { get; set; }

    [JsonPropertyName("cake_description")]
    public string? CakeDescription { get; set; } = default!;

    [JsonPropertyName("price_description")]
    public string? PriceDescription { get; set; } = default!;

    [JsonPropertyName("bakery_description")]
    public string? BakeryDescription { get; set; } = default!;
    // [JsonPropertyName("bank_account")]
    // public string? BankAccount { get; set; } = default!;
}



public class BakeryBaseActionModelValidator : AbstractValidator<BakeryBaseActionModel>
{
    public BakeryBaseActionModelValidator()
    {

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required and must be between 8 and 30 characters.")
            .Length(8, 30).WithMessage("Password must be between 8 and 30 characters.");


        RuleFor(x => x.BakeryName)
            .NotEmpty().WithMessage("Bakery name is required.")
            .MaximumLength(100).WithMessage("Bakery name cannot exceed 100 characters.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+84|0)(3[2-9]|5[2-9]|7[0|6-9]|8[1-9]|9[0-9])\d{7}$")
            .WithMessage("Phone number must be a valid Vietnamese number (e.g., +84912345678 or 0912345678).");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(100).WithMessage("Address cannot exceed 500 characters.");

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


        RuleFor(x => x.ShopImageFileIds)
            .NotNull().WithMessage("ShopImageFileIds is required.")
            .NotEmpty().WithMessage("ShopImageFileIds is required.")
            .Must(files => files!.All(file => file != Guid.Empty)).WithMessage("ShopImageFiles contains an invalid GUID.")
            .Must(files => files.Distinct().Count() == files.Count).WithMessage("ShopImageFileIds must be unique.");

    }
}

public class BakeryCreateModel : BakeryBaseActionModel
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;
}

public class BakeryCreateModelValidator : AbstractValidator<BakeryCreateModel>
{
    public BakeryCreateModelValidator()
    {

        Include(new BakeryBaseActionModelValidator());

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

    }
}

public class BakeryUpdateModel : BakeryBaseActionModel
{
}
