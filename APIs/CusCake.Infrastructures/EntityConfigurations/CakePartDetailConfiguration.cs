using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakePartDetailConfiguration : IEntityTypeConfiguration<CakePartDetail>
{
    public void Configure(EntityTypeBuilder<CakePartDetail> builder)
    {
        builder.HasOne(x => x.CakePart).WithMany(x => x.CakePartDetails).HasForeignKey(x => x.CakePartId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CustomCake).WithMany(x => x.CakePartDetails).HasForeignKey(x => x.CakePartId).OnDelete(DeleteBehavior.Cascade);

    }
}