using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeExtraConfiguration : IEntityTypeConfiguration<CakeExtra>
{
    public void Configure(EntityTypeBuilder<CakeExtra> builder)
    {
        builder.HasMany(x => x.CakeExtraDetails).WithOne(x => x.CakeExtra).HasForeignKey(x => x.CakeExtraId).OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(c => c.ExtraImage)
            .WithMany()
            .HasForeignKey(c => c.ExtraImageId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}