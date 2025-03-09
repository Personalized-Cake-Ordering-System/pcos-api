using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

// public class CakeMessageTypeConfiguration : IEntityTypeConfiguration<CakeMessageType>
// {
//     public void Configure(EntityTypeBuilder<CakeMessageType> builder)
//     {
//         builder
//             .HasMany(x => x.Options)
//             .WithOne(x => x.MessageType)
//             .HasForeignKey(x => x.MessageTypeId)
//             .OnDelete(DeleteBehavior.Cascade);

//     }
// }


public class CakeMessageOptionConfiguration : IEntityTypeConfiguration<CakeMessageOption>
{
    public void Configure(EntityTypeBuilder<CakeMessageOption> builder)
    {
        // builder
        //     .HasOne(x => x.MessageType)
        //     .WithMany(x => x.Options)
        //     .HasForeignKey(x => x.MessageTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}


public class CakeMessageSelectionConfiguration : IEntityTypeConfiguration<CakeMessageSelection>
{
    public void Configure(EntityTypeBuilder<CakeMessageSelection> builder)
    {
        builder.Property(b => b.MessageOptions)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Serialize to JSON
                    v => JsonSerializer.Deserialize<List<CakeMessageOption>>(v, (JsonSerializerOptions?)null) ?? new() // Deserialize from JSON
                )
                .HasColumnType("json");

        // builder
        //     .HasOne(x => x.MessageType)
        //     .WithMany()
        //     .HasForeignKey(x => x.MessageTypeId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.CustomCake)
            .WithOne(x => x.MessageSelection)
            .HasForeignKey<CakeMessageSelection>(c => c.CustomCakeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}