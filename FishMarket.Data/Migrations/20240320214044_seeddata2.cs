using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FishMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class seeddata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: -4L);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: -3L);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.InsertData(
                table: "Fish",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "Name", "Price", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "60c39b56-ef92-4411-96d8-45a33d059f50.jpg", "Hamsi", 100.10m, null, null },
                    { 2L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "53df3af8-19e2-4836-b3a2-e6c70ec7ec17.png", "Levrek", 200.20m, null, null },
                    { 3L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "611e29ba-b49b-404b-a1da-3ebb9026c5cc.jpg", "Lüfer", 300.30m, null, null },
                    { 4L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "150eafe9-360e-4208-9647-2a96a88e964b.jpg", "Somon", 400.40m, null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "IsEmailVerified", "Password", "UpdatedBy", "UpdatedDate", "VerificationToken" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mehmetyagci53@gmail.com", true, "mehmetyagci53@gmail.com", null, null, "" },
                    { 2L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@fishmarket.com", true, "admin@fishmarket.com", null, null, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Fish",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.InsertData(
                table: "Fish",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "Name", "Price", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { -4L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "150eafe9-360e-4208-9647-2a96a88e964b.jpg", "Somon", 400.40m, null, null },
                    { -3L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "611e29ba-b49b-404b-a1da-3ebb9026c5cc.jpg", "Lüfer", 300.30m, null, null },
                    { -2L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "53df3af8-19e2-4836-b3a2-e6c70ec7ec17.png", "Levrek", 200.20m, null, null },
                    { -1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "60c39b56-ef92-4411-96d8-45a33d059f50.jpg", "Hamsi", 100.10m, null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "IsEmailVerified", "Password", "UpdatedBy", "UpdatedDate", "VerificationToken" },
                values: new object[,]
                {
                    { -2L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@fishmarket.com", true, "admin@fishmarket.com", null, null, "" },
                    { -1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mehmetyagci53@gmail.com", true, "mehmetyagci53@gmail.com", null, null, "" }
                });
        }
    }
}
