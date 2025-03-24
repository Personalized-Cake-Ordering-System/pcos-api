using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CustomerVoucherConfiguration : IEntityTypeConfiguration<CustomerVoucher>
{
    public void Configure(EntityTypeBuilder<CustomerVoucher> builder)
    {
        builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Voucher).WithMany().HasForeignKey(x => x.VoucherId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Order).WithOne(x => x.CustomerVoucher).HasForeignKey<Order>(e => e.CustomerVoucherId).OnDelete(DeleteBehavior.Cascade);

    }
}