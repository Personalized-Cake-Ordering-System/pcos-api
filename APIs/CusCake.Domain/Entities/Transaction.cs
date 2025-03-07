using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("transactions")]
public class Transaction : BaseEntity
{
    [JsonPropertyName("amount")]
    [Column("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("order_id")]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [JsonPropertyName("order")]
    public Order Order { get; set; } = default!;

    [JsonPropertyName("bank_events")]
    public ICollection<BankEvent>? BankEvents { get; set; }
}
