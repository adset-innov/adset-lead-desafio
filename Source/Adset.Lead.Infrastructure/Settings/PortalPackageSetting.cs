using Adset.Lead.Domain.Automobiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adset.Lead.Infrastructure.Settings;

internal sealed class PortalPackageSetting : IEntityTypeConfiguration<PortalPackage>
{
    public void Configure(EntityTypeBuilder<PortalPackage> builder)
    {
        builder.ToTable("PortalPackages");
        
        builder.HasKey(pp => pp.Id);
        
        // Configuração das propriedades
        builder.Property(pp => pp.AutomobileId)
            .IsRequired();
            
        builder.Property(pp => pp.Portal)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(pp => pp.Package)
            .IsRequired()
            .HasConversion<string>();

        // Índices para melhorar performance
        builder.HasIndex(pp => pp.AutomobileId)
            .HasDatabaseName("IX_PortalPackages_AutomobileId");
            
        builder.HasIndex(pp => pp.Portal)
            .HasDatabaseName("IX_PortalPackages_Portal");

        // Índice composto único para garantir que um automóvel tenha apenas um pacote por portal
        builder.HasIndex(pp => new { pp.AutomobileId, pp.Portal })
            .HasDatabaseName("IX_PortalPackages_AutomobileId_Portal")
            .IsUnique();
    }
}
