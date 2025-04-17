using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class BakeryConfiguration : IEntityTypeConfiguration<Bakery>
{
   public void Configure(EntityTypeBuilder<Bakery> builder)
   {
      var storageComparer = new ValueComparer<List<Storage>>(
           (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2), // So sánh 2 danh sách
           c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Tạo hash code
           c => c.ToList()
       );
      builder.Property(b => b.ShopImageFiles)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Serialize to JSON
                  v => JsonSerializer.Deserialize<List<Storage>>(v, (JsonSerializerOptions?)null) ?? new() // Deserialize from JSON
              )
              .HasColumnType("json")
              .Metadata.SetValueComparer(storageComparer);

      // builder.HasMany(x => x.Notifications).WithOne(x => x.Bakery).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
      // builder.HasMany(x => x.AvailableCakes).WithOne(x => x.Bakery).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
      // builder.HasMany(x => x.Orders).WithOne(x => x.Bakery).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
      // builder.HasMany(x => x.CakeReviews).WithOne(x => x.Bakery).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
      // builder.HasMany(x => x.OrderSupports).WithOne(x => x.Bakery).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
      // builder.HasMany(x => x.Vouchers).WithOne(x => x.Bakery).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);

      builder
         .HasOne(c => c.AvatarFile)
         .WithMany()
         .HasForeignKey(c => c.AvatarFileId)
         .OnDelete(DeleteBehavior.Cascade);

      builder
         .HasOne(c => c.BackCardFile)
         .WithMany()
         .HasForeignKey(c => c.BackCardFileId)
         .OnDelete(DeleteBehavior.Cascade);

      builder
         .HasOne(c => c.FrontCardFile)
         .WithMany()
         .HasForeignKey(c => c.FrontCardFileId)
         .OnDelete(DeleteBehavior.Cascade);
   }
}

public class BakeryMetricConfiguration : IEntityTypeConfiguration<BakeryMetric>
{
   public void Configure(EntityTypeBuilder<BakeryMetric> builder)
   {
      builder
        .HasOne(c => c.Bakery)
        .WithMany()
        .HasForeignKey(c => c.BakeryId)
        .OnDelete(DeleteBehavior.Cascade);
   }
}