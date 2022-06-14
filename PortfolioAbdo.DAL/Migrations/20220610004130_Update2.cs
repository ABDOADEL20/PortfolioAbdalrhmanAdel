using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioAbdo.DAL.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo_Comp_ExpUrl",
                table: "Home");

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo_Comp_Exp",
                table: "Home",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo_Comp_Exp",
                table: "Home");

            migrationBuilder.AddColumn<string>(
                name: "Logo_Comp_ExpUrl",
                table: "Home",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
