using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastUpdate_allianceHistory",
                table: "corps");

            migrationBuilder.DropColumn(
                name: "last_update_publicInfo",
                table: "corps");

            migrationBuilder.DropColumn(
                name: "last_info_updated",
                table: "alliances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdate_allianceHistory",
                table: "corps",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update_publicInfo",
                table: "corps",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_info_updated",
                table: "alliances",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
