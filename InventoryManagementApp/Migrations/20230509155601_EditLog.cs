using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class EditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserName",
                table: "UsageLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "UsageLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserName",
                table: "RestockLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "RestockLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserName",
                table: "EqDamageLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockItemName",
                table: "DetailUsageLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockItemName",
                table: "DetailRestockLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EquipmentName",
                table: "DetailEqDamageLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserName",
                table: "UsageLogs");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "UsageLogs");

            migrationBuilder.DropColumn(
                name: "AppUserName",
                table: "RestockLogs");

            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "RestockLogs");

            migrationBuilder.DropColumn(
                name: "AppUserName",
                table: "EqDamageLogs");

            migrationBuilder.DropColumn(
                name: "StockItemName",
                table: "DetailUsageLogs");

            migrationBuilder.DropColumn(
                name: "StockItemName",
                table: "DetailRestockLogs");

            migrationBuilder.DropColumn(
                name: "EquipmentName",
                table: "DetailEqDamageLogs");
        }
    }
}
