using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CakePartModels;


public class CakePartCreateModel
{
    [JsonPropertyName("part_name")]
    public string PartName { get; set; } = default!;

    [JsonPropertyName("part_price")]
    public double PartPrice { get; set; } = 0;

    [JsonPropertyName("part_type")]
    public string PartType { get; set; } = default!;

    [JsonPropertyName("part_color")]
    public string? PartColor { get; set; }

    [JsonPropertyName("part_description")]
    public string? PartDescription { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [JsonPropertyName("part_image_id")]
    public Guid? PartImageId { get; set; }
}

public class ListCakePartCreateModelValidator : AbstractValidator<List<CakePartCreateModel>>
{
    public ListCakePartCreateModelValidator()
    {
        RuleForEach(x => x).SetValidator(new CakePartCreateModelValidator());
    }
}

public class CakePartCreateModelValidator : AbstractValidator<CakePartCreateModel>
{
    public CakePartCreateModelValidator()
    {
        RuleFor(x => x.PartName)
            .NotEmpty().WithMessage("Part name is required.")
            .MaximumLength(100).WithMessage("Part name cannot exceed 100 characters.");

        RuleFor(x => x.PartPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Part price must be greater than or equal to 0.");

        RuleFor(x => x.PartType)
           .NotNull().WithMessage("Part type is required.")
           .Must(value => Enum.IsDefined(typeof(CakePartTypeEnum), value))
           .WithMessage($"Invalid Part type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakePartTypeEnum)))}");

        RuleFor(x => x.PartDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.PartDescription != null);


        RuleFor(x => x.PartImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("PartImageId must different empty Guid.")
            .When(x => x.PartImageId != null);
    }
}

public class CakePartUpdateModel : CakePartCreateModel
{
}