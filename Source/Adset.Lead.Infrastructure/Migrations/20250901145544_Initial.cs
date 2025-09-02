using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adset.Lead.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Km = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AutomobileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Portal = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Package = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortalPackages_Automobiles_AutomobileId",
                        column: x => x.AutomobileId,
                        principalTable: "Automobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Brand",
                table: "Automobiles",
                column: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Brand_Model",
                table: "Automobiles",
                columns: new[] { "Brand", "Model" });

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Color",
                table: "Automobiles",
                column: "Color");

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Model",
                table: "Automobiles",
                column: "Model");

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Plate",
                table: "Automobiles",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Price",
                table: "Automobiles",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_Year",
                table: "Automobiles",
                column: "Year");

            migrationBuilder.CreateIndex(
                name: "IX_PortalPackages_AutomobileId",
                table: "PortalPackages",
                column: "AutomobileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortalPackages_AutomobileId_Portal",
                table: "PortalPackages",
                columns: new[] { "AutomobileId", "Portal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortalPackages_Portal",
                table: "PortalPackages",
                column: "Portal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortalPackages");

            migrationBuilder.DropTable(
                name: "Automobiles");
        }
    }
}
