using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class idkwiddhr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_TeacherUsers_TeacherUserId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_TeacherUsers_TeacherUserId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_TeacherUsers_TeacherUserId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Schools_TeacherUserId",
                table: "Schools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherUsers",
                table: "TeacherUsers");

            migrationBuilder.DropColumn(
                name: "TeacherUserId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "LastNames",
                table: "TeacherUsers");

            migrationBuilder.DropColumn(
                name: "Names",
                table: "TeacherUsers");

            migrationBuilder.RenameTable(
                name: "TeacherUsers",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "TeacherReports",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "RefreshTokens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentUserId",
                table: "Classrooms",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelfId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherUser_SelfId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PendingRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CodeHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valid = table.Column<bool>(type: "boolean", nullable: false),
                    Attempts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingRegistrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_StudentUserId",
                table: "Classrooms",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SelfId",
                table: "Users",
                column: "SelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeacherUser_SelfId",
                table: "Users",
                column: "TeacherUser_SelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Users_StudentUserId",
                table: "Classrooms",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Users_TeacherUserId",
                table: "Classrooms",
                column: "TeacherUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Students_SelfId",
                table: "Users",
                column: "SelfId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teachers_TeacherUser_SelfId",
                table: "Users",
                column: "TeacherUser_SelfId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Users_TeacherUserId",
                table: "Works",
                column: "TeacherUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Users_StudentUserId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Users_TeacherUserId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Students_SelfId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teachers_TeacherUser_SelfId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Users_TeacherUserId",
                table: "Works");

            migrationBuilder.DropTable(
                name: "PendingRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_StudentUserId",
                table: "Classrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SelfId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TeacherUser_SelfId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "TeacherReports");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "SelfId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeacherUser_SelfId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "TeacherUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherUserId",
                table: "Schools",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNames",
                table: "TeacherUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Names",
                table: "TeacherUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherUsers",
                table: "TeacherUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_TeacherUserId",
                table: "Schools",
                column: "TeacherUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_TeacherUsers_TeacherUserId",
                table: "Classrooms",
                column: "TeacherUserId",
                principalTable: "TeacherUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_TeacherUsers_TeacherUserId",
                table: "Schools",
                column: "TeacherUserId",
                principalTable: "TeacherUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_TeacherUsers_TeacherUserId",
                table: "Works",
                column: "TeacherUserId",
                principalTable: "TeacherUsers",
                principalColumn: "Id");
        }
    }
}
