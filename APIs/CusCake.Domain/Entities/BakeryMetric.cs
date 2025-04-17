using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("bakery_metrics")]
    public class BakeryMetric : BaseEntity
    {
        [Column("bakery_id")]
        [JsonPropertyName("bakery_id")]
        public Guid BakeryId { get; set; } = default!;

        [JsonPropertyName("bakery")]
        public Bakery Bakery { get; set; } = default!;

        [Column("total_revenue")]
        [JsonPropertyName("total_revenue")]
        public double TotalRevenue { get; set; } = default!;

        [Column("orders_count")]
        [JsonPropertyName("orders_count")]
        public int OrdersCount { get; set; } = default!;

        [Column("rating_average")]
        [JsonPropertyName("rating_average")]
        public double RatingAverage { get; set; } = default!;

        [Column("customers_count")]
        [JsonPropertyName("customers_count")]
        public int CustomersCount { get; set; } = default!;

        [Column("average_order_value")]
        [JsonPropertyName("average_order_value")]
        public double AverageOrderValue { get; set; } = default!;
    }
}