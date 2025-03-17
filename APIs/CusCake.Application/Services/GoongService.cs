using Newtonsoft.Json.Linq;

namespace CusCake.Application.Services;


public interface IGoongService
{
    public Task<(double distanceKm, double estimatedDeliveryTime)> GetShippingInfoAsync(
        string orgLat, string orgLng, string destLat, string destLng, string travelMode = "car");
}

public class GoongService(AppSettings appSettings) : IGoongService
{
    private const string BASE_URL = "https://rsapi.goong.io";
    private readonly AppSettings _appSettings = appSettings;

    public async Task<(double distanceKm, double estimatedDeliveryTime)> GetShippingInfoAsync(
        string orgLat, string orgLng, string destLat, string destLng, string travelMode = "car")
    {
        using HttpClient httpClient = new();
        string requestUrl = $"{BASE_URL}/direction?origin={orgLat},{orgLng}&destination={destLat},{destLng}&vehicle={travelMode}&api_key={_appSettings.GoongAPIKey}";

        using var response = await httpClient.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var jsonObject = JObject.Parse(jsonResponse); // Chuyển JSON thành JObject

        // Lấy khoảng cách (mét → km)
        double distanceKm = (jsonObject["routes"]?[0]?["legs"]?[0]?["distance"]?["value"]?.Value<double>() ?? 0) / 1000.0;

        // Lấy thời gian vận chuyển (giây → giờ)
        double shippingTimeH = (jsonObject["routes"]?[0]?["legs"]?[0]?["duration"]?["value"]?.Value<double>() ?? 0) / 3600.0;

        return (distanceKm, shippingTimeH);
    }
}
