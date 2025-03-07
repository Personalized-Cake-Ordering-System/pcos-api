using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("orders")]
public class Order : BaseEntity
{
    [JsonPropertyName("total_price")]
    [Column("total_price")]
    public double TotalPrice { get; set; }

    [JsonPropertyName("order_note")]
    [Column("order_note")]
    public string? OrderNote { get; set; }

    [JsonPropertyName("pickup_time")]
    [Column("pickup_time")]
    public DateTime? PickUpTime { get; set; } = DateTime.Now;

    [JsonPropertyName("shipping_type")]
    [Column("shipping_type")]
    public string ShippingType { get; set; } = default!;

    [JsonPropertyName("payment_type")]
    [Column("payment_type")]
    public string PaymentType { get; set; } = default!;

    [JsonPropertyName("canceled_reason")]
    [Column("canceled_reason")]
    public string? CanceledReason { get; set; }

    [JsonPropertyName("phone_number")]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("order_address")]
    [Column("order_address")]
    public string? OrderAddress { get; set; }

    [JsonPropertyName("order_status")]
    [Column("order_status")]
    public string? OrderStatus { get; set; }

    [JsonPropertyName("customer_id")]
    [Column("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = default!;

    [JsonPropertyName("bakery_id")]
    [Column("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;

    [JsonPropertyName("transaction_id")]
    [Column("transaction_id")]
    public Guid? TransactionId { get; set; }

    [JsonPropertyName("transaction")]
    public Transaction? Transaction { get; set; }

    [JsonPropertyName("voucher_id")]
    [Column("voucher_id")]
    public Guid? VoucherId { get; set; }

    [JsonPropertyName("voucher")]
    public Voucher? Voucher { get; set; }

    [JsonPropertyName("customer_voucher_id")]
    [Column("customer_voucher_id")]
    public Guid? CustomerVoucherId { get; set; }

    [JsonPropertyName("customer_voucher")]
    public CustomerVoucher? CustomerVoucher { get; set; }

    [JsonPropertyName("order_details")]
    public ICollection<OrderDetail> OrderDetails { get; set; } = default!;

    [JsonPropertyName("order_supports")]
    public ICollection<OrderSupport>? OrderSupports { get; set; }
}
