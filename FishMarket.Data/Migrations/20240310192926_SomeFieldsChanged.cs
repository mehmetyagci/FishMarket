using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeFieldsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Guid",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Fish_Guid",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Fish");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Fish",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Fish",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_Guid",
                table: "User",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fish_Guid",
                table: "Fish",
                column: "Guid",
                unique: true);
        }
    }
}
