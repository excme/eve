using Microsoft.EntityFrameworkCore.Migrations;

namespace eveDirect.Databases.Contexts.Migrations
{
    public partial class u3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_contrsbids_contrs_contract_id",
                table: "contrsbids",
                column: "contract_id",
                principalTable: "contrs",
                principalColumn: "contract_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contrsitems_contrs_contract_id",
                table: "contrsitems",
                column: "contract_id",
                principalTable: "contrs",
                principalColumn: "contract_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contrsbids_contrs_contract_id",
                table: "contrsbids");

            migrationBuilder.DropForeignKey(
                name: "FK_contrsitems_contrs_contract_id",
                table: "contrsitems");
        }
    }
}
