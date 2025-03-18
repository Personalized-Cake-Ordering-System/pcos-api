namespace CusCake.Application.ViewModels.TransactionModels;


public class TransactionWebhookModel
{
    public int Id { get; set; }
    public string? Gateway { get; set; }
    public string? TransactionDate { get; set; }
    public string? AccountNumber { get; set; }
    public string? Code { get; set; }
    public string? Content { get; set; }
    public string? TransferType { get; set; }
    public string? Description { get; set; }
    public double TransferAmount { get; set; }
    public double Accumulated { get; set; }
    public string? SubAccount { get; set; }
    public string? ReferenceCode { get; set; }
}

