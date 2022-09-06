using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "have_migrations",
                table: "charshistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "have_corpMigrations",
                table: "chars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "have_migrations",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "have_corpMigrations",
                table: "chars");
        }
    }
}
