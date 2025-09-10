namespace AdSet.Data.Configuration
{
    public class OptionalConfiguration
    {
        public void Configure(EntityTypeBuilder<Optional> builder)
        {
            builder.ToTable("Optional");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
