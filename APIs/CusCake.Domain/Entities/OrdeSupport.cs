using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("order_supports")]
public class OrderSupport : BaseEntity
{
    [JsonPropertyName("content")]
    [Column("content")]
    public string? Content { get; set; }

    [JsonPropertyName("file_id")]
    [Column("file_id")]
    public Guid? FileId { get; set; }

    [JsonPropertyName("file")]
    public Storage? File { get; set; }


    [JsonPropertyName("bakery_id")]
    [Column("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;

    [JsonPropertyName("customer_id")]
    [Column("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = default!;

    [JsonPropertyName("order_id")]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [ForeignKey("OrderId")]
    [JsonPropertyName("order")]
    public Order Order { get; set; } = default!;
}
