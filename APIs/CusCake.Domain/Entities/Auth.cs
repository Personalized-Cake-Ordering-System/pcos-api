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

}