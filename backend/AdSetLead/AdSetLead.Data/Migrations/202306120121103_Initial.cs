namespace AdSetLead.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ano = c.Int(nullable: false),
                        Cor = c.String(nullable: false),
                        Kilometragem = c.Double(),
                        MarcaId = c.Int(nullable: false),
                        ModeloId = c.Int(nullable: false),
                        Placa = c.String(nullable: false),
                        Preco = c.Double(nullable: false),
                        Opcional_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marca", t => t.MarcaId)
                .ForeignKey("dbo.Modelo", t => t.ModeloId)
                .ForeignKey("dbo.Opcional", t => t.Opcional_Id)
                .Index(t => t.MarcaId)
                .Index(t => t.ModeloId)
                .Index(t => t.Opcional_Id);
            
            CreateTable(
                "dbo.Imagem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        CarroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carro", t => t.CarroId, cascadeDelete: true)
                .Index(t => t.CarroId);
            
            CreateTable(
                "dbo.Marca",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modelo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        MarcaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marca", t => t.MarcaId, cascadeDelete: true)
                .Index(t => t.MarcaId);
            
            CreateTable(
                "dbo.Opcional",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarroOpcional",
                c => new
                    {
                        CarroId = c.Int(nullable: false),
                        OpcionalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CarroId, t.OpcionalId })
                .ForeignKey("dbo.Carro", t => t.CarroId, cascadeDelete: true)
                .ForeignKey("dbo.Opcional", t => t.OpcionalId, cascadeDelete: true)
                .Index(t => t.CarroId)
                .Index(t => t.OpcionalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarroOpcional", "OpcionalId", "dbo.Opcional");
            DropForeignKey("dbo.CarroOpcional", "CarroId", "dbo.Carro");
            DropForeignKey("dbo.Carro", "Opcional_Id", "dbo.Opcional");
            DropForeignKey("dbo.Carro", "ModeloId", "dbo.Modelo");
            DropForeignKey("dbo.Carro", "MarcaId", "dbo.Marca");
            DropForeignKey("dbo.Modelo", "MarcaId", "dbo.Marca");
            DropForeignKey("dbo.Imagem", "CarroId", "dbo.Carro");
            DropIndex("dbo.CarroOpcional", new[] { "OpcionalId" });
            DropIndex("dbo.CarroOpcional", new[] { "CarroId" });
            DropIndex("dbo.Modelo", new[] { "MarcaId" });
            DropIndex("dbo.Imagem", new[] { "CarroId" });
            DropIndex("dbo.Carro", new[] { "Opcional_Id" });
            DropIndex("dbo.Carro", new[] { "ModeloId" });
            DropIndex("dbo.Carro", new[] { "MarcaId" });
            DropTable("dbo.CarroOpcional");
            DropTable("dbo.Opcional");
            DropTable("dbo.Modelo");
            DropTable("dbo.Marca");
            DropTable("dbo.Imagem");
            DropTable("dbo.Carro");
        }
    }
}
