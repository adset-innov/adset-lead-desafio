public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicle");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Make).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Model).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Year).IsRequired();
        builder.Property(v => v.Plate).IsRequired().HasMaxLength(10);
        builder.Property(v => v.Km).IsRequired(false);
        builder.Property(v => v.Color).HasMaxLength(30);
        builder.Property(v => v.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasMany(v => v.VehicleOptionals);

        builder.HasMany(v => v.VehiclePortalPackages)
               .WithOne(p => p.Vehicle)
               .HasForeignKey(p => p.VehicleId);

    }
}