using System.Text.Json.Serialization;
using FluentValidation;

namespace CusCake.Application.ViewModels.CakeReviewModels;

public class CakeReviewCreateModel
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("image_id")]
    public Guid? ImageId { get; set; }

    [JsonPropertyName("order_detail_id")]
    public Guid OrderDetailId { get; set; }

    [JsonPropertyName("available_cake_id")]
    public Guid AvailableCakeId { get; set; }

    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }
}

public class CakeReviewUpdateModel : CakeReviewCreateModel
{

}
public class CakeReviewCreateModelValidator : AbstractValidator<CakeReviewCreateModel>
{
    public CakeReviewCreateModelValidator()
    {
        RuleFor(x => x.Rating)
            .NotEmpty().NotNull().WithMessage("Rating is required!")
            .GreaterThanOrEqualTo(0).LessThanOrEqualTo(5)
            .WithMessage("Rating from 0-5");
    }
}

