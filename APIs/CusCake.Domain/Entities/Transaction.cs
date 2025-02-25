using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("transactions")]
public class Transaction : BaseEntity
{
    [Column("amount")]
    public double Amount { get; set; }

    [Column("order_id")]
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public ICollection<BankEvent>? BankEvents { get; set; }
}