using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class roleseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81e79a46-54e0-4280-a51f-e69c1525f972", null, "Examiner", "EXAMINER" },
                    { "e4ab1a40-ec7f-43c7-b374-450e9afa7aff", null, "Admin", "ADMIN" },
                    { "fb180cbc-6d10-421d-b1eb-829846fe0375", null, "SeniorExaminer", "SENIOREXAMINER" },
                    { "fdb45f9f-260e-4634-8c62-e24d9a6b47e7", null, "Applicant", "APPLICANT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81e79a46-54e0-4280-a51f-e69c1525f972");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4ab1a40-ec7f-43c7-b374-450e9afa7aff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb180cbc-6d10-421d-b1eb-829846fe0375");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdb45f9f-260e-4634-8c62-e24d9a6b47e7");
        }
    }
}
