// using System.ComponentModel.DataAnnotations.Schema;
// using System.Text.Json.Serialization;

// namespace CusCake.Domain.Entities;

// [Table("cake_decoration_types")]
// public class CakeDecorationType : BaseEntity
// {
//     [Column("name")]
//     [JsonPropertyName("name")]
//     public string Name { get; set; } = default!;

//     [Column("bakery_id")]
//     [JsonPropertyName("bakery_id")]
//     public Guid BakeryId { get; set; } = default!;

//     [JsonPropertyName("options")]
//     public ICollection<CakeDecorationOption>? Options { get; set; }
// }