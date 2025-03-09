using CusCake.Domain.Constants;
using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CakePartModels;


public class CakePartCreateModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; } = false;

    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
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
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Part name is required.")
            .MaximumLength(100).WithMessage("Part name cannot exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Part price must be greater than or equal to 0.");

        RuleFor(x => x.Type)
           .NotNull().WithMessage("Part type is required.")
           .Must(value => Enum.IsDefined(typeof(CakePartTypeEnum), value))
           .WithMessage($"Invalid Part type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakePartTypeEnum)))}");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.Description != null);


        RuleFor(x => x.ImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("PartImageId must different empty Guid.")
            .When(x => x.ImageId != null);
    }
}

public class CakePartUpdateModel : CakePartCreateModel
{
}