using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeletoCarDealer.Data.Migrations
{
    public partial class RemoveVehicleSelectTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_ApplicationUserId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "VehiclesModelSelects");

            migrationBuilder.DropTable(
                name: "VehicleSpecificationsSelect");

            migrationBuilder.DropTable(
                name: "VehiclesYearSelects");

            migrationBuilder.DropTable(
                name: "VehiclesMakeSelects");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ApplicationUserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Vehicles");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Vehicles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleSpecifications_Vehicles_Id",
                        column: x => x.Id,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_IsDeleted",
                table: "Vehicles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId",
                table: "Vehicles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_UserId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicles");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Vehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specifications_Vehicles_Id",
                        column: x => x.Id,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesMakeSelects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesMakeSelects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSpecificationsSelect",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSpecificationsSelect", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesModelSelects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleMakeSelectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesModelSelects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclesModelSelects_VehiclesMakeSelects_VehicleMakeSelectId",
                        column: x => x.VehicleMakeSelectId,
                        principalTable: "VehiclesMakeSelects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesYearSelects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleMakeId = table.Column<int>(type: "int", nullable: false),
                    VehicleMakeSelectId = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesYearSelects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclesYearSelects_VehiclesMakeSelects_VehicleMakeSelectId",
                        column: x => x.VehicleMakeSelectId,
                        principalTable: "VehiclesMakeSelects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ApplicationUserId",
                table: "Vehicles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesModelSelects_VehicleMakeSelectId",
                table: "VehiclesModelSelects",
                column: "VehicleMakeSelectId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesYearSelects_VehicleMakeSelectId",
                table: "VehiclesYearSelects",
                column: "VehicleMakeSelectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_ApplicationUserId",
                table: "Vehicles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
