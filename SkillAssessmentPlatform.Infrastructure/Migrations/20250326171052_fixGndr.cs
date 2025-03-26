using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixGndr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Examiners_ExaminerId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Applicants_ApplicantId1",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_LevelProgresses_LevelProgressId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Applicants_ApplicantId1",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Examiners_ExaminerId1",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_UserId1",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_StageProgresses_Examiners_ExaminerId1",
                table: "StageProgresses");

            migrationBuilder.DropIndex(
                name: "IX_StageProgresses_ExaminerId1",
                table: "StageProgresses");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserId1",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ExaminerId1",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_ApplicantId1",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ExaminerId1",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificate",
                table: "Certificate");

            migrationBuilder.DropIndex(
                name: "IX_Certificate_ApplicantId1",
                table: "Certificate");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3652be4c-93ff-4abd-b6f3-83622f5d81f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0c1f138-d0ca-441a-8689-22f453e3e4b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8e136f4-e602-40bd-8f0d-13fa7e978347");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef3f52a3-88f2-420c-b9a7-0068e2eb369d");

            migrationBuilder.DropColumn(
                name: "ExaminerId1",
                table: "StageProgresses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ExaminerId1",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ApplicantId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "ExaminerId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ApplicantId1",
                table: "Certificate");

            migrationBuilder.RenameTable(
                name: "Certificate",
                newName: "Certificates");

            migrationBuilder.RenameColumn(
                name: "Gendar",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_LevelProgressId",
                table: "Certificates",
                newName: "IX_Certificates_LevelProgressId");

            migrationBuilder.AlterColumn<string>(
                name: "ExaminerId",
                table: "StageProgresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalScore",
                table: "Feedbacks",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ExaminerId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "Enrollments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "DetailedFeedbacks",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ExaminerId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantId",
                table: "Certificates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_StageProgresses_ExaminerId",
                table: "StageProgresses",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ExaminerId",
                table: "Feedbacks",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ApplicantId",
                table: "Enrollments",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExaminerId",
                table: "Appointments",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ApplicantId",
                table: "Certificates",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Examiners_ExaminerId",
                table: "Appointments",
                column: "ExaminerId",
                principalTable: "Examiners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Applicants_ApplicantId",
                table: "Certificates",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_LevelProgresses_LevelProgressId",
                table: "Certificates",
                column: "LevelProgressId",
                principalTable: "LevelProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Applicants_ApplicantId",
                table: "Enrollments",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Examiners_ExaminerId",
                table: "Feedbacks",
                column: "ExaminerId",
                principalTable: "Examiners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StageProgresses_Examiners_ExaminerId",
                table: "StageProgresses",
                column: "ExaminerId",
                principalTable: "Examiners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Examiners_ExaminerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Applicants_ApplicantId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_LevelProgresses_LevelProgressId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Applicants_ApplicantId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Examiners_ExaminerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Users_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_StageProgresses_Examiners_ExaminerId",
                table: "StageProgresses");

            migrationBuilder.DropIndex(
                name: "IX_StageProgresses_ExaminerId",
                table: "StageProgresses");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ExaminerId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_ApplicantId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ExaminerId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificates",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ApplicantId",
                table: "Certificates");

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

            migrationBuilder.RenameTable(
                name: "Certificates",
                newName: "Certificate");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "Gendar");

            migrationBuilder.RenameIndex(
                name: "IX_Certificates_LevelProgressId",
                table: "Certificate",
                newName: "IX_Certificate_LevelProgressId");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminerId",
                table: "StageProgresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExaminerId1",
                table: "StageProgresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Notification",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalScore",
                table: "Feedbacks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminerId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExaminerId1",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantId1",
                table: "Enrollments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "DetailedFeedbacks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminerId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExaminerId1",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "Certificate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantId1",
                table: "Certificate",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificate",
                table: "Certificate",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3652be4c-93ff-4abd-b6f3-83622f5d81f6", null, "Admin", "ADMIN" },
                    { "a0c1f138-d0ca-441a-8689-22f453e3e4b3", null, "Examiner", "EXAMINER" },
                    { "a8e136f4-e602-40bd-8f0d-13fa7e978347", null, "Applicant", "APPLICANT" },
                    { "ef3f52a3-88f2-420c-b9a7-0068e2eb369d", null, "SeniorExaminer", "SENIOREXAMINER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageProgresses_ExaminerId1",
                table: "StageProgresses",
                column: "ExaminerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId1",
                table: "Notification",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ExaminerId1",
                table: "Feedbacks",
                column: "ExaminerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ApplicantId1",
                table: "Enrollments",
                column: "ApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExaminerId1",
                table: "Appointments",
                column: "ExaminerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_ApplicantId1",
                table: "Certificate",
                column: "ApplicantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Examiners_ExaminerId1",
                table: "Appointments",
                column: "ExaminerId1",
                principalTable: "Examiners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Applicants_ApplicantId1",
                table: "Certificate",
                column: "ApplicantId1",
                principalTable: "Applicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_LevelProgresses_LevelProgressId",
                table: "Certificate",
                column: "LevelProgressId",
                principalTable: "LevelProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Applicants_ApplicantId1",
                table: "Enrollments",
                column: "ApplicantId1",
                principalTable: "Applicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Examiners_ExaminerId1",
                table: "Feedbacks",
                column: "ExaminerId1",
                principalTable: "Examiners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Users_UserId1",
                table: "Notification",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StageProgresses_Examiners_ExaminerId1",
                table: "StageProgresses",
                column: "ExaminerId1",
                principalTable: "Examiners",
                principalColumn: "Id");
        }
    }
}
