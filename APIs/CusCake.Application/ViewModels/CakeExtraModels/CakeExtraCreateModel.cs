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