using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeDecorationDetailConfiguration : IEntityTypeConfiguration<CakeDecorationDetail>
{
    public void Configure(EntityTypeBuilder<CakeDecorationDetail> builder)
    {
        builder.HasOne(x => x.CakeDecoration).WithMany(x => x.CakeDecorationDetails).HasForeignKey(x => x.CakeDecorationId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CustomCake).WithMany(x => x.CakeDecorationDetails).HasForeignKey(x => x.CakeDecorationId).OnDelete(DeleteBehavior.Cascade);

    }
}