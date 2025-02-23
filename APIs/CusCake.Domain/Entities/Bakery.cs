using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("bakeries")]
    public class Bakery : BaseEntity
    {
        [Column("shop_name")]
        public string ShopName { get; set; } = default!;
        [Column("email")]
        public string Email { get; set; } = default!;
        [Column("phone")]
        public string Phone { get; set; } = default!;
        [Column("address")]
        public string Address { get; set; } = default!;
        [Column("owner_name")]
        public string OwnerName { get; set; } = default!;
        [Column("avatar_file_id")]
        public Guid AvatarFileId { get; set; } = default!;
        [Column("identity_card_number")]
        public string IdentityCardNumber { get; set; } = default!;
        [Column("font_card_file_id")]
        public Guid FrontCardFileId { get; set; } = default!;
        [Column("back_card_file_id")]
        public Guid BackCardFileId { get; set; } = default!;
        [Column("tax_code")]
        public string TaxCode { get; set; } = default!;
        [Column("shop_image_files")]
        public List<Guid> ShopImageFiles { get; set; } = default!;
    }
}