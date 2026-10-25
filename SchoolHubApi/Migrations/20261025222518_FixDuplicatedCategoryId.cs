using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class FixDuplicatedCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_TableCategories_CategoryId",
                table: "TableMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_TableCategories_CategoryId1",
                table: "TableMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_TableMembers_TableMemberId",
                table: "TableMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_Users_SenderId",
                table: "TableMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_Works_WorkId",
                table: "TableMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableMessage",
                table: "TableMessage");

            migrationBuilder.DropIndex(
                name: "IX_TableMessage_CategoryId1",
                table: "TableMessage");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "TableMessage");

            migrationBuilder.RenameTable(
                name: "TableMessage",
                newName: "TableMessages");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessage_WorkId",
                table: "TableMessages",
                newName: "IX_TableMessages_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessage_TableMemberId",
                table: "TableMessages",
                newName: "IX_TableMessages_TableMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessage_SenderId",
                table: "TableMessages",
                newName: "IX_TableMessages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessage_CategoryId",
                table: "TableMessages",
                newName: "IX_TableMessages_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableMessages",
                table: "TableMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessages_TableCategories_CategoryId",
                table: "TableMessages",
                column: "CategoryId",
                principalTable: "TableCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessages_TableMembers_TableMemberId",
                table: "TableMessages",
                column: "TableMemberId",
                principalTable: "TableMembers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessages_Users_SenderId",
                table: "TableMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessages_Works_WorkId",
                table: "TableMessages",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMessages_TableCategories_CategoryId",
                table: "TableMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessages_TableMembers_TableMemberId",
                table: "TableMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessages_Users_SenderId",
                table: "TableMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessages_Works_WorkId",
                table: "TableMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableMessages",
                table: "TableMessages");

            migrationBuilder.RenameTable(
                name: "TableMessages",
                newName: "TableMessage");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessages_WorkId",
                table: "TableMessage",
                newName: "IX_TableMessage_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessages_TableMemberId",
                table: "TableMessage",
                newName: "IX_TableMessage_TableMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessages_SenderId",
                table: "TableMessage",
                newName: "IX_TableMessage_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessages_CategoryId",
                table: "TableMessage",
                newName: "IX_TableMessage_CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "TableMessage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableMessage",
                table: "TableMessage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableMessage_CategoryId1",
                table: "TableMessage",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_TableCategories_CategoryId",
                table: "TableMessage",
                column: "CategoryId",
                principalTable: "TableCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_TableCategories_CategoryId1",
                table: "TableMessage",
                column: "CategoryId1",
                principalTable: "TableCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_TableMembers_TableMemberId",
                table: "TableMessage",
                column: "TableMemberId",
                principalTable: "TableMembers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_Users_SenderId",
                table: "TableMessage",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_Works_WorkId",
                table: "TableMessage",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
