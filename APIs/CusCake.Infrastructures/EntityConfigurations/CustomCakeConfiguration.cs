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

        builder
            .HasMany(c => c.PartSelections)
            .WithOne(s => s.CustomCake)
            .HasForeignKey(s => s.CustomCakeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.DecorationSelections)
            .WithOne(x => x.CustomCake)
            .HasForeignKey(x => x.CustomCakeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ExtraSelections)
            .WithOne(x => x.CustomCake)
            .HasForeignKey(x => x.CustomCakeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.MessageSelection)
            .WithOne(x => x.CustomCake)
            .HasForeignKey<CustomCake>(x => x.MessageSelectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.OrderDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);

    }
}