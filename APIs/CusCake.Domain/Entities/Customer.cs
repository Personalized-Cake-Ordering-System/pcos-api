
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("customers")]
    public class Customer:BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
