namespace AdSetLead.Data.Migrations
{
    using AdSetLead.Core.Model;
    using AdSetLead.Core.Models;
    using AdSetLead.Data.Context;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Text;

    public sealed class MigrationConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Create();              
            }

            bool exiteCarro = context.Carro.Any();

            if (!exiteCarro)
            {
                SeedByScript(context);
            }            
        }

        private void SeedByScript(ApplicationDbContext context)
        {
            // Seed através dos script
            // Caso falhar pode rodar os script de forma manual -- AdSetLead.Data/script/inserts.sql
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string novoCaminho = baseDirectory.Replace("\\AdSetLead.Api\\", "");
            string scriptPath = Path.Combine(novoCaminho, "AdSetLead.Data\\script\\inserts.sql");
            string script = File.ReadAllText(scriptPath);
            context.Database.ExecuteSqlCommand(script);
        }
    }
}
