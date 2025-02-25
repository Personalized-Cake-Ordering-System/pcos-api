using System.Text.Json;
using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class CakeMessageConfiguration : IEntityTypeConfiguration<CakeMessage>
{
    public void Configure(EntityTypeBuilder<CakeMessage> builder)
    {
        builder.HasOne(x => x.CustomCake).WithMany(x => x.CakeMessages).HasForeignKey(x => x.CustomCakeId).OnDelete(DeleteBehavior.Cascade);
    }
}