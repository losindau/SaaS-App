using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "UsageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TruckStockItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Trucks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Toolboxes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "ToolboxEquipment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "StockItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "RestockLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "MaintenanceActivities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Equipment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "EqDamageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "DetailUsageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "DetailRestockLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "DetailEqDamageLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "UsageLogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TruckStockItems");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Toolboxes");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "ToolboxEquipment");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "RestockLogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "MaintenanceActivities");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "EqDamageLogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "DetailUsageLogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "DetailRestockLogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "DetailEqDamageLogs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Companies");
        }
    }
}
