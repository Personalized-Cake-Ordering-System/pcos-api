using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("transactions")]
public class Transaction : BaseEntity
{
    [JsonPropertyName("amount")]
    [Column("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("order_id")]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [JsonPropertyName("order")]
    public Order Order { get; set; } = default!;
    [Column("account_number")]
    [JsonPropertyName("account_number")]
    public string AccountNumber { get; set; } = default!;

    [Column("sub_account")]
    [JsonPropertyName("sub_account")]
    public string? SubAccount { get; set; }

    [Column("code")]
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [Column("content")]
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [Column("transfer_type")]
    [JsonPropertyName("transfer_type")]
    public string TransferType { get; set; } = default!;

    [Column("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [Column("transfer_amount")]
    [JsonPropertyName("transfer_amount")]
    public double TransferAmount { get; set; }

    [Column("reference_code")]
    [JsonPropertyName("reference_code")]
    public string ReferenceCode { get; set; } = default!;

    [Column("accumulated")]
    [JsonPropertyName("accumulated")]
    public double Accumulated { get; set; }

}
