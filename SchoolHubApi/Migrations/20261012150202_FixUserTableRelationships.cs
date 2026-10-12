using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class FixUserTableRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMember_Tables_TableId",
                table: "TableMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Users_OwnerId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Users_StudentUserId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OwnerId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_StudentUserId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_TableMember_TableId",
                table: "TableMember");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "Tables");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tables",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "TableId",
                table: "TableMember",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolHubUserId",
                table: "Schools",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OwnerId_Name",
                table: "Tables",
                columns: new[] { "OwnerId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableMember_TableId_UserId",
                table: "TableMember",
                columns: new[] { "TableId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolHubUserId",
                table: "Schools",
                column: "SchoolHubUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Users_SchoolHubUserId",
                table: "Schools",
                column: "SchoolHubUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMember_Tables_TableId",
                table: "TableMember",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Users_OwnerId",
                table: "Tables",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Users_SchoolHubUserId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMember_Tables_TableId",
                table: "TableMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Users_OwnerId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OwnerId_Name",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_TableMember_TableId_UserId",
                table: "TableMember");

            migrationBuilder.DropIndex(
                name: "IX_Schools_SchoolHubUserId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "SchoolHubUserId",
                table: "Schools");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentUserId",
                table: "Tables",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TableId",
                table: "TableMember",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OwnerId",
                table: "Tables",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_StudentUserId",
                table: "Tables",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TableMember_TableId",
                table: "TableMember",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMember_Tables_TableId",
                table: "TableMember",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Users_OwnerId",
                table: "Tables",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Users_StudentUserId",
                table: "Tables",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
