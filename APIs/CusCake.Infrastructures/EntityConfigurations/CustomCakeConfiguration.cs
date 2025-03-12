using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CustomCakeConfiguration : IEntityTypeConfiguration<CustomCake>
{
    public void Configure(EntityTypeBuilder<CustomCake> builder)
    {
        builder.HasOne(x => x.Bakery).WithMany().HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

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
            .WithMany()
            .HasForeignKey(x => x.MessageSelectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.OrderDetails).WithOne(x => x.CustomCake).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);

    }
}