namespace CusCake.Domain.Constants;

public static class NotificationType
{
    public const string MAKING_BILLING = "MAKING_BILLING";
    public const string PAYMENT_SUCCESS = "PAYMENT_SUCCESS";
    public const string NEW_ORDER = "NEW_ORDER";
    public const string PROCESSING_ORDER = "PROCESSING_ORDER";
    public const string SHIPPING_ORDER = "SHIPPING_ORDER";
    public const string READY_FOR_PICKUP = "READY_FOR_PICKUP";
    public const string ORDER_COMPLETED = "ORDER_COMPLETED";


    private static readonly Dictionary<string, (string Title, string Content)> NotificationDetails =
        new()
        {
            {
                PAYMENT_SUCCESS,
                ("Thanh toán thành công", "Đơn hàng của bạn đã được thanh toán thành công. Cảm ơn bạn đã mua hàng!")
            },
            {
                MAKING_BILLING,
                ("Đang tạo hóa đơn", "Hệ thống đang xử lý hóa đơn cho đơn hàng của bạn. Vui lòng chờ giây lát.")
            },
            {
                NEW_ORDER,
                ("Bạn có đơn hàng mới", "Một khách hàng vừa đặt hàng. Hãy kiểm tra và xác nhận ngay!")
            },
            {
                PROCESSING_ORDER,
                ("Đơn hàng đang xử lý", "Đơn hàng của bạn đang được chuẩn bị. Chúng tôi sẽ cập nhật sớm nhất!")
            },
            {
                SHIPPING_ORDER,
                ("Đơn hàng đang vận chuyển", "Đơn hàng đang trên đường đến bạn. Hãy theo dõi tình trạng giao hàng.")
            },
            {
                ORDER_COMPLETED,
                ("Đơn hàng hoàn tất", "Cảm ơn bạn đã mua hàng! Nếu có bất kỳ vấn đề nào, vui lòng liên hệ hỗ trợ.")
            },
            {
                READY_FOR_PICKUP,
                ("Đơn hàng hoàn tất", "Vui lòng đến lấy tại quầy!")
            }
        };


    public static string GetTitleByType(string type)
    {
        return NotificationDetails.TryGetValue(type, out var details) ? details.Title : "Thông báo";
    }

    public static string GetContentByType(string type)
    {
        return NotificationDetails.TryGetValue(type, out var details) ? details.Content : "Nội dung không xác định";
    }
}

public static class NotificationSenderType
{
    public const string SYSTEM = "SYSTEM";
}

