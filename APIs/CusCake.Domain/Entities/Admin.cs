
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("admins")]
    public class Admin : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = default!;

        [Column("email")]
        public string Email { get; set; } = default!;
        [Column("password")]
        public string Password { get; set; } = default!;
    }
}
