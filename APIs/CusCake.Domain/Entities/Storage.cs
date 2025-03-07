using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CusCake.Domain.Entities
{
    [Table("storages")]
    public class Storage : BaseEntity
    {
        [JsonPropertyName("file_name")]
        [Column("file_name")]
        public string FileName { get; set; } = default!;

        [JsonPropertyName("file_url")]
        [Column("file_url")]
        public string FileUrl { get; set; } = default!;
    }
}
