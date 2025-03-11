using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e541a99-38c6-4041-b935-9d887b9cc8ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55dc4ec3-ad6d-4d28-baad-a062d5e11ce8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1c9c5a6-0a14-482a-88df-3e6f2d349ce2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc573bc0-2e2f-4537-b460-6f4cae915cc5");

            migrationBuilder.AlterColumn<string>(
                name: "ExaminerID",
                table: "Applicants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_ExaminerID",
                table: "Applicants",
                column: "ExaminerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Examiners_ExaminerID",
                table: "Applicants",
                column: "ExaminerID",
                principalTable: "Examiners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Examiners_ExaminerID",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_ExaminerID",
                table: "Applicants");

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

            migrationBuilder.AlterColumn<string>(
                name: "ExaminerID",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e541a99-38c6-4041-b935-9d887b9cc8ad", null, "Applicant", "APPLICANT" },
                    { "55dc4ec3-ad6d-4d28-baad-a062d5e11ce8", null, "SeniorExaminer", "SENIOREXAMINER" },
                    { "e1c9c5a6-0a14-482a-88df-3e6f2d349ce2", null, "Examiner", "EXAMINER" },
                    { "fc573bc0-2e2f-4537-b460-6f4cae915cc5", null, "Admin", "ADMIN" }
                });
        }
    }
}
