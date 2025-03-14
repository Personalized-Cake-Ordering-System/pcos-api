using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

namespace CusCake.Domain.Entities;

[Table("vouchers")]
public class Voucher : BaseEntity
{
    [JsonPropertyName("bakery_id")]
    [Column("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;

    [JsonPropertyName("code")]
    [Column("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("discount_percentage")]
    [Column("discount_percentage")]
    public double DiscountPercentage { get; set; }

    [JsonPropertyName("min_order_amount")]
    [Column("min_order_amount")]
    public double MinOrderAmount { get; set; }

    [JsonPropertyName("max_discount_amount")]
    [Column("max_discount_amount")]
    public double MaxDiscountAmount { get; set; }

    [JsonPropertyName("expiration_date")]
    [Column("expiration_date")]
    public DateTime ExpirationDate { get; set; }

    [JsonPropertyName("quantity")]
    [Column("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("usage_count")]
    [Column("usage_count")]
    public int UsageCount { get; set; }

    [JsonPropertyName("description")]
    [Column("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("voucher_type")]
    [Column("voucher_type")]
    public string VoucherType { get; set; } = VoucherTypeConstants.GLOBAL;

}
