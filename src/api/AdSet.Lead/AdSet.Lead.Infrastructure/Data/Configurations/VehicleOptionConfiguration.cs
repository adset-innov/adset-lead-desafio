using AdSet.Lead.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdSet.Lead.Infrastructure.Data.Configurations;

public class VehicleOptionConfiguration : IEntityTypeConfiguration<VehicleOption>
{
    public void Configure(EntityTypeBuilder<VehicleOption> builder)
    {
        builder.HasKey(vo => vo.Id);

        builder.Property(vo => vo.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}