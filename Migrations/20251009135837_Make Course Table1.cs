using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy_Backend.Migrations
{
    /// <inheritdoc />
    public partial class MakeCourseTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TagId1",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TagId1",
                table: "Courses",
                column: "TagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Tags_TagId1",
                table: "Courses",
                column: "TagId1",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Tags_TagId1",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TagId1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TagId1",
                table: "Courses");
        }
    }
}
