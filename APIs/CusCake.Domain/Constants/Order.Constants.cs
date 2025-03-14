namespace CusCake.Domain.Constants;

public static class OrderStatusConstants
{
    public const string PENDING = "PENDING";                    // Đơn hàng mới được tạo, chưa xác nhận
    public const string CONFIRMED = "CONFIRMED";                // Đã xác nhận đơn hàng, chờ thanh toán
    public const string PAYMENT_PENDING = "PAYMENT_PENDING";    // Đang chờ thanh toán (QR Code hiển thị)
    public const string PAID = "PAID";                          // Đã thanh toán, chờ xử lý tại bakery
    public const string PROCESSING = "PROCESSING";              // Bakery đang xử lý đơn hàng
    public const string READY_FOR_PICKUP = "READY_FOR_PICKUP";  // Bakery đã hoàn thành, sẵn sàng giao
    public const string CUSTOMER_CONFIRMED = "CUSTOMER_CONFIRMED"; // Khách hàng xác nhận bánh hợp lệ
    public const string SHIPPING = "SHIPPING";                  // Đang giao hàng
    public const string COMPLETED = "COMPLETED";                // Khách hàng đã xác nhận DONE
    public const string AUTO_COMPLETED = "AUTO_COMPLETED";      // Tự động hoàn thành nếu khách không xác nhận sau X phút
    public const string CANCELED_BY_CUSTOMER = "CANCELED_BY_CUSTOMER"; // Khách hàng hủy đơn
    public const string CANCELED_BY_BAKERY = "CANCELED_BY_BAKERY";     // Bakery hủy đơn

}

public static class OrderConstants
{
    public const double TAX_RATE = 0.08;


}
