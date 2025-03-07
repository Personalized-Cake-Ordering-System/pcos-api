using CusCake.Domain.Enums;
using FluentValidation;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CakeMessageModels;

public class CakeMessageCreateModel
{
    [JsonPropertyName("message_name")]
    public string MessageName { get; set; } = default!;

    [JsonPropertyName("message_image_id")]
    public Guid? MessageImageId { get; set; } = default!;

    [JsonPropertyName("message_price")]
    public double MessagePrice { get; set; } = 0;

    [JsonPropertyName("message_type")]
    public string MessageType { get; set; } = default!;

    [JsonPropertyName("message_description")]
    public string? MessageDescription { get; set; }

    [JsonPropertyName("cake_message_type_creates")]
    public List<CakeMessageTypeModel>? CakeMessageTypeCreates { get; set; }
}

public class CakeMessageTypeModel
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;
}
public class ListCakeMessageCreateModelValidator : AbstractValidator<List<CakeMessageCreateModel>>
{
    public ListCakeMessageCreateModelValidator()
    {
        RuleForEach(x => x).SetValidator(new CakeMessageCreateModelValidator());
    }
}

public class CakeMessageCreateModelValidator : AbstractValidator<CakeMessageCreateModel>
{
    public CakeMessageCreateModelValidator()
    {
        RuleFor(x => x.MessageName)
            .NotEmpty().WithMessage("MessageName is required.")
            .MaximumLength(100).WithMessage("MessageName cannot exceed 100 characters.");

        RuleFor(x => x.MessageImageId)
            .NotEqual(Guid.Empty).WithMessage("MessageImageId cannot be an empty GUID.")
            .When(x => x.MessageImageId.HasValue);

        RuleFor(x => x.MessagePrice)
            .GreaterThanOrEqualTo(0).WithMessage("MessagePrice must be greater than or equal to 0.");

        RuleFor(x => x.MessageType)
            .NotNull().WithMessage("Message type is required.")
            .Must(value => Enum.IsDefined(typeof(CakeMessageTypeEnum), value))
            .WithMessage($"Invalid Message type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeMessageTypeEnum)))}");

        RuleFor(x => x.MessageDescription)
            .MaximumLength(500).WithMessage("MessageDescription cannot exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.MessageDescription));

        RuleForEach(x => x.CakeMessageTypeCreates)
            .SetValidator(new CakeMessageTypeCreateModelValidator())
            .When(x => x.CakeMessageTypeCreates != null && x.CakeMessageTypeCreates.Count != 0);
    }
}

public class CakeMessageTypeCreateModelValidator : AbstractValidator<CakeMessageTypeModel>
{
    public CakeMessageTypeCreateModelValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(50).WithMessage("Type cannot exceed 50 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Color is required.")
            .MaximumLength(20).WithMessage("Color cannot exceed 20 characters.");
    }
}

public class CakeMessageUpdateModel : CakeMessageCreateModel
{

}