using System.Text.Json.Serialization;
using CusCake.Application.Validators;
using CusCake.Domain.Entities;
using CusCake.Domain.Enums;
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

    [JsonPropertyName("message_selection")]
    public MessageSelection MessageSelection { get; set; } = default!;

    [JsonPropertyName("part_selections")]
    public List<PartSelection>? PartSelections { get; set; }

    [JsonPropertyName("decoration_selections")]
    public List<DecorationSelection>? DecorationSelections { get; set; }

    [JsonPropertyName("extra_selections")]
    public List<ExtraSelection>? ExtraSelections { get; set; }
}

public class PartSelection
{
    [JsonPropertyName("part_type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("part_option_id")]
    public Guid OptionId { get; set; }
}

public class ExtraSelection
{
    [JsonPropertyName("part_type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("part_option_id")]
    public Guid OptionId { get; set; }
}

public class DecorationSelection
{
    [JsonPropertyName("part_type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("part_option_id")]
    public Guid OptionId { get; set; }
}

public class MessageSelection
{
    [JsonPropertyName("text")]
    public string? Text { get; set; } = default!;

    [JsonPropertyName("message")]
    public string MessageType { get; set; } = default!;

    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("cake_message_option_ids")]
    public List<Guid> CakeMessageOptionIds { get; set; } = default!;
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

        RuleFor(x => x.MessageSelection)
            .NotEmpty()
            .NotNull()
            .SetValidator(new MessageCreateDetailValidator()!);

        RuleFor(x => x.PartSelections)
            .Must(types => types!.DistinctBy(x => x.Type).ToList().Count == types!.Count)
            .WithMessage("Cake part IDs must be unique.")
            .When(x => x.PartSelections != null && x.PartSelections.Count != 0);

        RuleFor(x => x.DecorationSelections)
            .Must(types => types!.DistinctBy(x => x.Type).ToList().Count == types!.Count)
            .WithMessage("Cake part IDs must be unique.")
            .When(x => x.DecorationSelections != null && x.DecorationSelections.Count != 0);

        RuleFor(x => x.ExtraSelections)
            .Must(types => types!.DistinctBy(x => x.Type).ToList().Count == types!.Count)
            .WithMessage("Cake part IDs must be unique.")
            .When(x => x.ExtraSelections != null && x.ExtraSelections.Count != 0);
    }
}

public class MessageCreateDetailValidator : AbstractValidator<MessageSelection>
{
    public MessageCreateDetailValidator()
    {
        RuleFor(x => x.MessageType)
            .NotEmpty().WithMessage("Message type is required.")
            .Must(type => type == "TEXT" || type == "IMAGE" || type == "NONE")
            .WithMessage("Message type must be either 'text', 'image' or 'none'.");

        RuleFor(x => x.Text)
            .NotEmpty()
            .When(x => x.MessageType == "TEXT")
            .WithMessage("Text is required when MessageType is TEXT.");

        RuleFor(x => x.CakeMessageOptionIds)
            .NotEmpty()
            .When(x => x.MessageType == "TEXT")
            .WithMessage("CakeMessageOptionIds is required when MessageType is TEXT.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("CakeMessageOptionIds must contain unique values.");

        RuleFor(x => x.ImageId)
            .NotNull()
            .When(x => x.MessageType == "IMAGE")
            .WithMessage("ImageId is required when MessageType is IMAGE.");
    }
}


public class PartSelectionValidator : AbstractValidator<PartSelection>
{
    public PartSelectionValidator()
    {
        RuleFor(x => x.Type)
          .NotNull().WithMessage("Part type is required.")
          .Must(value => Enum.IsDefined(typeof(CakePartTypeEnum), value))
          .WithMessage($"Invalid Part type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakePartTypeEnum)))}");
    }
}

public class DecorationSelectionValidator : AbstractValidator<DecorationSelection>
{
    public DecorationSelectionValidator()
    {
        RuleFor(x => x.Type)
            .NotNull().WithMessage("Decoration type is required.")
            .Must(value => Enum.IsDefined(typeof(CakeDecorationTypeEnum), value))
            .WithMessage($"Invalid Decoration type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeDecorationTypeEnum)))}");
    }
}
public class ExtraSelectionValidator : AbstractValidator<ExtraSelection>
{
    public ExtraSelectionValidator()
    {
        RuleFor(x => x.Type)
             .NotNull().WithMessage("Extra type is required.")
             .Must(value => Enum.IsDefined(typeof(CakeExtraTypeEnum), value))
             .WithMessage($"Invalid extra type. Must be one of: {string.Join(", ", Enum.GetNames(typeof(CakeExtraTypeEnum)))}");

    }
}