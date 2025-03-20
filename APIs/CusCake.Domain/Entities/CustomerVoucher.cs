using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("customer_vouchers")]
public class CustomerVoucher : BaseEntity
{
    [JsonPropertyName("customer_id")]
    [Column("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = default!;

    [JsonPropertyName("voucher_id")]
    [Column("voucher_id")]
    public Guid VoucherId { get; set; }

    [JsonPropertyName("voucher")]
    public Voucher Voucher { get; set; } = default!;

    [JsonPropertyName("order_id")]
    [Column("oder_id")]
    public Guid? OrderId { get; set; }

    [JsonPropertyName("order")]
    public Order? Order { get; set; } = default!;

    [JsonPropertyName("is_applied")]
    [Column("is_applied")]
    public bool IsApplied { get; set; } = false;

    [JsonPropertyName("applied_at")]
    [Column("applied_at")]
    public DateTime? AppliedAt { get; set; }


}
