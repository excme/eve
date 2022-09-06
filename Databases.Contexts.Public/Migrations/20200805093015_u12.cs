using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "update_corp_history",
                table: "chars");

            migrationBuilder.AddColumn<int>(
                name: "cmc",
                table: "chars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cmc",
                table: "chars");

            migrationBuilder.AddColumn<bool>(
                name: "update_corp_history",
                table: "chars",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
