
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("customers")]
    public class Customer : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = default!;
        [Column("email")]
        public string Email { get; set; } = default!;
        [Column("phone")]
        public string Phone { get; set; } = default!;
        [Column("address")]
        public string Address { get; set; } = default!;
    }
}
