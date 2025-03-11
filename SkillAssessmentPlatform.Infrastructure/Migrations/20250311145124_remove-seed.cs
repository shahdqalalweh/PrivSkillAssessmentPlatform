using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f93af27-14d7-45b5-a038-5626f781f68e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a7b8445-8342-492e-9f47-97bc982ea0ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b8e1cb4-3c90-45e2-b52e-0eb6177b2d77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1e36f78-d572-40bc-a070-38e54e6faff0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f93af27-14d7-45b5-a038-5626f781f68e", null, "Admin", "ADMIN" },
                    { "5a7b8445-8342-492e-9f47-97bc982ea0ec", null, "SeniorExaminer", "SENIOREXAMINER" },
                    { "9b8e1cb4-3c90-45e2-b52e-0eb6177b2d77", null, "Examiner", "EXAMINER" },
                    { "a1e36f78-d572-40bc-a070-38e54e6faff0", null, "Applicant", "APPLICANT" }
                });
        }
    }
}
