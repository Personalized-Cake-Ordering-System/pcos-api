namespace CusCake.Domain.Constants;

public static class WalletTransactionTypeConstants
{
    // Admin related transactions
    public const string ADMIN_HOLD_PAYMENT = "ADMIN_HOLD_PAYMENT";          // Admin tạm giữ tiền khi khách thanh toán
    public const string ADMIN_TRANSFER_TO_BAKERY = "ADMIN_TRANSFER_TO_BAKERY"; // Admin chuyển tiền cho bakery khi đơn hoàn thành
    public const string ADMIN_REFUND_TO_CUSTOMER = "ADMIN_REFUND_TO_CUSTOMER"; // Admin hoàn tiền cho khách khi đơn bị hủy

    // Bakery related transactions
    public const string BAKERY_RECEIVE_PAYMENT = "BAKERY_RECEIVE_PAYMENT";    // Bakery nhận tiền từ admin khi đơn hoàn thành
    public const string BAKERY_WITHDRAWAL = "BAKERY_WITHDRAWAL";              // Bakery rút tiền về tài khoản

    // Customer related transactions
    public const string CUSTOMER_PAYMENT = "CUSTOMER_PAYMENT";                // Customer thanh toán đơn hàng
    public const string CUSTOMER_REFUND = "CUSTOMER_REFUND";                 // Customer nhận hoàn tiền khi đơn bị hủy
    public const string CUSTOMER_WITHDRAWAL = "CUSTOMER_WITHDRAWAL";           // Customer rút tiền từ ví về tài khoản ngân hàng

    private static readonly Dictionary<string, (string Title, string Content)> TransactionDetails =
        new()
        {
            {
                ADMIN_HOLD_PAYMENT,
                ("Tạm giữ thanh toán", "Hệ thống tạm giữ {Amount}đ từ đơn hàng #{OrderCode}")
            },
            {
                ADMIN_TRANSFER_TO_BAKERY,
                ("Chuyển tiền cho cửa hàng", "Chuyển {Amount}đ đến cửa hàng {BakeryName} cho đơn hàng #{OrderCode}")
            },
            {
                ADMIN_REFUND_TO_CUSTOMER,
                ("Hoàn tiền cho khách hàng", "Hoàn {Amount}đ cho khách hàng {CustomerName} từ đơn hàng #{OrderCode}")
            },
            {
                BAKERY_RECEIVE_PAYMENT,
                ("Nhận thanh toán", "Nhận {Amount}đ từ đơn hàng #{OrderCode}")
            },
            {
                BAKERY_WITHDRAWAL,
                ("Rút tiền", "Rút {Amount}đ từ ví")
            },
            {
                CUSTOMER_PAYMENT,
                ("Thanh toán đơn hàng", "Thanh toán {Amount}đ cho đơn hàng #{OrderCode}")
            },
            {
                CUSTOMER_REFUND,
                ("Nhận hoàn tiền", "Nhận hoàn {Amount}đ từ đơn hàng #{OrderCode}")
            },
            {
                CUSTOMER_WITHDRAWAL,
                ("Rút tiền", "Rút {Amount}đ từ ví")
            }
        };

    public static string GetTitleByType(string type)
    {
        return TransactionDetails.TryGetValue(type, out var details) ? details.Title : "Giao dịch";
    }

    public static string GetContentByType(string type)
    {
        return TransactionDetails.TryGetValue(type, out var details) ? details.Content : "Nội dung không xác định";
    }
}
