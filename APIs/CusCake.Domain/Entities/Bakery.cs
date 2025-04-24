using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("bakeries")]
    public class Bakery : BaseEntity
    {
        [Column("bakery_name")]
        [JsonPropertyName("bakery_name")]
        public string BakeryName { get; set; } = default!;

        [Column("cake_description")]
        [JsonPropertyName("cake_description")]
        public string? CakeDescription { get; set; } = default!;

        [Column("price_description")]
        [JsonPropertyName("price_description")]
        public string? PriceDescription { get; set; } = default!;

        [Column("bakery_description")]
        [JsonPropertyName("bakery_description")]
        public string? BakeryDescription { get; set; } = default!;

        [Column("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = default!;

        [Column("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = default!;

        [Column("phone")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = default!;

        [Column("address")]
        [JsonPropertyName("address")]
        public string Address { get; set; } = default!;

        [JsonPropertyName("latitude")]
        [Column("latitude")]
        public string Latitude { get; set; } = default!;

        [JsonPropertyName("longitude")]
        [Column("longitude")]
        public string Longitude { get; set; } = default!;

        [JsonPropertyName("bank_account")]
        [Column("bank_account")]
        public string? BankAccount { get; set; } = default!;

        [Column("owner_name")]
        [JsonPropertyName("owner_name")]
        public string OwnerName { get; set; } = default!;

        [Column("avatar_file_id")]
        [JsonPropertyName("avatar_file_id")]
        public Guid? AvatarFileId { get; set; } = default!;

        [JsonPropertyName("avatar_file")]
        public Storage? AvatarFile { get; set; } = default!;

        [Column("identity_card_number")]
        [JsonPropertyName("identity_card_number")]
        public string IdentityCardNumber { get; set; } = default!;

        [Column("font_card_file_id")]
        [JsonPropertyName("front_card_file_id")]
        public Guid FrontCardFileId { get; set; } = default!;

        [JsonPropertyName("front_card_file")]
        public Storage FrontCardFile { get; set; } = default!;

        [Column("back_card_file_id")]
        [JsonPropertyName("back_card_file_id")]
        public Guid BackCardFileId { get; set; } = default!;

        [JsonPropertyName("back_card_file")]
        public Storage BackCardFile { get; set; } = default!;

        [Column("tax_code")]
        [JsonPropertyName("tax_code")]
        public string TaxCode { get; set; } = default!;

        [Column("status")]
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;

        [Column("confirmed_at")]
        [JsonPropertyName("confirmed_at")]
        public DateTime ConfirmedAt { get; set; } = default!;

        [Column("banned_at")]
        [JsonPropertyName("banned_at")]
        public DateTime? BannedAt { get; set; } = default!;

        [Column("shop_image_files")]
        [JsonPropertyName("shop_image_files")]
        public List<Storage>? ShopImageFiles { get; set; }

        [JsonPropertyName("metric")]
        public BakeryMetric? Metric { get; set; } = default!;

        [JsonPropertyName("reviews")]
        public ICollection<Review>? Reviews { get; set; }

        [JsonPropertyName("distance_to_user")]
        public double? DistanceToUser { get; set; }
    }
}
