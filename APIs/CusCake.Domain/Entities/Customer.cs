
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("customers")]
    public class Customer : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = default!;

        [Column("phone")]
        public string? Phone { get; set; } = default!;
        [Column("address")]
        public string? Address { get; set; } = default!;

        [Column("email")]
        public string Email { get; set; } = default!;
        [Column("password")]
        public string Password { get; set; } = default!;

        [Column("account_type")]
        public string AccountType { get; set; } = default!;
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<CustomCake>? CustomCakes { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CakeReview>? CakeReviews { get; set; }
        public ICollection<OrderSupport>? OrderSupports { get; set; }
        public ICollection<CustomerVoucher>? CustomerVouchers { get; set; }

    }
}
