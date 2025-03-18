using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Ignore(o => o.OrderDetails)
            .Ignore(o => o.OrderSupports);

        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Bakery).WithMany().HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasOne(x => x.Transaction).WithMany().HasForeignKey(e => e.TransactionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Voucher).WithMany().HasForeignKey(x => x.VoucherId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CustomerVoucher).WithOne(x => x.Order).HasForeignKey<CustomerVoucher>(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.OrderDetails).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.OrderSupports).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
    }
}