namespace AdSet.Data.Configuration
{
    public class PacotePortalConfiguration : IEntityTypeConfiguration<VehiclePortalPackage>
    {
        public void Configure(EntityTypeBuilder<VehiclePortalPackage> builder)
        {
            builder.ToTable("VehiclePortalPackage");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Portal)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.Package)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasOne(p => p.Vehicle)
                   .WithMany(v => v.VechiclePortalPackages)
                   .HasForeignKey(p => p.VehicleId);

            builder.HasIndex(p => new { p.VehicleId, p.Portal }).IsUnique();
        }
    }
}
