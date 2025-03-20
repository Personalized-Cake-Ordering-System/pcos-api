using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace CusCake.Application.ViewModels.CartModels;


public class CartEntity
{
    [BsonId]
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = new Guid();

    [JsonPropertyName("customer_id")]
    [BsonElement("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("bakery_id")]
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
    public string ShippingType { get; set; } = default!;

    [JsonPropertyName("payment_type")]
    [BsonElement("payment_type")]
    public string PaymentType { get; set; } = default!;

    [JsonPropertyName("voucher_code")]
    [BsonElement("voucher_code")]
    public string? VoucherCode { get; set; }

    // [BsonElement("items")]
    // public List<Item> Items { get; set; }  // Danh sách các mặt hàng trong giỏ hàng

}