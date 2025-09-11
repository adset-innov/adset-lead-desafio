namespace AdSet.Data.Configuration
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.ToTable("VehicleImages");
            builder.HasKey(vi => vi.Id);
            builder.Property(vi => vi.ImageUrl).IsRequired().HasMaxLength(500);

            builder.HasOne(vi => vi.Vehicle)
                   .WithMany(v => v.VehicleImages)
                   .HasForeignKey(vi => vi.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
