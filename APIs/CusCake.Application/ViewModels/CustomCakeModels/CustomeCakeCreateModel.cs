using System.Text.Json.Serialization;
using CusCake.Application.Validators;
using CusCake.Domain.Entities;
using FluentValidation;

namespace CusCake.Application.ViewModels.CustomCakeModels;

public class CustomCakeCreateModel
{
    [JsonPropertyName("cake_name")]
    public string CakeName { get; set; } = default!;

    [JsonPropertyName("cake_description")]
    public string? CakeDecorationDetail { get; set; }

    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("cake_message_detail")]
    public CakeMessageCreateDetail? CakeMessageDetail { get; set; }

    [JsonPropertyName("cake_part_ids")]
    public List<Guid>? CakePartIds { get; set; }

    [JsonPropertyName("cake_decoration_ids")]
    public List<Guid>? CakeDecorationIds { get; set; }

    [JsonPropertyName("cake_decoration_ids")]
    public List<Guid>? CakeExtraIds { get; set; }
}

public class CakeMessageCreateDetail
{
    [JsonPropertyName("cake_message_id")]
    public Guid CakeMessageId { get; set; }
    [JsonPropertyName("message_image_id")]
    public Guid? MessageImageId { get; set; } = default!;
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; } = default!;
    [JsonPropertyName("message_type_details")]
    public List<CakeMessageTypeDetail>? MessageTypeDetails { get; set; }
}
public class CustomCakeCreateModelValidator : AbstractValidator<CustomCakeCreateModel>
{
    public CustomCakeCreateModelValidator()
    {
        RuleFor(x => x.CakeName)
            .NotEmpty().WithMessage("Cake name is required.")
            .MaximumLength(100).WithMessage("Cake name must not exceed 100 characters.");

        RuleFor(x => x.BakeryId)
            .NotEmpty().WithMessage("Bakery ID is required.");

        RuleFor(x => x.CakeMessageDetail)
            .SetValidator(new CakeMessageCreateDetailValidator()!)
            .When(x => x.CakeMessageDetail != null);

        RuleFor(x => x.CakePartIds)
            .Must(ValidationUtils.BeUniqueId!).WithMessage("Cake part IDs must be unique.")
            .When(x => x.CakePartIds != null && x.CakePartIds.Count != 0);

        RuleFor(x => x.CakeDecorationIds)
            .Must(ValidationUtils.BeUniqueId!).WithMessage("Cake decoration IDs must be unique.")
            .When(x => x.CakeDecorationIds != null && x.CakeDecorationIds.Count != 0);

        RuleFor(x => x.CakeExtraIds)
            .Must(ValidationUtils.BeUniqueId!).WithMessage("Cake extra IDs must be unique.")
            .When(x => x.CakeExtraIds != null && x.CakeExtraIds.Count != 0);
    }


}

public class CakeMessageCreateDetailValidator : AbstractValidator<CakeMessageCreateDetail>
{
    public CakeMessageCreateDetailValidator()
    {
        RuleFor(x => x.CakeMessageId)
            .NotEmpty().WithMessage("Cake message ID is required.");

        RuleFor(x => x.MessageType)
            .NotEmpty().WithMessage("Message type is required.")
            .Must(type => type == "text" || type == "image")
            .WithMessage("Message type must be either 'text' or 'image'.");

        RuleFor(x => x.MessageTypeDetails)
            .NotEmpty().WithMessage("Message type details are required for text messages.")
            .When(x => x.MessageType == "text");

        RuleFor(x => x.MessageImageId)
            .NotNull().WithMessage("Message image ID is required for image messages.")
            .When(x => x.MessageType == "image");
    }
}