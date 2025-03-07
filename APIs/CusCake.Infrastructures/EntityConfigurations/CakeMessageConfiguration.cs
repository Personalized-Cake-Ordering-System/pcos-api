using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeMessageConfiguration : IEntityTypeConfiguration<CakeMessage>
{
    public void Configure(EntityTypeBuilder<CakeMessage> builder)
    {
        builder.HasMany(x => x.CakeMessageDetails).WithOne(x => x.CakeMessage).HasForeignKey(x => x.CakeMessageId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakeMessageTypes).WithOne(x => x.CakeMessage).HasForeignKey(x => x.CakeMessageId).OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(c => c.MessageImage)
            .WithMany()
            .HasForeignKey(c => c.MessageImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}


public class CakeMessageTypeConfiguration : IEntityTypeConfiguration<CakeMessageType>
{
    public void Configure(EntityTypeBuilder<CakeMessageType> builder)
    {
        builder.HasOne(x => x.CakeMessage).WithMany(x => x.CakeMessageTypes).HasForeignKey(x => x.CakeMessageId).OnDelete(DeleteBehavior.Cascade);
    }
}


public class CakeMessageDetailConfiguration : IEntityTypeConfiguration<CakeMessageDetail>
{
    public void Configure(EntityTypeBuilder<CakeMessageDetail> builder)
    {
        builder.Property(b => b.MessageTypeDetails)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Serialize to JSON
                    v => JsonSerializer.Deserialize<List<CakeMessageTypeDetail>>(v, (JsonSerializerOptions?)null) ?? new() // Deserialize from JSON
                )
                .HasColumnType("json");

        builder.HasOne(x => x.CakeMessage).WithMany(x => x.CakeMessageDetails).HasForeignKey(x => x.CakeMessageId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CustomCake).WithMany(x => x.CakeMessageDetails).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CustomCake).WithMany(x => x.CakeMessageDetails).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(c => c.MessageImageFile)
            .WithMany()
            .HasForeignKey(c => c.MessageImageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}