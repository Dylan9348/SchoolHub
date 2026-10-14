using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class TableMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMember_Tables_TableId",
                table: "TableMember");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMember_Users_UserId",
                table: "TableMember");

            migrationBuilder.DropForeignKey(
                name: "FK_TableWork_Tables_TableId",
                table: "TableWork");

            migrationBuilder.DropIndex(
                name: "IX_TableWork_TableId",
                table: "TableWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableMember",
                table: "TableMember");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "TableWork");

            migrationBuilder.RenameTable(
                name: "TableMember",
                newName: "TableMembers");

            migrationBuilder.RenameIndex(
                name: "IX_TableMember_UserId",
                table: "TableMembers",
                newName: "IX_TableMembers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMember_TableId_UserId",
                table: "TableMembers",
                newName: "IX_TableMembers_TableId_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Works",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableMembers",
                table: "TableMembers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                table: "Works",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_TableId",
                table: "Category",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMembers_Tables_TableId",
                table: "TableMembers",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMembers_Users_UserId",
                table: "TableMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Category_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableMembers_Tables_TableId",
                table: "TableMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TableMembers_Users_UserId",
                table: "TableMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Category_CategoryId",
                table: "Works");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Works_CategoryId",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableMembers",
                table: "TableMembers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Works");

            migrationBuilder.RenameTable(
                name: "TableMembers",
                newName: "TableMember");

            migrationBuilder.RenameIndex(
                name: "IX_TableMembers_UserId",
                table: "TableMember",
                newName: "IX_TableMember_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TableMembers_TableId_UserId",
                table: "TableMember",
                newName: "IX_TableMember_TableId_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "TableId",
                table: "TableWork",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableMember",
                table: "TableMember",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TableWork_TableId",
                table: "TableWork",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableMember_Tables_TableId",
                table: "TableMember",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableMember_Users_UserId",
                table: "TableMember",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableWork_Tables_TableId",
                table: "TableWork",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id");
        }
    }
}
