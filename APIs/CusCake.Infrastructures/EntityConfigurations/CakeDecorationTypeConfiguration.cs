using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

// public class CakeDecorationTypeConfiguration : IEntityTypeConfiguration<CakeDecorationType>
// {
//     public void Configure(EntityTypeBuilder<CakeDecorationType> builder)
//     {
//         builder
//             .HasMany(x => x.Options)
//             .WithOne(x => x.DecorationType)
//             .HasForeignKey(x => x.DecorationTypeId)
//             .OnDelete(DeleteBehavior.Cascade);

//     }
// }

public class CakeDecorationOptionConfiguration : IEntityTypeConfiguration<CakeDecorationOption>
{
    public void Configure(EntityTypeBuilder<CakeDecorationOption> builder)
    {
        // builder
        //     .HasOne(x => x.DecorationType)
        //     .WithMany(x => x.Options)
        //     .HasForeignKey(x => x.DecorationTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);

    }
}
public class CakeDecorationSelectionConfiguration : IEntityTypeConfiguration<CakeDecorationSelection>
{
    public void Configure(EntityTypeBuilder<CakeDecorationSelection> builder)
    {
        builder
            .HasOne(c => c.CustomCake)
            .WithMany()
            .HasForeignKey(c => c.CustomCakeId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(s => s.DecorationOption)
            .WithMany()
            .HasForeignKey(s => s.DecorationOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        // builder
        //     .HasOne(c => c.DecorationType)
        //     .WithMany()
        //     .HasForeignKey(c => c.DecorationTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}