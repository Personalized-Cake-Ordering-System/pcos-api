using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasOne(x => x.CustomCake).WithMany(x => x.OrderDetails).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.CakeReview).WithOne(x => x.OrderDetail).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AvailableCake).WithMany(x => x.OrderDetails).HasForeignKey(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);

    }
}