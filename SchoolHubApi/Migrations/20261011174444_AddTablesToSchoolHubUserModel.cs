using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToSchoolHubUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentUserId",
                table: "Tables",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_StudentUserId",
                table: "Tables",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Users_StudentUserId",
                table: "Tables",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Users_StudentUserId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_StudentUserId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Tables");
        }
    }
}
