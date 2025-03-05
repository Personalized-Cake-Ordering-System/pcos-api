using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("orders")]
public class Order : BaseEntity
{
    [Column("total_price")]
    public double TotalPrice { get; set; }
    [Column("order_note")]
    public string? OrderNote { get; set; }

    [Column("pickup_time")]
    public DateTime? PickUpTime { get; set; } = DateTime.Now;

    [Column("shipping_type")]
    public string ShippingType { get; set; } = default!;

    [Column("payment_type")]
    public string PaymentType { get; set; } = default!;

    [Column("canceled_reason")]
    public string? CanceledReason { get; set; }

    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [Column("order_address")]
    public string? OrderAddress { get; set; }

    [Column("order_status")]
    public string? OrderStatus { get; set; }

    [Column("customer_id")]
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = default!;

    [Column("bakery_id")]
    public Guid BakeryId { get; set; }

    public Bakery Bakery { get; set; } = default!;

    [Column("transaction_id")]
    public Guid? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }

    [Column("voucher_id")]
    public Guid? VoucherId { get; set; }
    public Voucher? Voucher { get; set; }
    [Column("customer_voucher_id")]
    public Guid? CustomerVoucherId { get; set; }
    public CustomerVoucher? CustomerVoucher { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = default!;
    public ICollection<OrderSupport>? OrderSupports { get; set; }

}