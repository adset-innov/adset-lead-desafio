namespace AdSet.Data.Configuration
{
    public class VehicleOptionalConfiguration
    {
        public void Configure(EntityTypeBuilder<VehicleOptional> builder)
        {
            builder.ToTable("VehicleOptional");

            builder.HasKey(vo => new { vo.VehicleId, vo.OptionalId });

            builder.HasOne(vo => vo.Vehicle)
                   .WithMany(v => v.VehicleOptionals)
                   .HasForeignKey(vo => vo.VehicleId);

            builder.HasOne(vo => vo.Optional)
                   .WithMany(o => o.VehicleOptionals)
                   .HasForeignKey(vo => vo.OptionalId);
        }
    }
}
