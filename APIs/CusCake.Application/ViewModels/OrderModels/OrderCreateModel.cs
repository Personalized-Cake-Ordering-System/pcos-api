using FluentValidation;

namespace CusCake.Application.ViewModels.OrderModels;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

public class OrderCreateModel
{
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("order_note")]
    public string? OrderNote { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("shipping_address")]
    public string? ShippingAddress { get; set; }

    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }

    [JsonPropertyName("pickup_time")]
    public DateTime? PickUpTime { get; set; } = DateTime.Now;

    [JsonPropertyName("shipping_type")]
    public string ShippingType { get; set; } = default!;

    [JsonPropertyName("payment_type")]
    public string PaymentType { get; set; } = default!;

    [JsonPropertyName("voucher_code")]
    public string? VoucherCode { get; set; }

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

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}

public class OrderCreateModelValidator : AbstractValidator<OrderCreateModel>
{
    public OrderCreateModelValidator()
    {
        RuleFor(x => x.BakeryId)
            .NotEmpty().WithMessage("BakeryId is require.");

        RuleFor(x => x.OrderDetailCreateModels)
            .NotNull().WithMessage("Order details can not null.")
            .Must(x => x != null && x.Count != 0).WithMessage("At least one order detail.")
            .DependentRules(() =>
            {
                RuleForEach(x => x.OrderDetailCreateModels)
                    .Must(orderDetail => orderDetail.CustomCakeId == null)
                    .When(x => x.ShippingType == ShippingTypeConstants.PICK_UP)
                    .WithMessage("CustomCakeId must be null when ShippingType is PICKUP.");
            });

        RuleFor(x => x.ShippingType)
                 .NotEmpty().WithMessage("Shipping type is required.")
                 .Must(type => type == ShippingTypeConstants.DELIVERY || type == ShippingTypeConstants.PICK_UP)
                 .WithMessage("Invalid shipping type.");

        RuleFor(x => x.PaymentType)
                 .NotEmpty().WithMessage("Payment type is required.")
                 .Must(type => type == PaymentTypeConstants.QR_CODE || type == PaymentTypeConstants.CASH || type == PaymentTypeConstants.WALLET)
                 .WithMessage("Invalid payment type.");

        RuleFor(x => x.PickUpTime)
        .Must(x => x.HasValue)
        .When(x => x.ShippingType == ShippingTypeConstants.PICK_UP)
        .WithMessage("PickUpTime is required when shipping type is PICKUP.");
    }
}

public class OrderDetailCreateModelValidator : AbstractValidator<OrderDetailCreateModel>
{
    public OrderDetailCreateModelValidator()
    {
        RuleFor(x => x.AvailableCakeId)
            .Must(x => x != null)
            .When(x => x.CustomCakeId == null)
            .WithMessage("AvailableCakeId or CustomCakeId can't be null.");

        RuleFor(x => x.CustomCakeId)
            .Must(x => x != null)
            .When(x => x.AvailableCakeId == null)
            .WithMessage("AvailableCakeId or CustomCakeId can't be null.");

        RuleFor(x => x.Quantity)
            .NotNull().NotEmpty().WithMessage("Quantity is required")
            .GreaterThan(0)
            .WithMessage("At least 1 for each quantity.");
    }
}

public class OrderUpdateModel : OrderCreateModel
{
}