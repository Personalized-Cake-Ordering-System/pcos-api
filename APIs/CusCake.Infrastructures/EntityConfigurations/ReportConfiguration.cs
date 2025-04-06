using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;
public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        var storageComparer = new ValueComparer<List<Storage>>(
           (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2), // So sánh 2 danh sách
           c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Tạo hash code
           c => c.ToList()
       );

        builder.Property(b => b.ReportFiles)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Serialize to JSON
                   v => JsonSerializer.Deserialize<List<Storage>>(v, (JsonSerializerOptions?)null) ?? new() // Deserialize from JSON
               )
               .HasColumnType("json")
               .Metadata.SetValueComparer(storageComparer);
        builder.HasOne(x => x.Bakery).WithMany().HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

    }
}
