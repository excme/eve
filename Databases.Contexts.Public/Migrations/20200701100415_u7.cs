using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_founded",
                table: "corps",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthday",
                table: "chars",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_founded",
                table: "alliances",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date_founded",
                table: "corps",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthday",
                table: "chars",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_founded",
                table: "alliances",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
