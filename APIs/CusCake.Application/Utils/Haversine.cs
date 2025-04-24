using System;
namespace CusCake.Application.Utils;
public static class Haversine
{
    private const double RadiusOfEarth = 6371; // Bán kính trái đất tính bằng km

    public static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Tính toán sự khác biệt giữa các vĩ độ và kinh độ
        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);

        // Áp dụng công thức Haversine
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // Tính khoảng cách (trả về kết quả bằng km)
        double distance = RadiusOfEarth * c;

        return distance;
    }
}
