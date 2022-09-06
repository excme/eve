using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "npc",
                table: "chars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "npc",
                table: "chars");
        }
    }
}
