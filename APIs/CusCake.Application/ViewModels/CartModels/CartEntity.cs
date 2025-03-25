using System.Text.Json.Serialization;
using CusCake.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CusCake.Application.ViewModels.CartModels;

public class CartActionModel
{
    // [JsonPropertyName("bakery_id")]
    [BsonElement("bakery_id")]
    public Guid BakeryId { get; set; }

    [JsonPropertyName("order_note")]
    [BsonElement("order_note")]
    public string? OrderNote { get; set; }

    [JsonPropertyName("phone_number")]
    [BsonElement("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("shipping_address")]
    [BsonElement("shipping_address")]
    public string? ShippingAddress { get; set; }

    [JsonPropertyName("latitude")]
    [BsonElement("latitude")]
    public string? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    [BsonElement("longitude")]
    public string? Longitude { get; set; }

    [JsonPropertyName("pickup_time")]
    [BsonElement("pickup_time")]
    public DateTime? PickUpTime { get; set; } = DateTime.Now;

    [JsonPropertyName("shipping_type")]
    [BsonElement("shipping_type")]
    public string? ShippingType { get; set; } = default!;

    [JsonPropertyName("payment_type")]
    [BsonElement("payment_type")]
    public string? PaymentType { get; set; } = default!;

    [JsonPropertyName("voucher_code")]
    [BsonElement("voucher_code")]
    public string? VoucherCode { get; set; }

    [BsonElement("items")]
    public List<CartItem>? CartItems { get; set; }

}

public class CartItem
{

    [BsonElement("cake_name")]
    [JsonPropertyName("cake_name")]
    public string CakeName { get; set; } = default!;

    [BsonElement("main_image_id")]
    [JsonPropertyName("main_image_id")]
    public Guid? MainImageId { get; set; }

    [BsonElement("main_image")]
    [JsonPropertyName("main_image")]
    public Storage? CakeMainImage { get; set; }

    // Order detail fields

    [BsonElement("quantity")]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; } = 1;

    [BsonElement("cake_note")]
    [JsonPropertyName("cake_note")]
    public string? CakeNote { get; set; }

    [JsonPropertyName("sub_total_price")]
    [BsonElement("sub_total_price")]
    public double? SubTotalPrice { get; set; } = 0;

    [BsonElement("available_cake_id")]
    [JsonPropertyName("available_cake_id")]
    public Guid? AvailableCakeId { get; set; }

    [BsonElement("custom_cake_id")]
    [JsonPropertyName("custom_cake_id")]
    public Guid? CustomCakeId { get; set; }

}

public class CartEntity : CartActionModel
{
    // [JsonPropertyName("customer_id")]
    [BsonId]
    public Guid CustomerId { get; set; } = Guid.NewGuid();
}