using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class BakeryConfiguration : IEntityTypeConfiguration<Bakery>
{
    public void Configure(EntityTypeBuilder<Bakery> builder)
    {
        builder.Property(b => b.ShopImageFiles)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Serialize to JSON
                    v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new() // Deserialize from JSON
                )
                .HasColumnType("json");
    }
}