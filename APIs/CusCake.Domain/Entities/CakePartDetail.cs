using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_part_details")]
public class CakePartDetail : BaseEntity
{


    [Column("custom_cake_id")]
    public Guid CustomCakeId { get; set; }
    public CustomCake CustomCake { get; set; } = default!;
    [Column("cake_part_id")]
    public Guid CakePartId { get; set; }
    public CakePart CakePart { get; set; } = default!;
}