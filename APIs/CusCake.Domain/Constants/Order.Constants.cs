namespace CusCake.Domain.Constants;

public static class OrderStatusConstants
{
    public const string PENDING = "PENDING";                    // Đơn hàng mới được tạo, chưa xác nhận
    public const string WAITING_BAKERY = "WAITING_BAKERY_CONFIRM";      // Chờ bakery xác nhận
    public const string PROCESSING = "PROCESSING";              // Bakery đang xử lý đơn hàng
    public const string READY_FOR_PICKUP = "READY_FOR_PICKUP";  // Bakery đã hoàn thành, sẵn sàng giao
    public const string SHIPPING = "SHIPPING";                  // Đang giao hàng
    public const string COMPLETED = "COMPLETED";                // Khách hàng đã xác nhận DONE
    public const string CANCELED = "CANCELED";                  // Hủy đơn
}

public static class OrderConstants
{
    public const double COMMISSION_RATE = 0.08;

}

public static class ShippingTypeConstants
{
    public const string DELIVERY = "DELIVERY";
    public const string PICK_UP = "PICKUP";
}

public static class PaymentTypeConstants
{
    public const string QR_CODE = "QR_CODE";
    public const string CASH = "CASH";
}