using Microsoft.EntityFrameworkCore.Migrations;

namespace GeletoCarDealer.Data.Migrations
{
    public partial class NullableVehicleIdInMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
