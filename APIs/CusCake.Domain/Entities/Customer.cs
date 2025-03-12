using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("customers")]
    public class Customer : BaseEntity
    {
        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [Column("phone")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; } = default!;

        [Column("address")]
        [JsonPropertyName("address")]
        public string? Address { get; set; } = default!;

        [Column("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [Column("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = default!;

        [Column("account_type")]
        [JsonPropertyName("account_type")]
        public string AccountType { get; set; } = default!;

        // [JsonPropertyName("notifications")]
        // public ICollection<Notification>? Notifications { get; set; }

        // [JsonPropertyName("orders")]
        // public ICollection<Order>? Orders { get; set; }

        // [JsonPropertyName("cake_reviews")]
        // public ICollection<CakeReview>? CakeReviews { get; set; }

        // [JsonPropertyName("order_supports")]
        // public ICollection<OrderSupport>? OrderSupports { get; set; }

        // [JsonPropertyName("customer_vouchers")]
        // public ICollection<CustomerVoucher>? CustomerVouchers { get; set; }
    }
}