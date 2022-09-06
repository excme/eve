using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchItems",
                table: "SearchItems");

            migrationBuilder.RenameTable(
                name: "SearchItems",
                newName: "ed_search");

            migrationBuilder.RenameIndex(
                name: "IX_SearchItems_type",
                table: "ed_search",
                newName: "IX_ed_search_type");

            migrationBuilder.RenameIndex(
                name: "IX_SearchItems_title",
                table: "ed_search",
                newName: "IX_ed_search_title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ed_search",
                table: "ed_search",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ed_search",
                table: "ed_search");

            migrationBuilder.RenameTable(
                name: "ed_search",
                newName: "SearchItems");

            migrationBuilder.RenameIndex(
                name: "IX_ed_search_type",
                table: "SearchItems",
                newName: "IX_SearchItems_type");

            migrationBuilder.RenameIndex(
                name: "IX_ed_search_title",
                table: "SearchItems",
                newName: "IX_SearchItems_title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchItems",
                table: "SearchItems",
                column: "id");
        }
    }
}
