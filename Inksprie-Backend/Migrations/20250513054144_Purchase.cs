using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Purchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Purchases",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "762fbd47-29d8-45b6-b39e-ea7d09973702", "AQAAAAIAAYagAAAAEHLEEtY0Nd0DrBUb5Y1ANLmPm0vOS/++NLc16Sm/6v4IoUXniaNR5jpEkQYKDidG+g==", "697d9698-2680-47b3-9b6c-1cd12c00576b" });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BookId",
                table: "Purchases",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_OrderId",
                table: "Purchases",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Books_BookId",
                table: "Purchases",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Orders_OrderId",
                table: "Purchases",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Books_BookId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Orders_OrderId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_BookId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_OrderId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Purchases");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3eb497c6-ba77-49b1-99a3-417ca46c4a54", "AQAAAAIAAYagAAAAEPA+jXr15wwsEAJ+QTLEOsAQx5GBoAoZ2dGZaDx9O/NAoRMUsY8NT4VN260wCW4PwQ==", "8bb46fe2-f5e9-4b9b-b974-f839c9f5d1cf" });
        }
    }
}
