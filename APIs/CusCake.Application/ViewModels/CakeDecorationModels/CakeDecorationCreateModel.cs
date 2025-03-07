using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CakeDecorationModels;



public class CakeDecorationCreateModel
{
    [JsonPropertyName("decoration_name")]
    public string DecorationName { get; set; } = default!;

    [JsonPropertyName("decoration_price")]
    public double DecorationPrice { get; set; } = 0;

    [JsonPropertyName("decoration_type")]
    public string DecorationType { get; set; } = default!;

    [JsonPropertyName("decoration_description")]
    public string? DecorationDescription { get; set; }

    [JsonPropertyName("decoration_color")]
    public string? DecorationColor { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [JsonPropertyName("decoration_image_id")]
    public Guid? DecorationImageId { get; set; }
}

public class ListCakeDecorationCreateModelValidator : AbstractValidator<List<CakeDecorationCreateModel>>
{
    public ListCakeDecorationCreateModelValidator()
    {
        RuleForEach(x => x).SetValidator(new CakeDecorationCreateModelValidator());
    }
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
             .NotNull().WithMessage("Decoration type is required.")
             .Must(value => Enum.IsDefined(typeof(CakeDecorationTypeEnum), value))
             .WithMessage($"Invalid Decoration type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeDecorationTypeEnum)))}");

        RuleFor(x => x.DecorationDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.DecorationDescription != null);


        RuleFor(x => x.DecorationImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("DecorationImageId must different empty Guid")
            .When(x => x.DecorationImageId != null);
    }
}

public class CakeDecorationUpdateModel : CakeDecorationCreateModel
{
}