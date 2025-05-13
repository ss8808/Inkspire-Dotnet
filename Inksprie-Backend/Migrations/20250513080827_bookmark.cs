using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class bookmark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59f2eea3-7ca6-4e83-93aa-1d2106c21023", "AQAAAAIAAYagAAAAEJoargzN9s1kbCXyIj+b69Dd7DRU/wYUaRINp4vQScB5tZN03JLuiCykAjFhNProQg==", "e8a5b392-89c8-43cd-a8be-1a5551b8c3ad" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_BookId",
                table: "Bookmarks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Books_BookId",
                table: "Bookmarks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Books_BookId",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_BookId",
                table: "Bookmarks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "762fbd47-29d8-45b6-b39e-ea7d09973702", "AQAAAAIAAYagAAAAEHLEEtY0Nd0DrBUb5Y1ANLmPm0vOS/++NLc16Sm/6v4IoUXniaNR5jpEkQYKDidG+g==", "697d9698-2680-47b3-9b6c-1cd12c00576b" });
        }
    }
}
