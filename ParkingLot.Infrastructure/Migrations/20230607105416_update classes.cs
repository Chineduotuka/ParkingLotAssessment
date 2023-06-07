using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLot.Infrastructure.Migrations
{
    public partial class updateclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "spot",
                table: "vehicles",
                newName: "Spot");

            migrationBuilder.RenameColumn(
                name: "isParked",
                table: "vehicles",
                newName: "IsParked");

            migrationBuilder.AddColumn<string>(
                name: "EntryDate",
                table: "vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "Spot",
                table: "vehicles",
                newName: "spot");

            migrationBuilder.RenameColumn(
                name: "IsParked",
                table: "vehicles",
                newName: "isParked");
        }
    }
}
