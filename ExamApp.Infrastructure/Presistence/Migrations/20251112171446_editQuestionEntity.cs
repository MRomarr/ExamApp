using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApp.Infrastructure.presistence.Migrations
{
    /// <inheritdoc />
    public partial class editQuestionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_QuestionOptions_SelectedQuestionOptionId",
                table: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_SelectedQuestionOptionId",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "SelectedAnswerOptionId",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "SelectedQuestionOptionId",
                table: "StudentAnswers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedAt",
                table: "StudentExams",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "SelectedOptionIndex",
                table: "StudentAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectOptionIndex",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedOptionIndex",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "CorrectOptionIndex",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "Questions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedAt",
                table: "StudentExams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedAnswerOptionId",
                table: "StudentAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedQuestionOptionId",
                table: "StudentAnswers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_SelectedQuestionOptionId",
                table: "StudentAnswers",
                column: "SelectedQuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_QuestionOptions_SelectedQuestionOptionId",
                table: "StudentAnswers",
                column: "SelectedQuestionOptionId",
                principalTable: "QuestionOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
