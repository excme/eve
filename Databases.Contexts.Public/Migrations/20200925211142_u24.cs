using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDisable",
                table: "mo");

            migrationBuilder.DropColumn(
                name: "type",
                table: "issorequests");

            migrationBuilder.AddColumn<bool>(
                name: "disabled",
                table: "mo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_mo_disabled",
                table: "mo",
                column: "disabled");

            migrationBuilder.CreateIndex(
                name: "IX_mo_region_id",
                table: "mo",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_start_date",
                table: "charshistory",
                column: "start_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_mo_disabled",
                table: "mo");

            migrationBuilder.DropIndex(
                name: "IX_mo_region_id",
                table: "mo");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_start_date",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "disabled",
                table: "mo");

            migrationBuilder.AddColumn<bool>(
                name: "isDisable",
                table: "mo",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "type",
                table: "issorequests",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
