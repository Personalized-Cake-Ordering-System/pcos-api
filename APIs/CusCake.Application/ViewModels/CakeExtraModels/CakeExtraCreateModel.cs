using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;


namespace CusCake.Application.ViewModels.CakeExtraModels;

public class CakeExtraCreateModel
{
    [JsonPropertyName("extra_name")]
    public string ExtraName { get; set; } = default!;

    [JsonPropertyName("extra_price")]
    public double ExtraPrice { get; set; } = 0;

    [JsonPropertyName("extra_type")]
    public string ExtraType { get; set; } = default!;

    [JsonPropertyName("extra_description")]
    public string? ExtraDescription { get; set; }

    [JsonPropertyName("extra_color")]
    public string? ExtraColor { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [JsonPropertyName("extra_image_id")]
    public Guid? ExtraImageId { get; set; }
}

public class ListCakeExtraCreateModelValidator : AbstractValidator<List<CakeExtraCreateModel>>
{
    public ListCakeExtraCreateModelValidator()
    {
        RuleForEach(x => x).SetValidator(new CakeExtraCreateModelValidator());
    }
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
            .NotNull().WithMessage("Extra type is required.")
            .Must(value => Enum.IsDefined(typeof(CakeExtraTypeEnum), value))
            .WithMessage($"Invalid extra type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeExtraTypeEnum)))}");


        RuleFor(x => x.ExtraDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.ExtraDescription != null);

        RuleFor(x => x.ExtraImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("ExtraImageId must be different from empty Guid.")
            .When(x => x.ExtraImageId != null);
    }

}


public class CakeExtraUpdateModel : CakeExtraCreateModel
{
}
