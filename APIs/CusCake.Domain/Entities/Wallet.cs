using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("wallets")]
public class Wallet : BaseEntity
{
    // [Column("auth_id")]
    // [JsonPropertyName("auth_id")]
    // public Guid AuthId { get; set; }

    // [JsonPropertyName("auth")]
    // public Auth Auth { get; set; } = default!;

    [Column("balance")]
    [JsonPropertyName("balance")]
    public double Balance { get; set; } = 0;
}
