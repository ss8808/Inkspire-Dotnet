using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class OrderHistoryChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58c62fa8-f519-4933-933a-d6b4cfad9115", "AQAAAAIAAYagAAAAED2s91DFKfo4pqlNGA89eerVboo59FOiWa/Hx6tQyT42kTy9Ki6bJK/GzhB3R1njQw==", "7c047221-9761-4142-a11f-7587a953d578" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5735e196-1c93-4a55-8825-de89305e522a", "AQAAAAIAAYagAAAAEMkjwPINz3Py/OKzosbCAJhTT4pA4ZGpJyzsWgrPADh3GTiMn7erEL5p/VXF3H2SpA==", "adc0dc2c-4988-4b21-b4bb-4fdb68bfe73d" });
        }
    }
}
