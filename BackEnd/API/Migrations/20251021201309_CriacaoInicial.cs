using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opcional",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Opcional__3214EC27101B7AB8", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Portal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Portal__3214EC27AF26D932", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Modelo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Placa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Km = table.Column<int>(type: "int", nullable: true),
                    Cor = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Veiculo__3214EC27C8994EF0", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacote",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    PortalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pacote__3214EC278F1A74BF", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pacote_Portal",
                        column: x => x.PortalID,
                        principalTable: "Portal",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoID = table.Column<int>(type: "int", nullable: false),
                    Arquivo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Path = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Foto__3214EC27478B8C5E", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Foto_Veiculo",
                        column: x => x.VeiculoID,
                        principalTable: "Veiculo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelacaoVeiculoOpcional",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoID = table.Column<int>(type: "int", nullable: false),
                    OpcionalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RelacaoV__3214EC276129C2AB", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelacaoVeiculoOpcional_Opcional",
                        column: x => x.OpcionalID,
                        principalTable: "Opcional",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RelacaoVeiculoOpcional_Veiculo",
                        column: x => x.VeiculoID,
                        principalTable: "Veiculo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelacaoVeiculoPacotePortal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoID = table.Column<int>(type: "int", nullable: false),
                    PacoteID = table.Column<int>(type: "int", nullable: false),
                    PortalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RelacaoV__3214EC27769DCE51", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RelacaoVeiculoPacotePortal_Pacote",
                        column: x => x.PacoteID,
                        principalTable: "Pacote",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RelacaoVeiculoPacotePortal_Portal",
                        column: x => x.PortalID,
                        principalTable: "Portal",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RelacaoVeiculoPacotePortal_Veiculo",
                        column: x => x.VeiculoID,
                        principalTable: "Veiculo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Opcional",
                columns: new[] { "ID", "Descricao" },
                values: new object[,]
                {
                    { 1, "Ar Condicionado" },
                    { 2, "Airbag" },
                    { 3, "Freios ABS" },
                    { 4, "Alarme" }
                });

            migrationBuilder.InsertData(
                table: "Portal",
                columns: new[] { "ID", "Nome" },
                values: new object[,]
                {
                    { 1, "iCarros" },
                    { 2, "Webmotors" }
                });

            migrationBuilder.InsertData(
                table: "Pacote",
                columns: new[] { "ID", "Nome", "PortalID" },
                values: new object[,]
                {
                    { 1, "Básico", 1 },
                    { 2, "Bronze", 1 },
                    { 3, "Platinum", 1 },
                    { 4, "Diamante", 1 },
                    { 5, "Básico", 2 },
                    { 6, "Bronze", 2 },
                    { 7, "Platinum", 2 },
                    { 8, "Diamante", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foto_VeiculoID",
                table: "Foto",
                column: "VeiculoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pacote_PortalID",
                table: "Pacote",
                column: "PortalID");

            migrationBuilder.CreateIndex(
                name: "IX_RelacaoVeiculoOpcional_OpcionalID",
                table: "RelacaoVeiculoOpcional",
                column: "OpcionalID");

            migrationBuilder.CreateIndex(
                name: "IX_RelacaoVeiculoOpcional_VeiculoID",
                table: "RelacaoVeiculoOpcional",
                column: "VeiculoID");

            migrationBuilder.CreateIndex(
                name: "IX_RelacaoVeiculoPacotePortal_PacoteID",
                table: "RelacaoVeiculoPacotePortal",
                column: "PacoteID");

            migrationBuilder.CreateIndex(
                name: "IX_RelacaoVeiculoPacotePortal_PortalID",
                table: "RelacaoVeiculoPacotePortal",
                column: "PortalID");

            migrationBuilder.CreateIndex(
                name: "UQ_Veiculo_Portal",
                table: "RelacaoVeiculoPacotePortal",
                columns: new[] { "VeiculoID", "PortalID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "RelacaoVeiculoOpcional");

            migrationBuilder.DropTable(
                name: "RelacaoVeiculoPacotePortal");

            migrationBuilder.DropTable(
                name: "Opcional");

            migrationBuilder.DropTable(
                name: "Pacote");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Portal");
        }
    }
}
