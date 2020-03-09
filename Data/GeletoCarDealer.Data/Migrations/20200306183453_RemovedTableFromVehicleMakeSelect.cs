using Microsoft.EntityFrameworkCore.Migrations;

namespace GeletoCarDealer.Data.Migrations
{
    public partial class RemovedTableFromVehicleMakeSelect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MakeId",
                table: "VehiclesModelSelects");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleMakeSelectId",
                table: "VehiclesModelSelects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VehicleMakeSelectId",
                table: "VehiclesModelSelects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MakeId",
                table: "VehiclesModelSelects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
