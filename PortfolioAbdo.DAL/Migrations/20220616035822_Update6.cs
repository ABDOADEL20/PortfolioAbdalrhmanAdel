using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioAbdo.DAL.Migrations
{
    public partial class Update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Category_PortoflioId",
                table: "Portfolio");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_Category_PortoflioId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "Category_PortoflioId",
                table: "Portfolio");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_Category_ProtoflioId",
                table: "Portfolio",
                column: "Category_ProtoflioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Category_ProtoflioId",
                table: "Portfolio",
                column: "Category_ProtoflioId",
                principalTable: "Category_Portoflio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Category_ProtoflioId",
                table: "Portfolio");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_Category_ProtoflioId",
                table: "Portfolio");

            migrationBuilder.AddColumn<int>(
                name: "Category_PortoflioId",
                table: "Portfolio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_Category_PortoflioId",
                table: "Portfolio",
                column: "Category_PortoflioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Category_PortoflioId",
                table: "Portfolio",
                column: "Category_PortoflioId",
                principalTable: "Category_Portoflio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
