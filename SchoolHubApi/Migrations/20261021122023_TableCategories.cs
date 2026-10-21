using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class TableCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Tables_TableId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Category_CategoryId",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "TableCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Category_TableId",
                table: "TableCategories",
                newName: "IX_TableCategories_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableCategories",
                table: "TableCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableCategories_Tables_TableId",
                table: "TableCategories",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_TableCategories_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "TableCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableCategories_Tables_TableId",
                table: "TableCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_TableCategories_CategoryId",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableCategories",
                table: "TableCategories");

            migrationBuilder.RenameTable(
                name: "TableCategories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_TableCategories_TableId",
                table: "Category",
                newName: "IX_Category_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Tables_TableId",
                table: "Category",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Category_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
