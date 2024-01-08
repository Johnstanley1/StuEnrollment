using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JAjagu_Assignment2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseInstructor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfStudent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseInstructor", "CourseName", "NumberOfStudent", "RoomNumber", "StartDate" },
                values: new object[,]
                {
                    { 100, "Jasveen Kaur", "Programing Concepts 1", 2, "1C09", new DateTime(2023, 10, 18, 20, 15, 45, 342, DateTimeKind.Local).AddTicks(9113) },
                    { 101, "Yash Shah", "System Analysis", 2, "4G25", new DateTime(2023, 11, 2, 20, 15, 45, 342, DateTimeKind.Local).AddTicks(9187) },
                    { 102, "Rick Guzik", "UX/UI Experience", 2, "2B25", new DateTime(2023, 10, 13, 20, 15, 45, 342, DateTimeKind.Local).AddTicks(9191) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "CourseId", "Status", "StudentEmail", "StudentName" },
                values: new object[,]
                {
                    { 1, 100, "ConfirmationMessageNotSent", "barts@gmail.com", "Bart Simpson" },
                    { 2, 100, "ConfirmationMessageNotSent", "lbart@yahoo.com", "Lisa Bart" },
                    { 3, 101, "ConfirmationMessageNotSent", "culjon@yahoo.com", "Ajagu" },
                    { 4, 101, "ConfirmationMessageNotSent", "culjon@yahoo.com", "Amos Mars" },
                    { 5, 102, "ConfirmationMessageNotSent", "culjon@yahoo.com", "James Cordon" },
                    { 6, 102, "ConfirmationMessageNotSent", "culjon@yahoo.com", "Jordan Holmes" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
