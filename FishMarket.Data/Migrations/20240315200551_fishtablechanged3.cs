using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class fishtablechanged3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Fish");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Fish",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Fish");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Fish",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
