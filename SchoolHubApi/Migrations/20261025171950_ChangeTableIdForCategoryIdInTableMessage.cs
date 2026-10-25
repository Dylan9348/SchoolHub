using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableIdForCategoryIdInTableMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_Tables_TableId",
                table: "TableMessage");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "TableMessage",
                newName: "CategoryId1");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessage_TableId",
                table: "TableMessage",
                newName: "IX_TableMessage_CategoryId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "TableMessage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TableMemberId",
                table: "TableMessage",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableMessage_TableMemberId",
                table: "TableMessage",
                column: "TableMemberId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_TableCategories_CategoryId1",
                table: "TableMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_TableMembers_TableMemberId",
                table: "TableMessage");

            migrationBuilder.DropIndex(
                name: "IX_TableMessage_TableMemberId",
                table: "TableMessage");

            migrationBuilder.DropColumn(
                name: "TableMemberId",
                table: "TableMessage");

            migrationBuilder.RenameColumn(
                name: "CategoryId1",
                table: "TableMessage",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMessage_CategoryId1",
                table: "TableMessage",
                newName: "IX_TableMessage_TableId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "TableMessage",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_Tables_TableId",
                table: "TableMessage",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
