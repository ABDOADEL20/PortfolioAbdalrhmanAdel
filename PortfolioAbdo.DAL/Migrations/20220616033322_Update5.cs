using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioAbdo.DAL.Migrations
{
    public partial class Update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project_Service",
                table: "Portfolio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Project_Service",
                table: "Portfolio",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
