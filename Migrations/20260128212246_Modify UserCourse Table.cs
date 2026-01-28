using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUserCourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_userCourses",
                table: "userCourses");

            migrationBuilder.RenameTable(
                name: "userCourses",
                newName: "UserCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourses",
                table: "UserCourses");

            migrationBuilder.RenameTable(
                name: "UserCourses",
                newName: "userCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userCourses",
                table: "userCourses",
                column: "Id");
        }
    }
}
