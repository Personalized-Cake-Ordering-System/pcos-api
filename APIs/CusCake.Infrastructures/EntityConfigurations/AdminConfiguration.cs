using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CusCake.Infrastructures.EntityConfigurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
    }
}