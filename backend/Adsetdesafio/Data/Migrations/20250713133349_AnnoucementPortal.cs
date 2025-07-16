using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adsetdesafio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnnoucementPortal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementPortal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePortal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementPortal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementPortal_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPortal_CarId",
                table: "AnnouncementPortal",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPortal_NomePortal",
                table: "AnnouncementPortal",
                column: "NomePortal",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementPortal");
        }
    }
}
