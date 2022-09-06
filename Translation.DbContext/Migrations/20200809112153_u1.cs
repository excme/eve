using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Translation.WebApplication.Migrations
{
    public partial class u1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TranslationVersions",
                columns: table => new
                {
                    lang = table.Column<string>(nullable: false),
                    version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationVersions", x => x.lang);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationVersions");
        }
    }
}
