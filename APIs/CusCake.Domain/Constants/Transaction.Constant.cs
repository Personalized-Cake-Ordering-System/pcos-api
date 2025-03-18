namespace CusCake.Domain.Constants;

public static class WalletTransactionTypeConstants
{
    public const string PENDING_PAYMENT = "PENDING_PAYMENT"; // Tiền tạm giữ trong ví admin
    public const string SHOP_REVENUE_TRANSFER = "SHOP_REVENUE_TRANSFER"; // Tiền doanh thu chuyển từ ví admin sang ví bakery
    public const string ADMIN_TO_BAKERY_TRANSFER = "ADMIN_TO_BAKERY_TRANSFER"; // Tiền chuyển từ ví admin qua ví bakery
    public const string WITHDRAWAL = "WITHDRAWAL";           // Rút tiền
    public const string TRANSFER = "TRANSFER";               // Chuyển tiền
    public const string REFUND = "REFUND";                   // Hoàn tiền
}