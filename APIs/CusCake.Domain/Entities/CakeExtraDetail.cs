using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_extra_details")]
public class CakeExtraDetail : BaseEntity
{

    [Column("custom_cake_id")]
    public Guid CustomCakeId { get; set; }
    public CustomCake CustomCake { get; set; } = default!;
    [Column("cake_extra_id")]
    public Guid CakeExtraId { get; set; }
    public CakeExtra CakeExtra { get; set; } = default!;
}