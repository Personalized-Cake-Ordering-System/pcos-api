using FluentValidation;

namespace CusCake.Application.ViewModels.VoucherModels;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

public class VoucherCreateModel
{

    [JsonPropertyName("discount_percentage")]
    public double DiscountPercentage { get; set; }

    [JsonPropertyName("min_order_amount")]
    public double MinOrderAmount { get; set; }

    [JsonPropertyName("max_discount_amount")]
    public double MaxDiscountAmount { get; set; }

    [JsonPropertyName("expiration_date")]
    public DateTime ExpirationDate { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("usage_count")]
    public int UsageCount { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("voucher_type")]
    public string VoucherType { get; set; } = VoucherTypeConstants.GLOBAL;
}

public class VoucherUpdateModel : VoucherCreateModel
{

}

public class AssignVoucherModel
{
    public Guid CustomerId { get; set; }
}

public class VoucherCreateModelValidator : AbstractValidator<VoucherCreateModel>
{
    public VoucherCreateModelValidator()
    {
        RuleFor(x => x.DiscountPercentage)
            .InclusiveBetween(1, 100).WithMessage("Discount percentage must be between 1% and 100%.");

        RuleFor(x => x.MinOrderAmount)
            .GreaterThanOrEqualTo(1).WithMessage("Minimum order amount must be at least 1.");

        RuleFor(x => x.MaxDiscountAmount)
            .GreaterThanOrEqualTo(1).WithMessage("Maximum discount amount must be at least 1.");

        RuleFor(x => x.ExpirationDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.");

        RuleFor(x => x.UsageCount)
            .GreaterThanOrEqualTo(0).WithMessage("Usage count must be at least 0.")
            .LessThanOrEqualTo(x => x.Quantity).WithMessage("Usage count cannot exceed quantity.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.VoucherType)
            .Must(v => v == VoucherTypeConstants.GLOBAL || v == VoucherTypeConstants.PRIVATE || v == VoucherTypeConstants.SYSTEM)
            .WithMessage("Voucher type must be either SYSTEM, GLOBAL or PRIVATE.");
    }
}
