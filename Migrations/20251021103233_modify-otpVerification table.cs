using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy_Backend.Migrations
{
    /// <inheritdoc />
    public partial class modifyotpVerificationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "OtpVerifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "OtpVerifications");
        }
    }
}
