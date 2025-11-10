using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fintech.Migrations
{
    /// <inheritdoc />
    public partial class SeedRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84b1a093-3e77-422c-b91e-f2c9e5aee85d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a38b5ef1-d3e1-441d-9f4e-5cb0c037dccd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ecbbbbe-dd37-400d-9b5a-e0e10ac67427", null, "Admin", "ADMIN" },
                    { "50bf80a7-0da0-45ff-90bb-2a264af4e3bd", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ecbbbbe-dd37-400d-9b5a-e0e10ac67427");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50bf80a7-0da0-45ff-90bb-2a264af4e3bd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "84b1a093-3e77-422c-b91e-f2c9e5aee85d", null, "Admin", "ADMIN" },
                    { "a38b5ef1-d3e1-441d-9f4e-5cb0c037dccd", null, "User", "USER" }
                });
        }
    }
}
