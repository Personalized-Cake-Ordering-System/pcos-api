using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_message_details")]
public class CakeMessageDetail : BaseEntity
{
    [Column("cake_message_id")]
    public Guid CakeMessageId { get; set; }
    public CakeMessage CakeMessage { get; set; } = default!;

    [Column("custom_cake_id")]
    public Guid CustomCakeId { get; set; }
    public CustomCake CustomCake { get; set; } = default!;

    [Column("message_image_id")]
    public Guid? MessageImageId { get; set; } = default!;
    public Storage? MessageImageFile { get; set; }

    [Column("message")]
    public string? Message { get; set; }

    [Column("message_type_name")]
    public string? TypeName { get; set; } = default!;

    [Column("message_type_color")]
    public string? TypeColor { get; set; } = default!;

    [Column("message_type")]
    public string MessageType { get; set; } = default!;
}