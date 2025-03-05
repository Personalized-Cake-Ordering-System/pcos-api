using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("cake_messages")]
public class CakeMessage : BaseEntity
{

    [Column("message_name")]
    public string MessageName { get; set; } = default!;
    [Column("message_image_id")]
    public Guid? MessageImageId { get; set; } = default!;
    public Storage? MessageImage { get; set; }

    [Column("message_price")]
    public double MessagePrice { get; set; } = 0;

    [Column("message_type")]
    public string MessageType { get; set; } = default!;

    [Column("message_description")]
    public string? MessageDescription { get; set; }
    public Guid BakeryId { get; set; } = default!;

    public ICollection<CakeMessageType>? CakeMessageTypes { get; set; }

    public ICollection<CakeMessageDetail>? CakeMessageDetails { get; set; }

}


[Table("cake_message_types")]
public class CakeMessageType : BaseEntity
{
    [Column("message_type")]
    public string Type { get; set; } = default!;

    [Column("message_name")]
    public string Name { get; set; } = default!;

    [Column("message_color")]
    public string? Color { get; set; } = default!;

    [Column("cake_message_id")]
    public Guid CakeMessageId { get; set; }

    public CakeMessage CakeMessage { get; set; } = default!;

}