using FluentValidation;

namespace CusCake.Application.ViewModels.OrderModels;
using System.Text.Json.Serialization;

public class OrderCreateModel
{
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("order_note")]
    public string? OrderNote { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("order_address")]
    public string? OrderAddress { get; set; }

    [JsonPropertyName("order_detail_create_models")]
    public List<OrderDetailCreateModel> OrderDetailCreateModels { get; set; } = default!;
}

public class OrderDetailCreateModel
{
    [JsonPropertyName("available_cake_id")]
    public Guid? AvailableCakeId { get; set; }

    [JsonPropertyName("custom_cake_id")]
    public Guid? CustomCakeId { get; set; }

    [JsonPropertyName("cake_note")]
    public string? CakeNote { get; set; }
}

public class OrderCreateModelValidator : AbstractValidator<OrderCreateModel>
{
    public OrderCreateModelValidator()
    {
        RuleFor(x => x.BakeryId)
            .NotEmpty().WithMessage("BakeryId is require.");

        RuleFor(x => x.OrderDetailCreateModels)
            .NotNull().WithMessage("Order details can not null.")
            .Must(x => x != null && x.Count != 0).WithMessage("At least one order detail.");
    }
}

public class OrderDetailCreateModelValidator : AbstractValidator<OrderDetailCreateModel>
{
    public OrderDetailCreateModelValidator()
    {
        RuleFor(x => x.AvailableCakeId)
            .NotNull().When(x => x.CustomCakeId == null)
            .WithMessage("AvailableCakeId or CustomCakeId can't be null.");

        RuleFor(x => x.CustomCakeId)
            .NotNull().When(x => x.AvailableCakeId == null)
            .WithMessage("AvailableCakeId or CustomCakeId can't be null.");
    }
}