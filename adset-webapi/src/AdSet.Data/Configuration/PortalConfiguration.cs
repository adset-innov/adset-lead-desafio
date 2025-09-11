
namespace AdSet.Data.Configuration
{
    public class PortalConfiguration : IEntityTypeConfiguration<Portal>
    {
        public void Configure(EntityTypeBuilder<Portal> builder)
        {
            builder.ToTable("Portals");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            builder.HasData(
                new Portal { Id = 1, Name = "iCarros" },
                new Portal { Id = 2, Name = "WebMotors" }
            );
        }
    }
}
