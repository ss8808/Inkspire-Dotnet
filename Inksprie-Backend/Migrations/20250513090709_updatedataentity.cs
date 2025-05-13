using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class updatedataentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89cc10d7-aa3c-4c53-ba0b-a5416f20af91", "AQAAAAIAAYagAAAAEAJAkF6ZQpzMPYGBEECXXPP8ZgAsyZtdgDosHvtPpi3SLvnIbuk1yN/JN6+a/U3D2A==", "808d52be-207d-47ed-adae-c72b5db22673" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59f2eea3-7ca6-4e83-93aa-1d2106c21023", "AQAAAAIAAYagAAAAEJoargzN9s1kbCXyIj+b69Dd7DRU/wYUaRINp4vQScB5tZN03JLuiCykAjFhNProQg==", "e8a5b392-89c8-43cd-a8be-1a5551b8c3ad" });
        }
    }
}
