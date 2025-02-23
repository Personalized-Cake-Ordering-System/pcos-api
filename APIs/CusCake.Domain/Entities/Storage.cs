using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    [Table("storages")]
    public class Storage : BaseEntity
    {
        [Column("file_name")]
        public string FileName { get; set; } = default!;
        [Column("file_url")]
        public string FileUrl { get; set; } = default!;
    }

}