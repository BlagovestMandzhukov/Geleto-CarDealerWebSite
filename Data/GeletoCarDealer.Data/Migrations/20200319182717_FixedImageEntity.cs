using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeletoCarDealer.Data.Migrations
{
    public partial class FixedImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Images",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_IsDeleted",
                table: "Images",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_IsDeleted",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Images");
        }
    }
}
