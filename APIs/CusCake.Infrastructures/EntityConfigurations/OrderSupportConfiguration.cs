using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class OrderSupportConfiguration : IEntityTypeConfiguration<OrderSupport>
{
    public void Configure(EntityTypeBuilder<OrderSupport> builder)
    {
        builder.HasOne(x => x.Customer).WithMany(x => x.OrderSupports).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Bakery).WithMany(x => x.OrderSupports).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Order).WithMany(x => x.OrderSupports).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
    }
}