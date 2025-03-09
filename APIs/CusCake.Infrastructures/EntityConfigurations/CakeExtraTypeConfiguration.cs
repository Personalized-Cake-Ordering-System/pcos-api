using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

// public class CakeExtraTypeConfiguration : IEntityTypeConfiguration<CakeExtraType>
// {
//     public void Configure(EntityTypeBuilder<CakeExtraType> builder)
//     {
//         builder
//             .HasMany(x => x.Options)
//             .WithOne(x => x.ExtraType)
//             .HasForeignKey(x => x.ExtraTypeId)
//             .OnDelete(DeleteBehavior.Cascade);

//     }
// }
public class CakeExtraOptionConfiguration : IEntityTypeConfiguration<CakeExtraOption>
{
    public void Configure(EntityTypeBuilder<CakeExtraOption> builder)
    {
        // builder
        //     .HasOne(x => x.ExtraType)
        //     .WithMany(x => x.Options)
        //     .HasForeignKey(x => x.ExtraTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);


    }
}
public class CakeExtraSelectionConfiguration : IEntityTypeConfiguration<CakeExtraSelection>
{
    public void Configure(EntityTypeBuilder<CakeExtraSelection> builder)
    {
        builder
             .HasOne(c => c.CustomCake)
             .WithMany()
             .HasForeignKey(c => c.CustomCakeId)
             .OnDelete(DeleteBehavior.Cascade);
        // builder
        //     .HasOne(s => s.ExtraType)
        //     .WithMany()
        //     .HasForeignKey(s => s.ExtraTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.ExtraOption)
            .WithMany()
            .HasForeignKey(c => c.ExtraOptionId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}