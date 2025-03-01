using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakePartConfiguration : IEntityTypeConfiguration<CakePart>
{
    public void Configure(EntityTypeBuilder<CakePart> builder)
    {
        builder.HasMany(x => x.CakePartDetails).WithOne(x => x.CakePart).HasForeignKey(x => x.CakePartId).OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(c => c.PartImage)
            .WithMany()
            .HasForeignKey(c => c.PartImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}