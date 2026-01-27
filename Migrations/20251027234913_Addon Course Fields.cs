using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddonCourseFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CertificateAvailable",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CourseCode",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnrollCount",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateAvailable",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrollCount",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");
        }
    }
}
