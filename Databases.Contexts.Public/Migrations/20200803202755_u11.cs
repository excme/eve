using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ed_checkpoints",
                table: "ed_checkpoints");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ed_checkpoints");

            migrationBuilder.AlterColumn<string>(
                name: "checkpointName",
                table: "ed_checkpoints",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ed_checkpoints",
                table: "ed_checkpoints",
                column: "checkpointName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ed_checkpoints",
                table: "ed_checkpoints");

            migrationBuilder.AlterColumn<string>(
                name: "checkpointName",
                table: "ed_checkpoints",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "ed_checkpoints",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ed_checkpoints",
                table: "ed_checkpoints",
                column: "id");
        }
    }
}
