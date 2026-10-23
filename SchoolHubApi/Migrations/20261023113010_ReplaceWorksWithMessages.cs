using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceWorksWithMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_TableCategories_CategoryId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_CategoryId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Works");

            migrationBuilder.CreateTable(
                name: "TableMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    WorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableMessage_TableCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TableCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableMessage_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableMessage_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableMessage_CategoryId",
                table: "TableMessage",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TableMessage_TableId",
                table: "TableMessage",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_TableMessage_WorkId",
                table: "TableMessage",
                column: "WorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableMessage");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Works",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                table: "Works",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_TableCategories_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "TableCategories",
                principalColumn: "Id");
        }
    }
}
