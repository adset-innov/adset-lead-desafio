namespace AdSet.Data.Configuration
{
    public class PacotePortalConfiguration : IEntityTypeConfiguration<VehiclePortalPackage>
    {
        public void Configure(EntityTypeBuilder<VehiclePortalPackage> builder)
        {
            builder.ToTable("VehiclePortalPackages");

            builder.HasKey(vpp => new { vpp.VehicleId, vpp.PortalId });

            builder.HasOne(vpp => vpp.Vehicle)
                   .WithMany(v => v.VehiclePortalPackages)
                   .HasForeignKey(vpp => vpp.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vpp => vpp.Portal)
                   .WithMany(p => p.VehiclePortalPackages)
                   .HasForeignKey(vpp => vpp.PortalId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vpp => vpp.Package)
                   .WithMany(pk => pk.VehiclePortalPackages)
                   .HasForeignKey(vpp => vpp.PackageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
