using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAny : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBookmarks",
                table: "BookBookmarks");

            migrationBuilder.RenameTable(
                name: "BookBookmarks",
                newName: "Bookmarks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8658780d-ce45-43ba-9e7e-197d4107c289", "AQAAAAIAAYagAAAAEGxFHgJ6zQV4wz0A5XJtRa+qQHJOpEZEHDszwrDXO7jamCZVVHtXx52h9avmp2588Q==", "6c07aa03-89ab-4812-bb69-dd15f417dceb" });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_BookId",
                table: "Discounts",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Books_BookId",
                table: "Discounts",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Books_BookId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_BookId",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookmarks",
                table: "Bookmarks");

            migrationBuilder.RenameTable(
                name: "Bookmarks",
                newName: "BookBookmarks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBookmarks",
                table: "BookBookmarks",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d3c51f7-d690-4ad6-b69e-445391b751b1", "AQAAAAIAAYagAAAAEEojCp1njGwFI6uXnwClIbaoXOeaNhxg3dQy3S3rTlhnDv+Izf+F5BZq07MPh/AuPQ==", "6ab7f1cf-3292-4fe1-bc19-9954255c7b90" });
        }
    }
}
