using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class AvailableCakeConfiguration : IEntityTypeConfiguration<AvailableCake>
{
    public void Configure(EntityTypeBuilder<AvailableCake> builder)
    {
        builder
        //  .Ignore(o => o.Metric)
         .Ignore(o => o.Reviews);

        var storageComparer = new ValueComparer<List<Storage>>(
            (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2), // So sánh 2 danh sách
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Tạo hash code
            c => c.ToList()
        );

        builder.HasOne(x => x.Bakery).WithMany().HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.OrderDetails).WithOne(x => x.AvailableCake).HasForeignKey(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.CakeReviews).WithOne(x => x.AvailableCake).HasForeignKey(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);
        builder
           .HasOne(c => c.AvailableCakeMainImage)
           .WithMany()
           .HasForeignKey(c => c.AvailableCakeMainImageId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.AvailableCakeImageFiles)
          .HasConversion(
              v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Serialize to JSON
              v => JsonSerializer.Deserialize<List<Storage>>(v, (JsonSerializerOptions?)null) ?? new() // Deserialize from JSON
          )
          .HasColumnType("json")
          .Metadata.SetValueComparer(storageComparer);
    }
}
public class AvailableCakeMetricConfiguration : IEntityTypeConfiguration<AvailableCakeMetric>
{
    public void Configure(EntityTypeBuilder<AvailableCakeMetric> builder)
    {
        builder.HasOne(x => x.AvailableCake).WithOne(x => x.Metric).HasForeignKey<AvailableCakeMetric>(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);
    }
}