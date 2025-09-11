namespace AdSet.Data.Configuration
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Packages");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

            // Seeding inicial para Pacotes
            builder.HasData(
                new Package { Id = 1, Name = "Bronze" },
                new Package { Id = 2, Name = "Diamante" },
                new Package { Id = 3, Name = "Platinum" },
                new Package { Id = 4, Name = "Básico" }
            );
        }
    }
}
