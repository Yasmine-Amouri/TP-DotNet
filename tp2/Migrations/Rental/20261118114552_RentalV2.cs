using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp2.Migrations.Rental
{
    /// <inheritdoc />
    public partial class RentalV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "rental",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "rental",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipTypeId",
                schema: "rental",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                schema: "rental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId",
                schema: "rental",
                table: "Rentals",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MembershipTypeId",
                schema: "rental",
                table: "Customers",
                column: "MembershipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipTypeId",
                schema: "rental",
                table: "Customers",
                column: "MembershipTypeId",
                principalSchema: "rental",
                principalTable: "MembershipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customers_CustomerId",
                schema: "rental",
                table: "Rentals",
                column: "CustomerId",
                principalSchema: "rental",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MembershipTypes_MembershipTypeId",
                schema: "rental",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customers_CustomerId",
                schema: "rental",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "MembershipTypes",
                schema: "rental");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CustomerId",
                schema: "rental",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Customers_MembershipTypeId",
                schema: "rental",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "rental",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MembershipTypeId",
                schema: "rental",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "rental",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
