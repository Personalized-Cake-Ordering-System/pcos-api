using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("admins")]
    public class Admin : BaseEntity
    {
        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [Column("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [Column("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = default!;
    }
}
