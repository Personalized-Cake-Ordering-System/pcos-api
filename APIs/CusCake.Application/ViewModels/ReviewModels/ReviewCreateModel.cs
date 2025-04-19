using System.Text.Json.Serialization;
using CusCake.Domain.Constants;
using FluentValidation;

namespace CusCake.Application.ViewModels.ReviewModels;

public class ReviewCreateModel
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("order_detail_id")]
    public Guid? OrderDetailId { get; set; }

    [JsonPropertyName("available_cake_id")]
    public Guid? AvailableCakeId { get; set; }

    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("review_type")]
    public string ReviewType { get; set; } = ReviewTypeConstants.AVAILABLE_CAKE_REVIEW;
}

public class ReviewUpdateModel : ReviewCreateModel
{

}
public class ReviewCreateModelValidator : AbstractValidator<ReviewCreateModel>
{
    public ReviewCreateModelValidator()
    {
        RuleFor(x => x.Rating)
            .NotEmpty().NotNull().WithMessage("Rating is required!")
            .GreaterThanOrEqualTo(0).LessThanOrEqualTo(5)
            .WithMessage("Rating from 0-5");
    }
}

