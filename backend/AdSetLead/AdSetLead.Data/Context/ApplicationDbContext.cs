using AdSetLead.Core.Model;
using AdSetLead.Core.Models;
using AdSetLead.Data.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

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

            modelBuilder.Entity<Opcional>().Ignore(c => c.Carros);


            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        // Override SaveChanges method to handle relationship deletion
        public override int SaveChanges()
        {
            // Get the entities that are being deleted
            var deletedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted)
                .Select(e => e.Entity);

            // Manually remove the relationships without deleting the other entity
            foreach (var entity in deletedEntities)
            {
                var carro = entity as Carro;
                if (carro != null)
                {
                    carro.Opcionais.Clear(); // Remove the relationships by clearing the collection
                }
            }

            return base.SaveChanges();
        }
    }
}
