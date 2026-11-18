using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tp2.Migrations.Catalog
{
    /// <inheritdoc />
    public partial class CatalogV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropColumn(name: "GenreId", table: "Movies");
            migrationBuilder.DropColumn(name: "Id", table: "Genres");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3f2b8c4e-5a1d-4e7b-9c60-1d2e3f4a5b6c"), "Science-fiction" },
                    { new Guid("79e6f638-d7e7-4f63-8365-f172cb925381"), "Drame" },
                    { new Guid("84ca0bcd-082c-49cb-aa77-ea2f1f5f8285"), "Action" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "IsAvailable", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("3f2b8c4e-5a1d-4e7b-9c60-1d2e3f4a5b6c"), true, "Inception" },
                    { 2, new Guid("84ca0bcd-082c-49cb-aa77-ea2f1f5f8285"), true, "Gladiator" },
                    { 3, new Guid("3f2b8c4e-5a1d-4e7b-9c60-1d2e3f4a5b6c"), true, "Interstellar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("79e6f638-d7e7-4f63-8365-f172cb925381"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3f2b8c4e-5a1d-4e7b-9c60-1d2e3f4a5b6c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("84ca0bcd-082c-49cb-aa77-ea2f1f5f8285"));

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Drame" },
                    { 3, "Comédie" }
                });
        }
    }
}
