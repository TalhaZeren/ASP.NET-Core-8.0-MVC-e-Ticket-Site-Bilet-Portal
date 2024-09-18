using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiletPortal.Migrations
{
    /// <inheritdoc />
    public partial class HallInfoAddedToSelecSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "hallId",
                table: "SelectSeat",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HallPicture",
                table: "HallInfo",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectSeat_hallId",
                table: "SelectSeat",
                column: "hallId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectSeat_HallInfo_hallId",
                table: "SelectSeat",
                column: "hallId",
                principalTable: "HallInfo",
                principalColumn: "Hallid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectSeat_HallInfo_hallId",
                table: "SelectSeat");

            migrationBuilder.DropIndex(
                name: "IX_SelectSeat_hallId",
                table: "SelectSeat");

            migrationBuilder.DropColumn(
                name: "hallId",
                table: "SelectSeat");

            migrationBuilder.AlterColumn<string>(
                name: "HallPicture",
                table: "HallInfo",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
