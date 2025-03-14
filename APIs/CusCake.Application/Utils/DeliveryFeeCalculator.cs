
namespace CusCake.Application.Utils;
public static class DeliveryFeeCalculator
{
    // Danh sách bảng giá động (có thể dễ dàng chỉnh sửa)
    private static readonly List<Tuple<decimal, decimal>> FeeTable =
    [
        Tuple.Create(3m, 30000m),
        Tuple.Create(6m, 35000m),
        Tuple.Create(7m, 40000m),
        Tuple.Create(8m, 45000m),
        Tuple.Create(9m, 50000m),
        Tuple.Create(10m, 55000m),
        Tuple.Create(12m, 65000m),
        Tuple.Create(13m, 75000m)
    ];

    private const decimal Over13KmRate = 6000m; // 6.000 VNĐ/km

    public static decimal CalculateFee(decimal distanceInKm)
    {
        foreach (var (maxDistance, fee) in FeeTable)
        {
            if (distanceInKm <= maxDistance)
                return fee;
        }

        // Nếu trên 13km, tính theo công thức động
        decimal baseFee = FeeTable[^1].Item2; // Lấy mức phí cuối cùng (75000)
        decimal extraDistance = distanceInKm - 13;
        return baseFee + extraDistance * Over13KmRate;
    }
}