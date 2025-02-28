using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("bakeries")]
    public class Bakery : BaseEntity
    {
        [Column("bakery_name")]
        public string BakeryName { get; set; } = default!;
        [Column("email")]
        public string Email { get; set; } = default!;
        [Column("password")]
        public string Password { get; set; } = default!;

        [Column("phone")]
        public string Phone { get; set; } = default!;
        [Column("address")]
        public string Address { get; set; } = default!;
        [Column("owner_name")]
        public string OwnerName { get; set; } = default!;
        [Column("avatar_file_id")]
        public Guid? AvatarFileId { get; set; } = default!;
        public Storage? AvatarFile { get; set; } = default!;
        [Column("identity_card_number")]
        public string IdentityCardNumber { get; set; } = default!;
        [Column("font_card_file_id")]
        public Guid FrontCardFileId { get; set; } = default!;
        public Storage FrontCardFile { get; set; } = default!;

        [Column("back_card_file_id")]
        public Guid BackCardFileId { get; set; } = default!;
        public Storage BackCardFile { get; set; } = default!;

        [Column("tax_code")]
        public string TaxCode { get; set; } = default!;

        [Column("status")]
        public string Status { get; set; } = default!;

        [Column("confirmed_at")]
        public DateTime ConfirmedAt { get; set; } = default!;

        [Column("shop_image_files")]
        public List<Guid> ShopImageFiles { get; set; } = new List<Guid>()!;
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<CustomCake>? CustomCakes { get; set; }
        public ICollection<AvailableCake>? AvailableCakes { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CakeReview>? CakeReviews { get; set; }
        public ICollection<OrderSupport>? OrderSupports { get; set; }
        public ICollection<Voucher>? Vouchers { get; set; }

    }
}