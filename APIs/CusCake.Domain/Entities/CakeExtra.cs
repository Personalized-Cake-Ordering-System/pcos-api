using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_extras")]
public class CakeExtra : BaseEntity
{
    [Column("extra_name")]
    public string ExtraName { get; set; } = default!;
    [Column("extra_price")]
    public double ExtraPrice { get; set; } = 0;

    [Column("extra_type")]
    public string ExtraType { get; set; } = default!;

    [Column("extra_color")]
    public string? ExtraColor { get; set; }

    [Column("extra_description")]
    public string? ExtraDescription { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; } = false;

    [Column("extra_image_id")]
    public Guid? ExtraImageId { get; set; }
    public Storage? ExtraImage { get; set; }

    public Guid BakeryId { get; set; } = default!;

    public ICollection<CakeExtraDetail>? CakeExtraDetails { get; set; }

}