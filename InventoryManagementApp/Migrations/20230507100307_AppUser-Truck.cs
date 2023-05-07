using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class AppUserTruck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_AspNetUsers_AppUserID",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_AppUserID",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Trucks");

            migrationBuilder.AddColumn<int>(
                name: "TruckID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TruckID",
                table: "AspNetUsers",
                column: "TruckID",
                unique: true,
                filter: "[TruckID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trucks_TruckID",
                table: "AspNetUsers",
                column: "TruckID",
                principalTable: "Trucks",
                principalColumn: "TruckID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Trucks_TruckID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TruckID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TruckID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserID",
                table: "Trucks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_AppUserID",
                table: "Trucks",
                column: "AppUserID",
                unique: true,
                filter: "[AppUserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_AspNetUsers_AppUserID",
                table: "Trucks",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
