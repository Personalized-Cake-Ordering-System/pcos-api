using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities;

[Table("bank_events")]
public class BankEvent : BaseEntity
{
    [Column("transaction_id")]
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = default!;

    [Column("gateway")]
    public string Gateway { get; set; } = default!;

    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }

    [Column("account_number")]
    public string AccountNumber { get; set; } = default!;

    [Column("sub_account")]
    public string? SubAccount { get; set; }

    [Column("code")]
    public string Code { get; set; } = default!;

    [Column("content")]
    public string Content { get; set; } = default!;

    [Column("transfer_type")]
    public string TransferType { get; set; } = default!;

    [Column("description")]
    public string Description { get; set; } = default!;

    [Column("tranfer_amount")]
    public double TransferAmount { get; set; }

    [Column("reference_code")]
    public string ReferenceCode { get; set; } = default!;

    [Column("accumulated")]
    public double Accumulated { get; set; }

    [Column("is_processed")]
    public bool IsProcessed { get; set; }

    [Column("processed_at")]
    public DateTime? ProcessedAt { get; set; }

}
