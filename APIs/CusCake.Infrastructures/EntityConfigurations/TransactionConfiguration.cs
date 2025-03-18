using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        // builder.HasMany(x => x.BankEvents).WithOne(x => x.Transaction).HasForeignKey(x => x.TransactionId).OnDelete(DeleteBehavior.Cascade);
    }
}