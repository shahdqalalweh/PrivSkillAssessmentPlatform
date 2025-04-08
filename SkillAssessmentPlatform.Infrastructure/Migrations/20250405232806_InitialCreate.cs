using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a7d4fd6-99a5-48c7-96ac-92f5572dfb05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "413f0cf7-2dd5-4b5f-99f8-4623581cd556");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6876ca37-1d03-4a73-b9d6-34075637dd6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d96ff5a8-1449-4f30-9889-aa87f53894c8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a8bd976-c7a5-43cf-996f-c4c14da4ac1c", null, "Admin", "ADMIN" },
                    { "2c442c60-5fd2-4051-a706-7cd15b6ab9e5", null, "Examiner", "EXAMINER" },
                    { "cce6b340-fbc8-42fd-b66d-e3a29fdd0b05", null, "Applicant", "APPLICANT" },
                    { "e9686f29-e4e1-4def-9b1d-1722f3bf7ce3", null, "SeniorExaminer", "SENIOREXAMINER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a8bd976-c7a5-43cf-996f-c4c14da4ac1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c442c60-5fd2-4051-a706-7cd15b6ab9e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cce6b340-fbc8-42fd-b66d-e3a29fdd0b05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9686f29-e4e1-4def-9b1d-1722f3bf7ce3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a7d4fd6-99a5-48c7-96ac-92f5572dfb05", null, "Examiner", "EXAMINER" },
                    { "413f0cf7-2dd5-4b5f-99f8-4623581cd556", null, "Applicant", "APPLICANT" },
                    { "6876ca37-1d03-4a73-b9d6-34075637dd6c", null, "SeniorExaminer", "SENIOREXAMINER" },
                    { "d96ff5a8-1449-4f30-9889-aa87f53894c8", null, "Admin", "ADMIN" }
                });
        }
    }
}
