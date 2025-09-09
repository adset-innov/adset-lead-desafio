namespace AdSet.Data.Configuration
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.ToTable("Foto");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.FilePath)
                   .IsRequired();

            builder.HasOne(f => f.Vehicle)
                   .WithMany(v => v.VehicleImages)
                   .HasForeignKey(f => f.VehicleId);
        }
    }
}
