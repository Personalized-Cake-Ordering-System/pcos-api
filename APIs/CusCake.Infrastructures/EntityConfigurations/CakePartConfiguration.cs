using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

// public class CakePartTypeConfiguration : IEntityTypeConfiguration<CakePartType>
// {
//     public void Configure(EntityTypeBuilder<CakePartType> builder)
//     {
//         builder
//             .HasMany(x => x.Options)
//             .WithOne(x => x.CakePartType)
//             .HasForeignKey(o => o.CakePartTypeId)
//             .OnDelete(DeleteBehavior.Cascade);
//     }
// }

public class CakePartOptionConfiguration : IEntityTypeConfiguration<CakePartOption>
{
    public void Configure(EntityTypeBuilder<CakePartOption> builder)
    {
        // builder
        //     .HasOne(x => x.CakePartType)
        //     .WithMany(x => x.Options)
        //     .HasForeignKey(o => o.CakePartTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Image)
            .WithMany()
            .HasForeignKey(c => c.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(c => c.Bakery)
            .WithMany()
            .HasForeignKey(c => c.BakeryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class CakePartSelectionConfiguration : IEntityTypeConfiguration<CakePartSelection>
{
    public void Configure(EntityTypeBuilder<CakePartSelection> builder)
    {
        builder
            .HasOne(s => s.PartOption)
            .WithMany()
            .HasForeignKey(s => s.PartOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        // builder
        //     .HasOne(c => c.PartType)
        //     .WithMany()
        //     .HasForeignKey(c => c.PartTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.CustomCake)
            .WithMany()
            .HasForeignKey(c => c.CustomCakeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}