using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeletoCarDealer.Data.Migrations
{
    public partial class UpdatedSomeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
