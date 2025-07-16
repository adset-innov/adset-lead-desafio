using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adsetdesafio.Data.Migrations
{
    /// <inheritdoc />
    public partial class relationshipwithportal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementPortal_Cars_CarId",
                table: "AnnouncementPortal");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementPortal_NomePortal",
                table: "AnnouncementPortal");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementPortal_Cars_CarId",
                table: "AnnouncementPortal",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementPortal_Cars_CarId",
                table: "AnnouncementPortal");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPortal_NomePortal",
                table: "AnnouncementPortal",
                column: "NomePortal",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementPortal_Cars_CarId",
                table: "AnnouncementPortal",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }
    }
}
