using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("customer_vouchers")]
public class CustomerVoucher : BaseEntity
{
    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    [Column("voucher_id")]
    public Guid VoucherId { get; set; }
    public Voucher Voucher { get; set; } = default!;

    [Column("oder_id")]
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; } = default!;

}

