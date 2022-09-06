using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdate_allianceHistory",
                table: "corps",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "allyComplete",
                table: "charshistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastupdate_corphistory",
                table: "chars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "charsallyhist",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    character_id = table.Column<int>(nullable: false),
                    alliance_id = table.Column<int>(nullable: false),
                    allyHistory_recordId = table.Column<int>(nullable: false),
                    corpHistory_recordId = table.Column<int>(nullable: false),
                    start = table.Column<DateTime>(nullable: false),
                    end = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_charsallyhist", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_charsallyhist_alliance_id",
                table: "charsallyhist",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_charsallyhist_character_id",
                table: "charsallyhist",
                column: "character_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "charsallyhist");

            migrationBuilder.DropColumn(
                name: "lastUpdate_allianceHistory",
                table: "corps");

            migrationBuilder.DropColumn(
                name: "allyComplete",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "lastupdate_corphistory",
                table: "chars");
        }
    }
}
