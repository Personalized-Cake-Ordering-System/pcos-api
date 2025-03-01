using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_decorations")]
public class CakeDecoration : BaseEntity
{
    [Column("decoration_name")]
    public string DecorationName { get; set; } = default!;
    [Column("decoration_price")]
    public double DecorationPrice { get; set; } = 0;

    [Column("decoration_type")]
    public string DecorationType { get; set; } = default!;

    [Column("decoration_description")]
    public string? DecorationDescription { get; set; }

    [Column("decoration_color")]
    public string? DecorationColor { get; set; }


    [Column("decoration_image_id")]
    public Guid? DecorationImageId { get; set; }
    public Storage? DecorationImage { get; set; }


    public Guid BakeryId { get; set; } = default!;

    public ICollection<CakeDecorationDetail>? CakeDecorationDetails { get; set; }

}