using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_extras")]
public class CakeExtra : BaseEntity
{
    [Column("extra_name")]
    public string ExtraName { get; set; } = default!;
    [Column("extra_price")]
    public double PartPrice { get; set; } = 0;
    [Column("extra_number")]
    public double? ExtraNumber { get; set; }

    [Column("extra_type")]
    public string ExtraType { get; set; } = default!;

    [Column("extra_description")]
    public string? ExtraDescription { get; set; }

    public ICollection<CakeExtraDetail>? CakeExtraDetails { get; set; }

}