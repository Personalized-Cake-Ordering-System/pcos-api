using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeReviewConfiguration : IEntityTypeConfiguration<CakeReview>
{
    public void Configure(EntityTypeBuilder<CakeReview> builder)
    {
        builder.HasOne(x => x.OrderDetail).WithOne(x => x.CakeReview).HasForeignKey<OrderDetail>(e => e.CakeReviewId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.AvailableCake).WithMany(x => x.CakeReviews).HasForeignKey(x => x.AvailableCakeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Bakery).WithMany(x => x.CakeReviews).HasForeignKey(x => x.BakeryId).OnDelete(DeleteBehavior.Cascade);

    }
}