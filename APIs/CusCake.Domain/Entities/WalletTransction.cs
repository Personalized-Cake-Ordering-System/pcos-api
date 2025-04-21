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

    [Column("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [Column("order_target_id")]
    [JsonPropertyName("order_target_id")]
    public Guid? OrderTargetId { get; set; } // ID của đơn hàng liên quan đến giao dịch này (nếu có)

    [Column("order_target_code")]
    [JsonPropertyName("order_target_code")]
    public string? OrderTargetCode { get; set; }

    [Column("target_user_id")]
    [JsonPropertyName("target_user_id")]
    public Guid? TargetUserId { get; set; }

    [Column("target_user_type")]
    [JsonPropertyName("target_user_type")]
    public string? TargetUserType { get; set; }
}
