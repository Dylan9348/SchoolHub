using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class WorkCreatorAndUIntMaxValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Users_TeacherUserId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_TeacherUserId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "TeacherUserId",
                table: "Works");

            migrationBuilder.AlterColumn<long>(
                name: "MaxValue",
                table: "Works",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Works",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Works_CreatorId",
                table: "Works",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Users_CreatorId",
                table: "Works",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Users_CreatorId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_CreatorId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "MaxValue",
                table: "Works",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherUserId",
                table: "Works",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_TeacherUserId",
                table: "Works",
                column: "TeacherUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Users_TeacherUserId",
                table: "Works",
                column: "TeacherUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
