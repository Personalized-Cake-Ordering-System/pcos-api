using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CusCake.Domain.Constants;

namespace CusCake.Domain.Entities;

[Table("reports")]
public class Report : BaseEntity
{
    [JsonPropertyName("content")]
    [Column("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("reject_reason")]
    [Column("reject_reason")]
    public string? RejectReason { get; set; }

    [JsonPropertyName("type")]
    [Column("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("status")]
    [Column("status")]
    public string Status { get; set; } = ReportStatusConstants.PENDING;

    [Column("report_files")]
    [JsonPropertyName("report_files")]
    public List<Storage>? ReportFiles { get; set; }

    [JsonPropertyName("customer_id")]
    [Column("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = default!;

    [JsonPropertyName("order_id")]
    [Column("order_id")]
    public Guid? OrderId { get; set; }

    [JsonPropertyName("order")]
    public Order? Order { get; set; } = default!;

    [Column("bakery_id")]
    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("bakery")]
    public Bakery Bakery { get; set; } = default!;
}