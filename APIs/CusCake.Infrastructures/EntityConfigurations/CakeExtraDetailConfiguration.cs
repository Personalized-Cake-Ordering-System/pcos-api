using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeExtraDetailConfiguration : IEntityTypeConfiguration<CakeExtraDetail>
{
    public void Configure(EntityTypeBuilder<CakeExtraDetail> builder)
    {
        builder.HasOne(x => x.CakeExtra).WithMany(x => x.CakeExtraDetails).HasForeignKey(x => x.CakeExtraId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CustomCake).WithMany(x => x.CakeExtraDetails).HasForeignKey(x => x.CakeExtraId).OnDelete(DeleteBehavior.Cascade);

    }
}