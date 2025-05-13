using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class orderuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5735e196-1c93-4a55-8825-de89305e522a", "AQAAAAIAAYagAAAAEMkjwPINz3Py/OKzosbCAJhTT4pA4ZGpJyzsWgrPADh3GTiMn7erEL5p/VXF3H2SpA==", "adc0dc2c-4988-4b21-b4bb-4fdb68bfe73d" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89cc10d7-aa3c-4c53-ba0b-a5416f20af91", "AQAAAAIAAYagAAAAEAJAkF6ZQpzMPYGBEECXXPP8ZgAsyZtdgDosHvtPpi3SLvnIbuk1yN/JN6+a/U3D2A==", "808d52be-207d-47ed-adae-c72b5db22673" });
        }
    }
}
