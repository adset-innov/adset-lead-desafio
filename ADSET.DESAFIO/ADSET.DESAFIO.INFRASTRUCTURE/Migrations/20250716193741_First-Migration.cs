using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADSET.DESAFIO.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_car",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    km = table.Column<int>(type: "int", nullable: false),
                    color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_car", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_optional",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_optional", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_car_photo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    car_id = table.Column<int>(type: "int", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_car_photo", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_car_photo_tb_car_car_id",
                        column: x => x.car_id,
                        principalTable: "tb_car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_car_portal_package",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "int", nullable: false),
                    portal = table.Column<int>(type: "int", nullable: false),
                    package = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_car_portal_package", x => new { x.car_id, x.portal });
                    table.ForeignKey(
                        name: "FK_tb_car_portal_package_tb_car_car_id",
                        column: x => x.car_id,
                        principalTable: "tb_car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_car_optional",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "int", nullable: false),
                    optional_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_car_optional", x => new { x.car_id, x.optional_id });
                    table.ForeignKey(
                        name: "FK_tb_car_optional_tb_car_car_id",
                        column: x => x.car_id,
                        principalTable: "tb_car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_car_optional_tb_optional_optional_id",
                        column: x => x.optional_id,
                        principalTable: "tb_optional",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_car_optional_optional_id",
                table: "tb_car_optional",
                column: "optional_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_car_photo_car_id",
                table: "tb_car_photo",
                column: "car_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_car_optional");

            migrationBuilder.DropTable(
                name: "tb_car_photo");

            migrationBuilder.DropTable(
                name: "tb_car_portal_package");

            migrationBuilder.DropTable(
                name: "tb_optional");

            migrationBuilder.DropTable(
                name: "tb_car");
        }
    }
}
