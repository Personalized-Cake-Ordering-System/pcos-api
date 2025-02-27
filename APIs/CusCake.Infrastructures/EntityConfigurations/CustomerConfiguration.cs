using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.Notifications).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CustomCakes).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Orders).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CakeReviews).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.CustomerVouchers).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.OrderSupports).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);

    }
}