
namespace CusCake.Application.Utils;

public static class DeliveryFeeCalculator
{
    // Danh sách bảng giá động (có thể dễ dàng chỉnh sửa)
    private static readonly List<Tuple<double, double>> FeeTable =
    [
        Tuple.Create(3.0, 30000.0),
        Tuple.Create(6.0, 35000.0),
        Tuple.Create(7.0, 40000.0),
        Tuple.Create(8.0, 45000.0),
        Tuple.Create(9.0, 50000.0),
        Tuple.Create(10.0, 55000.0),
        Tuple.Create(12.0, 65000.0),
        Tuple.Create(13.0, 75000.0)
    ];

    private const double Over13KmRate = 6000.0; // 6.000 VNĐ/km

    public static double CalculateFee(double distanceInKm)
    {
        foreach (var (maxDistance, fee) in FeeTable)
        {
            if (distanceInKm <= maxDistance)
                return fee;
        }

        // Nếu trên 13km, tính theo công thức động
        double baseFee = FeeTable[^1].Item2; // Lấy mức phí cuối cùng (75000)
        double extraDistance = distanceInKm - 13.0;
        return baseFee + extraDistance * Over13KmRate;
    }
}