using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CusCake.Infrastructures.EntityConfigurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        // builder.HasOne(x => x.OrderDetail).WithOne().HasForeignKey<OrderDetail>(e => e.CakeReviewId).OnDelete(DeleteBehavior.Cascade);
    }
}
public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.HasOne(x => x.Wallet).WithMany().HasForeignKey(e => e.WalletId).OnDelete(DeleteBehavior.Cascade);
    }
}