using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiletPortal.Migrations
{
    /// <inheritdoc />
    public partial class seat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "seatIdNumber",
                table: "Seat",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seatIdNumber",
                table: "Seat");
        }
    }
}
