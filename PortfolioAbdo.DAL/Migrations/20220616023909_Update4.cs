using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioAbdo.DAL.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project_Photo",
                table: "Portfolio");

            migrationBuilder.AddColumn<string>(
                name: "Project_Photo_Name",
                table: "Portfolio",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project_Photo_Name",
                table: "Portfolio");

            migrationBuilder.AddColumn<byte[]>(
                name: "Project_Photo",
                table: "Portfolio",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
