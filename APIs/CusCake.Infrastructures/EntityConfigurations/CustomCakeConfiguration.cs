using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CustomCakeConfiguration : IEntityTypeConfiguration<CustomCake>
{
    public void Configure(EntityTypeBuilder<CustomCake> builder)
    {
        builder.HasOne(x => x.Bakery).WithMany(x => x.CustomCakes).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Customer).WithMany(x => x.CustomCakes).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakePartDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakeDecorationDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakeExtraDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakeMessageDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.OrderDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);

    }
}