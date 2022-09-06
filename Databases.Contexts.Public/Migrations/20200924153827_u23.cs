using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "issorequests");

            migrationBuilder.CreateIndex(
                name: "IX_charshistory_start_date",
                table: "charshistory",
                column: "start_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_charshistory_start_date",
                table: "charshistory");

            migrationBuilder.AddColumn<byte>(
                name: "type",
                table: "issorequests",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
