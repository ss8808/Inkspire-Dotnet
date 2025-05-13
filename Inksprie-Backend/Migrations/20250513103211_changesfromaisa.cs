using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class changesfromaisa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a146d1db-37b4-4faa-a8d4-c20da34400b4", "AQAAAAIAAYagAAAAEEzs5bHvNZxJUN18cMDY9UsMYaFTonUoyRPhrXzyYheKOtzlzW/BQh4BCGkr94lisA==", "d1a93ce8-0e25-406d-997d-6a0c4e07250b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58c62fa8-f519-4933-933a-d6b4cfad9115", "AQAAAAIAAYagAAAAED2s91DFKfo4pqlNGA89eerVboo59FOiWa/Hx6tQyT42kTy9Ki6bJK/GzhB3R1njQw==", "7c047221-9761-4142-a11f-7587a953d578" });
        }
    }
}
