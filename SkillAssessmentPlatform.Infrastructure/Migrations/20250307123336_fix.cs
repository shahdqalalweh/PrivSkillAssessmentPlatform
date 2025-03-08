using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46ffb756-f4c5-4dbe-988b-8bed1ba13d88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "969e25ee-0773-4b1b-80c4-d683f9d3f1cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bc99e9e-973a-4e33-bce8-9e1ea83c82ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa3171e0-c407-40ac-a259-1ab687464e5d");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46ffb756-f4c5-4dbe-988b-8bed1ba13d88", null, "Examiner", "EXAMINER" },
                    { "969e25ee-0773-4b1b-80c4-d683f9d3f1cb", null, "Admin", "ADMIN" },
                    { "9bc99e9e-973a-4e33-bce8-9e1ea83c82ec", null, "SeniorExaminer", "SENIOREXAMINER" },
                    { "aa3171e0-c407-40ac-a259-1ab687464e5d", null, "Applicant", "APPLICANT" }
                });
        }
    }
}
