using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CakeDecorationModels;



public class CakeDecorationCreateModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("price")]
    public double Price { get; set; } = 0;

    [JsonPropertyName("color")]
    public string? Color { get; set; }
    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
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
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Extra name cannot exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

        RuleFor(x => x.Type)
             .NotNull().WithMessage("Decoration type is required.")
             .Must(value => Enum.IsDefined(typeof(CakeDecorationTypeEnum), value))
             .WithMessage($"Invalid Decoration type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeDecorationTypeEnum)))}");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.Description != null);


        RuleFor(x => x.ImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("DecorationImageId must different empty Guid")
            .When(x => x.ImageId != null);
    }
}

public class CakeDecorationUpdateModel : CakeDecorationCreateModel
{
}