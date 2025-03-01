﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CusCake.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("created_by")]
        public Guid? CreatedBy { get; set; } = Guid.Empty;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = null;

        [Column("updated_by")]
        public Guid? UpdatedBy { get; set; } = default!;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
