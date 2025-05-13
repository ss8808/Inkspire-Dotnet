using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inksprie_Backend.Migrations
{
    /// <inheritdoc />
    public partial class OrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3eb497c6-ba77-49b1-99a3-417ca46c4a54", "AQAAAAIAAYagAAAAEPA+jXr15wwsEAJ+QTLEOsAQx5GBoAoZ2dGZaDx9O/NAoRMUsY8NT4VN260wCW4PwQ==", "8bb46fe2-f5e9-4b9b-b974-f839c9f5d1cf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0030b29e-68f0-402f-9eaa-dcfcac9434ef", "AQAAAAIAAYagAAAAEAPZzIQOY2eZfoPwD7vrT2B+AfS4qmpsEDh1Bt4dB5te/Z/oHi00rHa7PoRLApITzQ==", "0f068f2f-fef5-4ef0-ae3f-935987e1bccc" });
        }
    }
}
