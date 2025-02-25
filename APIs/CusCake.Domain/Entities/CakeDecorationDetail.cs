using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_decoration_details")]
public class CakeDecorationDetail : BaseEntity
{

    [Column("custom_cake_id")]
    public Guid CustomCakeId { get; set; }
    public CustomCake CustomCake { get; set; } = default!;

    [Column("cake_extra_id")]
    public Guid CakeDecorationId { get; set; }
    public CakeDecoration CakeDecoration { get; set; } = default!;
}