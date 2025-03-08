using AutoMapper.Configuration.Annotations;
using CusCake.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.AvailableCakeModels;


public class AvailableCakeBaseActionModel
{
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("available_cake_price")]
    public double AvailableCakePrice { get; set; } = 0;

    [JsonPropertyName("available_cake_name")]
    public string AvailableCakeName { get; set; } = default!;

    [JsonPropertyName("available_cake_description")]
    public string? AvailableCakeDescription { get; set; }

    [JsonPropertyName("available_cake_type")]
    public string AvailableCakeType { get; set; } = default!;

    [JsonPropertyName("available_cake_quantity")]
    public int AvailableCakeQuantity { get; set; } = 0;

    [JsonPropertyName("available_cake_image_files")]
    public List<Guid> AvailableCakeImageFiles { get; set; } = default!;
}


public class AvailableCakeBaseActionModelValidator : AbstractValidator<AvailableCakeBaseActionModel>
{
    public AvailableCakeBaseActionModelValidator()
    {
        RuleFor(x => x.BakeryId)
            .NotEmpty().WithMessage("Bakery ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Bakery ID must be a valid GUID.");

        RuleFor(x => x.AvailableCakePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

        RuleFor(x => x.AvailableCakeName)
            .NotEmpty().WithMessage("Cake name is required.")
            .MaximumLength(100).WithMessage("Cake name cannot exceed 100 characters.");

        RuleFor(x => x.AvailableCakeDescription)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => x.AvailableCakeDescription != null);

        RuleFor(x => x.AvailableCakeType)
            .NotEmpty().WithMessage("Cake type is required.")
            .MaximumLength(50).WithMessage("Cake type cannot exceed 50 characters.");

        RuleFor(x => x.AvailableCakeQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0.");

        RuleFor(x => x.AvailableCakeImageFiles)
            .NotNull().WithMessage("AvailableCakeImageFiles cannot be null")
            .NotEmpty().WithMessage("AvailableCakeImageFiles cannot be empty")
            .Must(files => files.All(file => file != Guid.Empty))
            .WithMessage("AvailableCakeImageFiles contains an invalid GUID");

    }
}

public class AvailableCakeCreateModel : AvailableCakeBaseActionModel
{
    [JsonPropertyName("available_main_image_id")]
    public Guid AvailableCakeMainImageId { get; set; } = default!;
}

public class AvailableCakeCreateModelValidator : AbstractValidator<AvailableCakeCreateModel>
{
    public AvailableCakeCreateModelValidator()
    {

        Include(new AvailableCakeBaseActionModelValidator());

    }
}


public class AvailableCakeUpdateModel : AvailableCakeBaseActionModel
{
    [JsonPropertyName("available_main_image_id")]
    public Guid AvailableCakeMainImageId { get; set; }

}

public class AvailableCakeUpdateModelValidator : AbstractValidator<AvailableCakeUpdateModel>
{
    public AvailableCakeUpdateModelValidator()
    {
        Include(new AvailableCakeBaseActionModelValidator());

    }
}