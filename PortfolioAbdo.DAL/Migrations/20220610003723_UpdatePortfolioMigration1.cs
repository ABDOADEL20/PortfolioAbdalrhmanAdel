using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioAbdo.DAL.Migrations
{
    public partial class UpdatePortfolioMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Id_CategoryProtoflio",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Testimonials_AspNetUsers_applicationUserId",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "Project_PhotoUrl",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "CvUrl",
                table: "Home");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "Testimonials",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Testimonials_applicationUserId",
                table: "Testimonials",
                newName: "IX_Testimonials_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "Id_CategoryProtoflio",
                table: "Portfolio",
                newName: "Category_PortoflioId");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolio_Id_CategoryProtoflio",
                table: "Portfolio",
                newName: "IX_Portfolio_Category_PortoflioId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Testimonials",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category_ProtoflioId",
                table: "Portfolio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Project_Photo",
                table: "Portfolio",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Cv",
                table: "Home",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Category_PortoflioId",
                table: "Portfolio",
                column: "Category_PortoflioId",
                principalTable: "Category_Portoflio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonials_AspNetUsers_ApplicationUserId",
                table: "Testimonials",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Category_PortoflioId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Testimonials_AspNetUsers_ApplicationUserId",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "Category_ProtoflioId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "Project_Photo",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "Cv",
                table: "Home");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Testimonials",
                newName: "applicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Testimonials_ApplicationUserId",
                table: "Testimonials",
                newName: "IX_Testimonials_applicationUserId");

            migrationBuilder.RenameColumn(
                name: "Category_PortoflioId",
                table: "Portfolio",
                newName: "Id_CategoryProtoflio");

            migrationBuilder.RenameIndex(
                name: "IX_Portfolio_Category_PortoflioId",
                table: "Portfolio",
                newName: "IX_Portfolio_Id_CategoryProtoflio");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Testimonials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Project_PhotoUrl",
                table: "Portfolio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CvUrl",
                table: "Home",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_Category_Portoflio_Id_CategoryProtoflio",
                table: "Portfolio",
                column: "Id_CategoryProtoflio",
                principalTable: "Category_Portoflio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonials_AspNetUsers_applicationUserId",
                table: "Testimonials",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
