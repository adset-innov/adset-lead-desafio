using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciarCarros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InclusaoCampoNomeTabelaImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Imagens",
                type: "varchar(200)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Imagens");
        }
    }
}
