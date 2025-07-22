using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADSET.DESAFIO.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoSalvarImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "tb_car_photo");

            migrationBuilder.AddColumn<byte[]>(
                name: "photo_data",
                table: "tb_car_photo",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_data",
                table: "tb_car_photo");

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "tb_car_photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
