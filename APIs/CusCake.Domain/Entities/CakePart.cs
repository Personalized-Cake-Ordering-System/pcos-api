using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_parts")]
public class CakePart : BaseEntity
{
    [Column("part_name")]
    public string PartName { get; set; } = default!;
    [Column("part_price")]
    public double PartPrice { get; set; } = 0;
    [Column("part_size")]
    public double? PartSize { get; set; }

    [Column("part_type")]
    public string PartType { get; set; } = default!;

    [Column("part_description")]
    public string? PartDescription { get; set; }

    public ICollection<CakePartDetail>? CakePartDetails { get; set; }

}