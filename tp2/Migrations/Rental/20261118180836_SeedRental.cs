using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tp2.Migrations.Rental
{
    /// <inheritdoc />
    public partial class SeedRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "rental",
                table: "MembershipTypes",
                columns: new[] { "Id", "DiscountPercent", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Standard" },
                    { 2, 10, "Premium" },
                    { 3, 20, "Gold" }
                });

            migrationBuilder.InsertData(
                schema: "rental",
                table: "Customers",
                columns: new[] { "Id", "Email", "MembershipTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "amal@example.com", 2, "Amal Ben Salah" },
                    { 2, "youssef@example.com", 1, "Youssef Trabelsi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "rental",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "rental",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "rental",
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "rental",
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "rental",
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
