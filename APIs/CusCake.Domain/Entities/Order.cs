using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

namespace CusCake.Domain.Entities;

[Table("orders")]
public class Order : BaseEntity
{
    [JsonPropertyName("total_product_price")]
    [Column("total_product_price")]
    public double TotalProductPrice { get; set; } = 0;

    [JsonPropertyName("total_customer_paid")]
    [Column("total_customer_paid")]
    public double TotalCustomerPaid { get; set; } = 0;

    [JsonPropertyName("shipping_distance")]
    [Column("shipping_distance")]
    public double ShippingDistance { get; set; } = 0;

    [JsonPropertyName("discount_amount")]
    [Column("discount_amount")]
    public double DiscountAmount { get; set; } = 0;

    [JsonPropertyName("shipping_fee")]
    [Column("shipping_fee")]
    public double ShippingFee { get; set; } = 0;

    [JsonPropertyName("shipping_time")]
    [Column("shipping_time")]
    public double? ShippingTime { get; set; }

    [JsonPropertyName("shipping_type")]
    [Column("shipping_type")]
    public string ShippingType { get; set; } = ShippingTypeConstants.PICK_UP;

    [JsonPropertyName("commission_rate")]
    [Column("commission_rate")]
    public double CommissionRate { get; set; } = OrderConstants.COMMISSION_RATE;

    [JsonPropertyName("app_commission_fee")]
    [Column("app_commission_fee")]
    public double AppCommissionFee { get; set; }

    [JsonPropertyName("shop_revenue")]
    [Column("shop_revenue")]
    public double ShopRevenue { get; set; }

    [JsonPropertyName("voucher_code")]
    [Column("voucher_code")]
    public string? VoucherCode { get; set; }

    [JsonPropertyName("order_note")]
    [Column("order_note")]
    public string? OrderNote { get; set; }

    [JsonPropertyName("pickup_time")]
    [Column("pickup_time")]
    public DateTime? PickUpTime { get; set; } = DateTime.Now;

    [JsonPropertyName("payment_type")]
    [Column("payment_type")]
    public string PaymentType { get; set; } = PaymentTypeConstants.QR_CODE;

    [JsonPropertyName("canceled_reason")]
    [Column("canceled_reason")]
    public string? CanceledReason { get; set; }

    [JsonPropertyName("phone_number")]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("shipping_address")]
    [Column("shipping_address")]
    public string? ShippingAddress { get; set; }

    [JsonPropertyName("latitude")]
    [Column("latitude")]
    public string? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    [Column("longitude")]
    public string? Longitude { get; set; }

    [JsonPropertyName("order_status")]
    [Column("order_status")]
    public string? OrderStatus { get; set; } = OrderStatusConstants.PENDING;

    [JsonPropertyName("cancel_by")]
    [Column("cancel_by")]
    public string? CancelBy { get; set; }

    [JsonPropertyName("order_code")]
    [Column("order_code")]
    public string OrderCode { get; set; } = default!;

    [JsonPropertyName("paid_at")]
    [Column("paid_at")]
    public DateTime? PaidAt { get; set; }
    public List<OrderDetail>? OrderDetails { get; set; }
    public List<OrderSupport>? OrderSupports { get; set; }

    #region RELATION

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

    #endregion

}
