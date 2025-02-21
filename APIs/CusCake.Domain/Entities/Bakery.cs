using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("bakeries")]
    public class Bakery : BaseEntity
    {
        [Column("shop_name")]
        public string ShopName { get; set; } = default!;
        [Column("owner_name")]
        public string OwnerName { get; set; } = default!;
        [Column("email")]
        public string Email { get; set; } = default!;
        [Column("phone")]
        public string Phone { get; set; } = default!;
        [Column("address")]
        public string Address { get; set; } = default!;
        [Column("identity_card_number")]
        public string IdentityCardNumber { get; set; } = default!;
        [Column("font_id_card_file")]
        public Guid FrontIdCardFile { get; set; } = default!;
        [Column("back_id_card_file")]
        public Guid BackIdCardFile { get; set; } = default!;
        [Column("tax_code")]
        public string TaxCode { get; set; } = default!;
        [Column("shop_image_files")]
        public List<Guid> ShopImageFiles { get; set; } = default!;
    }
}