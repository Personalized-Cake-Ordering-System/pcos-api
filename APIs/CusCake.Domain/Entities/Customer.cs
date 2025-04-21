using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("customers")]
    public class Customer : BaseEntity
    {
        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [Column("phone")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; } = default!;

        [Column("address")]
        [JsonPropertyName("address")]
        public string? Address { get; set; } = default!;

        [JsonPropertyName("latitude")]
        [Column("latitude")]
        public string? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        [Column("longitude")]
        public string? Longitude { get; set; }

        [Column("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [Column("password")]
        [JsonPropertyName("password")]
        public string? Password { get; set; } = default!;

        [Column("account_type")]
        [JsonPropertyName("account_type")]
        public string AccountType { get; set; } = default!;

    }
}