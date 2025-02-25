using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class AvailableCakeConfiguration : IEntityTypeConfiguration<AvailableCake>
{
    public void Configure(EntityTypeBuilder<AvailableCake> builder)
    {

        builder.HasOne(x => x.Bakery).WithMany(x => x.AvailableCakes).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.OrderDetails).WithOne(x => x.AvailableCake).HasForeignKey(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakeReviews).WithOne(x => x.AvailableCake).HasForeignKey(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);
    }
}