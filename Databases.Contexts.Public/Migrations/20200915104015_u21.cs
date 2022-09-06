using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_charshistory_alliance_id",
                table: "charshistory");

            migrationBuilder.DropIndex(
                name: "IX_charshistory_prev_ally_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "alliance_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "next_ally_id",
                table: "charshistory");

            migrationBuilder.DropColumn(
                name: "prev_ally_id",
                table: "charshistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "alliance_id",
                table: "charshistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "next_ally_id",
                table: "charshistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "prev_ally_id",
                table: "charshistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_alliance_id",
                table: "charshistory",
                column: "alliance_id");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_prev_ally_id",
                table: "charshistory",
                column: "prev_ally_id");
        }
    }
}
