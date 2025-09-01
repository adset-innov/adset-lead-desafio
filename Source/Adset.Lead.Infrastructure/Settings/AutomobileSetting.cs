using Adset.Lead.Domain.Automobiles;
using Adset.Lead.Infrastructure.Extensions;
using Adset.Lead.Infrastructure.ValueComparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adset.Lead.Infrastructure.Settings;

internal sealed class AutomobileSetting : IEntityTypeConfiguration<Automobile>
{
    public void Configure(EntityTypeBuilder<Automobile> builder)
    {
        builder.ToTable("Automobiles");
        
        builder.HasKey(automobile => automobile.Id);
        
        builder.Property(automobile => automobile.Brand)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(automobile => automobile.Model)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(automobile => automobile.Year)
            .IsRequired();
            
        builder.Property(automobile => automobile.Plate)
            .IsRequired()
            .HasMaxLength(10);
            
        builder.Property(automobile => automobile.Km)
            .IsRequired(false);
            
        builder.Property(automobile => automobile.Color)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(automobile => automobile.Price)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder.Property(p => p.Features)
            .IsRequired()
            .HasJsonConversion<Automobile, OptionalFeatures>();
        
        // Configuração para Photos - JSON conversion com comparador customizado para detectar mudanças
        builder.Property(a => a.Photos)
            .HasJsonConversion<Automobile, Photo>()
            .Metadata.SetValueComparer(PhotoListValueComparer.Create());

        // Configuração do relacionamento um-para-um com PortalPackage
        builder.HasOne(a => a.PortalPackage)
            .WithOne()
            .HasForeignKey<PortalPackage>(pp => pp.AutomobileId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices para melhorar performance nas consultas
        builder.HasIndex(a => a.Brand)
            .HasDatabaseName("IX_Automobiles_Brand");
            
        builder.HasIndex(a => a.Model)
            .HasDatabaseName("IX_Automobiles_Model");
            
        builder.HasIndex(a => a.Year)
            .HasDatabaseName("IX_Automobiles_Year");
            
        builder.HasIndex(a => a.Plate)
            .HasDatabaseName("IX_Automobiles_Plate")
            .IsUnique();
            
        builder.HasIndex(a => a.Price)
            .HasDatabaseName("IX_Automobiles_Price");
            
        builder.HasIndex(a => a.Color)
            .HasDatabaseName("IX_Automobiles_Color");
        
        builder.Ignore(a => a.Portal);
        builder.Ignore(a => a.Package);

        // Índice composto para busca por marca e modelo
        builder.HasIndex(a => new { a.Brand, a.Model })
            .HasDatabaseName("IX_Automobiles_Brand_Model");
    }
}