using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationAPIs.Migrations
{
    /// <inheritdoc />
    public partial class refresh_token_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RefreshToken",
                value: "asdfghjklasdfghjkl");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RefreshToken",
                value: "asdfghjklasdfghjkl");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RefreshToken",
                value: "asdfghjklasdfghjkl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");
        }
    }
}
