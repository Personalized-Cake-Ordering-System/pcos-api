// using System.ComponentModel.DataAnnotations.Schema;
// using System.Text.Json.Serialization;

// namespace CusCake.Domain.Entities;

// [Table("cake_message_types")]
// public class CakeMessageType : BaseEntity
// {

//     [Column("name")]
//     [JsonPropertyName("name")]
//     public string Name { get; set; } = default!;

//     [Column("price")]
//     [JsonPropertyName("price")]
//     public double Price { get; set; } = 0;

//     [Column("description")]
//     [JsonPropertyName("description")]
//     public string? Description { get; set; }

//     [Column("is_default ")]
//     [JsonPropertyName("is_default")]
//     public bool IsDefault { get; set; } = false;

//     [Column("bakery_id")]
//     [JsonPropertyName("bakery_id")]
//     public Guid BakeryId { get; set; } = default!;

//     [Column("options")]
//     [JsonPropertyName("options")]
//     public ICollection<CakeMessageOption>? Options { get; set; }
// }

