using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiletPortal.Migrations
{
    /// <inheritdoc />
    public partial class SeatUpdating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Seat",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Seat",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Seat",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_ProductsProductId",
                table: "Seat",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_UserId",
                table: "Seat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_AspNetUsers_UserId",
                table: "Seat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Products_ProductsProductId",
                table: "Seat",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_AspNetUsers_UserId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Products_ProductsProductId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_ProductsProductId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_UserId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Seat");
        }
    }
}
