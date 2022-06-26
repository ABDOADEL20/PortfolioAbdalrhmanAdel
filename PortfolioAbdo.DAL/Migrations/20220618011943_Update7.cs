using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioAbdo.DAL.Migrations
{
    public partial class Update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ExpCompaniesPhoto");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ExpCompaniesPhoto",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ExpCompaniesPhoto");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ExpCompaniesPhoto",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
