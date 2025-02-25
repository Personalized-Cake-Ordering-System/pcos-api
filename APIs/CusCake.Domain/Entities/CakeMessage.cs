using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_messages")]
public class CakeMessage : BaseEntity
{
    [Column("message_image_id")]
    public Guid MessageImageId { get; set; } = default!;
    [Column("message_color")]
    public string? MessageColor { get; set; }
    [Column("message")]
    public string? Message { get; set; }

    [Column("message_type")]
    public string MessageType { get; set; } = default!;

    [Column("message_description")]
    public string? MessageDescription { get; set; }

    [Column("custom_cake_id")]
    public Guid CustomCakeId { get; set; }
    public CustomCake CustomCake { get; set; } = default!;

}