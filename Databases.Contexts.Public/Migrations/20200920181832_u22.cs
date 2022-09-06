using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastUpdate_allianceHistory",
                table: "corps",
                newName: "al_hi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "al_hi",
                table: "corps",
                newName: "lastUpdate_allianceHistory");
        }
    }
}
