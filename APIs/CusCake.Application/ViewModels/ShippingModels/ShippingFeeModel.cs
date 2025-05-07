using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Application.ViewModels.ShippingModels;

[Table("shipping_fee")]
public class ShippingFeeModel
{
    [JsonPropertyName("shipping_time")]
    public double ShippingTime { get; set; }
    [JsonPropertyName("shipping_distance")]
    public double ShippingDistance { get; set; }
    [JsonPropertyName("shipping_fee")]
    public double ShippingFee { get; set; }
}