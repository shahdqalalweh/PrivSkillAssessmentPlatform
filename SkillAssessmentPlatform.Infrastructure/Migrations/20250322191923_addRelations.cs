using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillAssessmentPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminerLoad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2621f9b8-07e3-403c-bb6f-404539928e09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "605e416a-a428-4eef-87d3-795c17e33095");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7526f193-77b7-4cd3-af2f-66eedb96eb08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dab7c4d0-0b55-4af8-9bbf-f06c11debc1f");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Examiners",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminerId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    ExaminerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Examiners_ExaminerId1",
                        column: x => x.ExaminerId1,
                        principalTable: "Examiners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExaminerLoads",
                columns: table => new
                {
                    ExaminerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    MaxWorkLoad = table.Column<int>(type: "int", nullable: false),
                    CurrWorkLoad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminerLoads", x => new { x.ExaminerID, x.Type });
                    table.ForeignKey(
                        name: "FK_ExaminerLoads_Examiners_ExaminerID",
                        column: x => x.ExaminerID,
                        principalTable: "Examiners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminerId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExaminerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Examiners_ExaminerId1",
                        column: x => x.ExaminerId1,
                        principalTable: "Examiners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeniorExaminerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objectives = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssociatedSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Examiners_SeniorExaminerID",
                        column: x => x.SeniorExaminerID,
                        principalTable: "Examiners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Applicants_ApplicantId1",
                        column: x => x.ApplicantId1,
                        principalTable: "Applicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Enrollments_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExaminerTrack",
                columns: table => new
                {
                    ExaminersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkingTracksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminerTrack", x => new { x.ExaminersId, x.WorkingTracksId });
                    table.ForeignKey(
                        name: "FK_ExaminerTrack_Examiners_ExaminersId",
                        column: x => x.ExaminersId,
                        principalTable: "Examiners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExaminerTrack_Tracks_WorkingTracksId",
                        column: x => x.WorkingTracksId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LevelProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelProgresses_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LevelProgresses_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PassingScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stages_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    LeveProgressId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LevelProgressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificate_Applicants_ApplicantId1",
                        column: x => x.ApplicantId1,
                        principalTable: "Applicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificate_LevelProgresses_LevelProgressId",
                        column: x => x.LevelProgressId,
                        principalTable: "LevelProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCriteria_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionsType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    MaxDaysToBook = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StageProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ExaminerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    ExaminerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LevelProgressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageProgresses_Examiners_ExaminerId1",
                        column: x => x.ExaminerId1,
                        principalTable: "Examiners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StageProgresses_LevelProgresses_LevelProgressId",
                        column: x => x.LevelProgressId,
                        principalTable: "LevelProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StageProgresses_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TasksPools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    DaysToSubmit = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksPools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksPools_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailedFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackId = table.Column<int>(type: "int", nullable: false),
                    CriterionId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EvaluationCriteriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailedFeedbacks_EvaluationCriteria_EvaluationCriteriaId",
                        column: x => x.EvaluationCriteriaId,
                        principalTable: "EvaluationCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailedFeedbacks_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamRequests_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamRequests_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewBooks_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewBooks_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewBooks_Interviews_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskPoolId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TasksPoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_TasksPools_TasksPoolId",
                        column: x => x.TasksPoolId,
                        principalTable: "TasksPools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskApplicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StageProgressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskApplicants_StageProgresses_StageProgressId",
                        column: x => x.StageProgressId,
                        principalTable: "StageProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskApplicants_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskApplicantId = table.Column<int>(type: "int", nullable: false),
                    SubmissionUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSubmissions_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskSubmissions_TaskApplicants_TaskApplicantId",
                        column: x => x.TaskApplicantId,
                        principalTable: "TaskApplicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Examiners_UserId",
                table: "Examiners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ExaminerId1",
                table: "Appointments",
                column: "ExaminerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_ApplicantId1",
                table: "Certificate",
                column: "ApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_LevelProgressId",
                table: "Certificate",
                column: "LevelProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedFeedbacks_EvaluationCriteriaId",
                table: "DetailedFeedbacks",
                column: "EvaluationCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedFeedbacks_FeedbackId",
                table: "DetailedFeedbacks",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ApplicantId1",
                table: "Enrollments",
                column: "ApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TrackId",
                table: "Enrollments",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_StageId",
                table: "EvaluationCriteria",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminerTrack_WorkingTracksId",
                table: "ExaminerTrack",
                column: "WorkingTracksId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_ExamId",
                table: "ExamRequests",
                column: "ExamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamRequests_FeedbackId",
                table: "ExamRequests",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StageId",
                table: "Exams",
                column: "StageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ExaminerId1",
                table: "Feedbacks",
                column: "ExaminerId1");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewBooks_AppointmentId",
                table: "InterviewBooks",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewBooks_FeedbackId",
                table: "InterviewBooks",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewBooks_InterviewId",
                table: "InterviewBooks",
                column: "InterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_StageId",
                table: "Interviews",
                column: "StageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LevelProgresses_EnrollmentId",
                table: "LevelProgresses",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelProgresses_LevelId",
                table: "LevelProgresses",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_TrackId",
                table: "Levels",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId1",
                table: "Notification",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_StageProgresses_ExaminerId1",
                table: "StageProgresses",
                column: "ExaminerId1");

            migrationBuilder.CreateIndex(
                name: "IX_StageProgresses_LevelProgressId",
                table: "StageProgresses",
                column: "LevelProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_StageProgresses_StageId",
                table: "StageProgresses",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_LevelId",
                table: "Stages",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApplicants_StageProgressId",
                table: "TaskApplicants",
                column: "StageProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskApplicants_TaskId",
                table: "TaskApplicants",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TasksPoolId",
                table: "Tasks",
                column: "TasksPoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TasksPools_StageId",
                table: "TasksPools",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_FeedbackId",
                table: "TaskSubmissions",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_TaskApplicantId",
                table: "TaskSubmissions",
                column: "TaskApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_SeniorExaminerID",
                table: "Tracks",
                column: "SeniorExaminerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Examiners_Users_UserId",
                table: "Examiners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examiners_Users_UserId",
                table: "Examiners");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "DetailedFeedbacks");

            migrationBuilder.DropTable(
                name: "ExaminerLoads");

            migrationBuilder.DropTable(
                name: "ExaminerTrack");

            migrationBuilder.DropTable(
                name: "ExamRequests");

            migrationBuilder.DropTable(
                name: "InterviewBooks");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "TaskSubmissions");

            migrationBuilder.DropTable(
                name: "EvaluationCriteria");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "TaskApplicants");

            migrationBuilder.DropTable(
                name: "StageProgresses");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "LevelProgresses");

            migrationBuilder.DropTable(
                name: "TasksPools");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Examiners_UserId",
                table: "Examiners");

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
                name: "UserId",
                table: "Examiners");

            migrationBuilder.CreateTable(
                name: "ExaminerLoad",
                columns: table => new
                {
                    ExaminerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CurrWorkLoad = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    MaxWorkLoad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminerLoad", x => new { x.ExaminerID, x.Type });
                    table.ForeignKey(
                        name: "FK_ExaminerLoad_Examiners_ExaminerID",
                        column: x => x.ExaminerID,
                        principalTable: "Examiners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2621f9b8-07e3-403c-bb6f-404539928e09", null, "Examiner", "EXAMINER" },
                    { "605e416a-a428-4eef-87d3-795c17e33095", null, "SeniorExaminer", "SENIOREXAMINER" },
                    { "7526f193-77b7-4cd3-af2f-66eedb96eb08", null, "Applicant", "APPLICANT" },
                    { "dab7c4d0-0b55-4af8-9bbf-f06c11debc1f", null, "Admin", "ADMIN" }
                });
        }
    }
}
