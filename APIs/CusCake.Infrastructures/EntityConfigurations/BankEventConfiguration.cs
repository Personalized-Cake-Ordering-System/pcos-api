using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class BankEventConfiguration : IEntityTypeConfiguration<BankEvent>
{
    public void Configure(EntityTypeBuilder<BankEvent> builder)
    {

        builder.HasOne(x => x.Transaction).WithMany(x => x.BankEvents).HasForeignKey(x => x.TransactionId).OnDelete(DeleteBehavior.Cascade);
    }
}