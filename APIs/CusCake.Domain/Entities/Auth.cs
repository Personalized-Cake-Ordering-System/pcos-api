using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("auths")]
public class Auth : BaseEntity
{
    [Column("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [Column("password")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

    [Column("role")]
    [JsonPropertyName("role")]
    public string Role { get; set; } = default!;

    [Column("entity_id")]
    [JsonPropertyName("entity_id")]
    public Guid EntityId { get; set; } = default!;

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid? BakeryId { get; set; } = default!;

    [JsonPropertyName("bakery")]
    public Bakery? Bakery { get; set; }

    [Column("customer_id")]
    [JsonPropertyName("customer_id")]
    public Guid? CustomerId { get; set; } = default!;
    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [Column("admin_id")]
    [JsonPropertyName("admin_id")]
    public Guid? AdminId { get; set; } = default!;

    [JsonPropertyName("admin")]
    public Admin? Admin { get; set; } = default!;

    [Column("wallet_id")]
    [JsonPropertyName("wallet_id")]
    public Guid WalletId { get; set; }

    [JsonPropertyName("wallet")]
    public Wallet Wallet { get; set; } = default!;

}