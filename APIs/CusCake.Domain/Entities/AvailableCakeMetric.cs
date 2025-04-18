using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("available_cake_metrics")]
    public class AvailableCakeMetric : BaseEntity
    {
        [Column("available_cake_id")]
        [JsonPropertyName("available_cake_id")]
        public Guid AvailableCakeId { get; set; } = default!;
        [JsonPropertyName("available_cake")]
        public AvailableCake AvailableCake { get; set; } = default!;

        [Column("rating_average")]
        [JsonPropertyName("rating_average")]
        public double RatingAverage { get; set; } = default!;

        [Column("reviews_count")]
        [JsonPropertyName("reviews_count")]
        public int ReviewsCount { get; set; } = default!;

        [Column("quantity_sold")]
        [JsonPropertyName("quantity_sold")]
        public int QuantitySold { get; set; } = default!;
    }
}