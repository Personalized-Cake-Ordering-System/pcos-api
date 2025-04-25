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

      builder
         .Ignore(o => o.DistanceToUser)
         .Ignore(o => o.Reviews);

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

      builder
         .HasOne(c => c.BusinessLicenseFile)
         .WithMany()
         .HasForeignKey(c => c.BusinessLicenseFileId)
         .OnDelete(DeleteBehavior.Cascade);

      builder
         .HasOne(c => c.FoodSafetyCertificateFile)
         .WithMany()
         .HasForeignKey(c => c.FoodSafetyCertificateFileId)
         .OnDelete(DeleteBehavior.Cascade);

   }
}

public class BakeryMetricConfiguration : IEntityTypeConfiguration<BakeryMetric>
{
   public void Configure(EntityTypeBuilder<BakeryMetric> builder)
   {
      builder
        .HasOne(c => c.Bakery)
        .WithOne(x => x.Metric)
        .HasForeignKey<BakeryMetric>(c => c.BakeryId)
        .OnDelete(DeleteBehavior.Cascade);
   }
}