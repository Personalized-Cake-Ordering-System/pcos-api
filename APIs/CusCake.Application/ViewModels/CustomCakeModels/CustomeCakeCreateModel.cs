using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels.CustomCakeModels;

public class CustomCakeCreateModel
{
    [JsonPropertyName("cake_name")]
    public string CakeName { get; set; } = default!;

    [JsonPropertyName("cake_description")]
    public string? CakeDecorationDetail { get; set; }

    [JsonPropertyName("bakery_id")]
    public Guid BakeryId { get; set; }

}

public class CakeMessageDetail
{
    public Guid CakeMessageId { get; set; }
    public Guid? MessageImageId { get; set; } = default!;
    public string? Message { get; set; }
    public string MessageType { get; set; } = default!;
}