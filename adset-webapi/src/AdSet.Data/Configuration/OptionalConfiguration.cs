namespace AdSet.Data.Configuration
{
    public class OptionalConfiguration : IEntityTypeConfiguration<Optional>
    {
        public void Configure(EntityTypeBuilder<Optional> builder)
        {
            builder.ToTable("Optionals");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
               new Optional { Id = 1, Name = "Ar Condicionado" },
               new Optional { Id = 2, Name = "Alarme" },
               new Optional { Id = 3, Name = "Airbag" },
               new Optional { Id = 4, Name = "Freio ABS" }
           );
        }
    }
}
