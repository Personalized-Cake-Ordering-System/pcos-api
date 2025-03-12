using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CakeMessageModels;

public class CakeMessageOptionCreateModel
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

}

public class ListCakeMessageCreateModelValidator : AbstractValidator<List<CakeMessageOptionCreateModel>>
{
    public ListCakeMessageCreateModelValidator()
    {
        RuleForEach(x => x).SetValidator(new CakeMessageCreateModelValidator());
    }
}

public class CakeMessageCreateModelValidator : AbstractValidator<CakeMessageOptionCreateModel>
{
    public CakeMessageCreateModelValidator()
    {

        RuleFor(x => x.Type)
            .Must(x => x == "TEXT")
            .WithMessage($"Invalid Message type. Type must be TEXT");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("MessageName is required.")
            .MaximumLength(100).WithMessage("MessageName cannot exceed 100 characters.");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Message type is required.")
            .Must(value => Enum.IsDefined(typeof(CakeMessageOptionTypeEnum), value))
            .WithMessage($"Invalid Message type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeMessageOptionTypeEnum)))}");

    }
}



public class CakeMessageOptionUpdateModel : CakeMessageOptionCreateModel
{

}