using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class TableMessageSenderAndPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pending",
                table: "TableMessage",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "TableMessage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TableMessage_SenderId",
                table: "TableMessage",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMessage_Users_SenderId",
                table: "TableMessage",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMessage_Users_SenderId",
                table: "TableMessage");

            migrationBuilder.DropIndex(
                name: "IX_TableMessage_SenderId",
                table: "TableMessage");

            migrationBuilder.DropColumn(
                name: "Pending",
                table: "TableMessage");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "TableMessage");
        }
    }
}
