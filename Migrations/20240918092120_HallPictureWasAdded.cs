using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiletPortal.Migrations
{
    /// <inheritdoc />
    public partial class HallPictureWasAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HallPicture",
                table: "HallInfo",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HallPicture",
                table: "HallInfo");
        }
    }
}
