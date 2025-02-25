using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("vouchers")]
public class Voucher : BaseEntity
{
    [Column("bakery_id")]
    public Guid BakeryId { get; set; }
    public Bakery Bakery { get; set; } = default!;

    [Column("code")]
    public string Code { get; set; } = default!;

    [Column("discount_amount")]
    public double DiscountAmount { get; set; }

    [Column("discount_percentage")]
    public double DiscountPercentage { get; set; }

    [Column("min_order_amount")]
    public double MinOrderAmount { get; set; }

    [Column("max_discount_amount")]
    public double MaxDiscountAmount { get; set; }

    [Column("expiration_date")]
    public DateTime ExpirationDate { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("usage_limit")]
    public int UsageLimit { get; set; }

    [Column("usage_count")]
    public int UsageCount { get; set; }

    [Column("description")]
    public string Description { get; set; } = default!;

    [Column("voucher_type")]
    public string VoucherType { get; set; } = default!;

    public ICollection<Order>? Orders { get; set; }
    public ICollection<CustomerVoucher>? CustomerVouchers { get; set; }

}
