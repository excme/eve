using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "have_migrations",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "have_corpMigrations",
                table: "chars");

            migrationBuilder.DropColumn(
                name: "lastUpdate_corpHistory",
                table: "chars");

            migrationBuilder.DropColumn(
                name: "last_update_affiliation",
                table: "chars");

            migrationBuilder.DropColumn(
                name: "last_update_publicInfo",
                table: "chars");

            migrationBuilder.DropColumn(
                name: "npc",
                table: "chars");

            migrationBuilder.DropColumn(
                name: "comigr",
                table: "alliances");

            migrationBuilder.AddColumn<bool>(
                name: "instat",
                table: "corpshistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "prev_ally_id",
                table: "corpshistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "alliance_id",
                table: "charshistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "instat",
                table: "charshistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "nb",
                table: "charshistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "next_ally_id",
                table: "charshistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "prev_ally_id",
                table: "charshistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "update_corp_history",
                table: "chars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "statallycorps",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alliance_id = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    count = table.Column<int>(nullable: false),
                    @in = table.Column<int>(name: "in", nullable: false),
                    @out = table.Column<int>(name: "out", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statallycorps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statallymems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alliance_id = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    count = table.Column<int>(nullable: false),
                    @in = table.Column<int>(name: "in", nullable: false),
                    @out = table.Column<int>(name: "out", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statallymems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statcorpmems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    corporation_id = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    count = table.Column<int>(nullable: false),
                    _in = table.Column<int>(nullable: false),
                    _out = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statcorpmems", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_alliance_id",
                table: "charshistory",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_corporation_id",
                table: "charshistory",
                column: "corporation_id");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_nb",
                table: "charshistory",
                column: "nb");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_prev_ally_id",
                table: "charshistory",
                column: "prev_ally_id");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_prev_corp_id",
                table: "charshistory",
                column: "prev_corp_id");

            migrationBuilder.CreateIndex(
                name: "IX_statallycorps_alliance_id",
                table: "statallycorps",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_statallycorps_date",
                table: "statallycorps",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_statallymems_alliance_id",
                table: "statallymems",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_statallymems_date",
                table: "statallymems",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_statcorpmems_corporation_id",
                table: "statcorpmems",
                column: "corporation_id");

            migrationBuilder.CreateIndex(
                name: "IX_statcorpmems_date",
                table: "statcorpmems",
                column: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "statallycorps");

            migrationBuilder.DropTable(
                name: "statallymems");

            migrationBuilder.DropTable(
                name: "statcorpmems");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_alliance_id",
                table: "charshistory");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_corporation_id",
                table: "charshistory");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_nb",
                table: "charshistory");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_prev_ally_id",
                table: "charshistory");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_prev_corp_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "instat",
                table: "corpshistory");

            migrationBuilder.DropColumn(
                name: "prev_ally_id",
                table: "corpshistory");

            migrationBuilder.DropColumn(
                name: "alliance_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "instat",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "nb",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "next_ally_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "prev_ally_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "update_corp_history",
                table: "chars");

            migrationBuilder.AddColumn<bool>(
                name: "have_migrations",
                table: "charshistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "have_corpMigrations",
                table: "chars",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdate_corpHistory",
                table: "chars",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update_affiliation",
                table: "chars",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update_publicInfo",
                table: "chars",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "npc",
                table: "chars",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<List<int>>(
                name: "comigr",
                table: "alliances",
                type: "jsonb",
                nullable: true);
        }
    }
}
