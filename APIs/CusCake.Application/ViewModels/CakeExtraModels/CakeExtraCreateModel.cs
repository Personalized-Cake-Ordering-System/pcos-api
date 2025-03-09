using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;


namespace CusCake.Application.ViewModels.CakeExtraModels;

public class CakeExtraCreateModel
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
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Extra name is required.")
            .MaximumLength(100).WithMessage("Extra name cannot exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Extra price must be greater than or equal to 0.");

        RuleFor(x => x.Type)
            .NotNull().WithMessage("Extra type is required.")
            .Must(value => Enum.IsDefined(typeof(CakeExtraTypeEnum), value))
            .WithMessage($"Invalid extra type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeExtraTypeEnum)))}");


        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.Description != null);

        RuleFor(x => x.ImageId)
            .Must(x => x != Guid.Empty)
            .WithMessage("ExtraImageId must be different from empty Guid.")
            .When(x => x.ImageId != null);
    }

}


public class CakeExtraUpdateModel : CakeExtraCreateModel
{
}
