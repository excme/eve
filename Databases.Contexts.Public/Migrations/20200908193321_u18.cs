using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "last_actions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    auth = table.Column<bool>(nullable: false),
                    owner_id = table.Column<int>(nullable: false),
                    parent_id = table.Column<int>(nullable: false),
                    type = table.Column<byte>(nullable: false),
                    ref_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_last_actions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_last_actions_owner_id",
                table: "last_actions",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_last_actions_parent_id",
                table: "last_actions",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_last_actions_type",
                table: "last_actions",
                column: "type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "last_actions");
        }
    }
}
