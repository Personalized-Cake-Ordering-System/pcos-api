using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities;

[Table("bank_events")]
public class BankEvent : BaseEntity
{
    [Column("transaction_id")]
    [JsonPropertyName("transaction_id")]
    public Guid TransactionId { get; set; }

    [JsonPropertyName("transaction")]
    public Transaction Transaction { get; set; } = default!;

    [Column("gateway")]
    [JsonPropertyName("gateway")]
    public string Gateway { get; set; } = default!;

    [Column("transaction_date")]
    [JsonPropertyName("transaction_date")]
    public DateTime TransactionDate { get; set; }

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

    [Column("tranfer_amount")]
    [JsonPropertyName("transfer_amount")]
    public double TransferAmount { get; set; }

    [Column("reference_code")]
    [JsonPropertyName("reference_code")]
    public string ReferenceCode { get; set; } = default!;

    [Column("accumulated")]
    [JsonPropertyName("accumulated")]
    public double Accumulated { get; set; }

    [Column("is_processed")]
    [JsonPropertyName("is_processed")]
    public bool IsProcessed { get; set; }

    [Column("processed_at")]
    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; set; }
}