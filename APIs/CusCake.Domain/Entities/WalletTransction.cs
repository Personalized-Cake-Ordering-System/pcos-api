using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("wallet_transactions")]
public class WalletTransaction : BaseEntity
{
    [Column("wallet_id")]
    [JsonPropertyName("wallet_id")]
    public Guid WalletId { get; set; }

    [JsonPropertyName("wallet")]
    public Wallet Wallet { get; set; } = default!;

    [Column("amount")]
    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [Column("transaction_type")]
    [JsonPropertyName("transaction_type")]
    public string TransactionType { get; set; } = default!;
}
