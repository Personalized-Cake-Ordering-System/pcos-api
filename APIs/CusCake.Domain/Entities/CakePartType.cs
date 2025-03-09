// using System.ComponentModel.DataAnnotations.Schema;
// using System.Text.Json.Serialization;

// namespace CusCake.Domain.Entities;

// [Table("cake_part_types")]
// public class CakePartType : BaseEntity
// {
//     [Column("name")]
//     [JsonPropertyName("name")]
//     public string Name { get; set; } = default!;

//     [Column("is_required ")]
//     [JsonPropertyName("is_required")]
//     public bool IsRequired { get; set; } = false;

//     [Column("bakery_id")]
//     [JsonPropertyName("bakery_id")]
//     public Guid Bakery_id { get; set; }

//     [JsonPropertyName("options")]
//     public ICollection<CakePartOption> Options { get; set; } = new List<CakePartOption>();

// }