using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasOne(x => x.Bakery).WithMany().HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.Orders).WithOne(x => x.Voucher).HasForeignKey(x => x.VoucherId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.CustomerVouchers).WithOne(x => x.Voucher).HasForeignKey(x => x.VoucherId).OnDelete(DeleteBehavior.Cascade);
    }
}