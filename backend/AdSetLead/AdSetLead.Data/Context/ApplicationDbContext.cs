using AdSetLead.Core.Model;
using AdSetLead.Core.Models;
using AdSetLead.Data.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AdSetLead.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("AdSetLeadConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, MigrationConfiguration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Carro> Carro { get; set; }       
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Opcional> Opcional { get; set; }         
        public DbSet<Imagem> Imagem { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Relacionamento de Carro e Marca            
            modelBuilder.Entity<Carro>()
                .HasKey(c => c.Id)
                .HasRequired(c => c.Marca)
                .WithMany()
                .HasForeignKey(c => c.MarcaId)
                .WillCascadeOnDelete(false);            

            // Relacionamento de Carro e Modelo           
            modelBuilder.Entity<Carro>()
                .HasRequired(c => c.Modelo)
                .WithMany()
                .HasForeignKey(c => c.ModeloId)
                .WillCascadeOnDelete(false);

            // Relacionamento de carro e Imagem
            modelBuilder.Entity<Carro>()
            .HasMany(c => c.Imagens)
            .WithRequired(i => i.Carro)
            .HasForeignKey(i => i.CarroId);

            // Relacionamento de Marca e Modelo           
            modelBuilder.Entity<Marca>()
                .HasKey(m => m.Id)
                .HasMany(m => m.Modelos)
                .WithRequired(m => m.Marca)
                .HasForeignKey(m => m.MarcaId)
                .WillCascadeOnDelete(true);

            // Relacionamento de Modelo e Marca
            modelBuilder.Entity<Modelo>()
                .HasKey(m => m.Id)
                .HasRequired(m => m.Marca)
                .WithMany(m => m.Modelos)
                .HasForeignKey(m => m.MarcaId)
                .WillCascadeOnDelete(true);

            // Relacionamento de muitos para muitos
            modelBuilder.Entity<Carro>()
                .HasMany(c => c.Opcionais)
                .WithMany()                
                .Map(m =>
                {
                    m.ToTable("CarroOpcional");
                    m.MapLeftKey("CarroId");
                    m.MapRightKey("OpcionalId");
                });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
