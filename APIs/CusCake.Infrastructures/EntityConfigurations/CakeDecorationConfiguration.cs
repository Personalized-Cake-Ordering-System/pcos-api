using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeDecorationConfiguration : IEntityTypeConfiguration<CakeDecoration>
{
    public void Configure(EntityTypeBuilder<CakeDecoration> builder)
    {
        builder.HasMany(x => x.CakeDecorationDetails).WithOne(x => x.CakeDecoration).HasForeignKey(x => x.CakeDecorationId).OnDelete(DeleteBehavior.Cascade);

        builder
           .HasOne(c => c.DecorationImage)
           .WithMany()
           .HasForeignKey(c => c.DecorationImageId)
           .OnDelete(DeleteBehavior.SetNull);

    }
}