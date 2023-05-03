using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    QualityState = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentID);
                    table.ForeignKey(
                        name: "FK_Equipment_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    StockItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityState = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.StockItemID);
                    table.ForeignKey(
                        name: "FK_StockItems_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Toolboxes",
                columns: table => new
                {
                    ToolboxID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toolboxes", x => x.ToolboxID);
                    table.ForeignKey(
                        name: "FK_Toolboxes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EqDamageLogs",
                columns: table => new
                {
                    EqDamageLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogState = table.Column<int>(type: "int", nullable: false),
                    ReplaceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestockState = table.Column<int>(type: "int", nullable: false),
                    ToolboxID = table.Column<int>(type: "int", nullable: true),
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EqDamageLogs", x => x.EqDamageLogID);
                    table.ForeignKey(
                        name: "FK_EqDamageLogs_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EqDamageLogs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_EqDamageLogs_Toolboxes_ToolboxID",
                        column: x => x.ToolboxID,
                        principalTable: "Toolboxes",
                        principalColumn: "ToolboxID");
                });

            migrationBuilder.CreateTable(
                name: "ToolboxEquipment",
                columns: table => new
                {
                    ToolboxEquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolboxID = table.Column<int>(type: "int", nullable: true),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    QuantityInToolbox = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolboxEquipment", x => x.ToolboxEquipmentID);
                    table.ForeignKey(
                        name: "FK_ToolboxEquipment_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_ToolboxEquipment_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID");
                    table.ForeignKey(
                        name: "FK_ToolboxEquipment_Toolboxes_ToolboxID",
                        column: x => x.ToolboxID,
                        principalTable: "Toolboxes",
                        principalColumn: "ToolboxID");
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    TruckID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToolboxID = table.Column<int>(type: "int", nullable: true),
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.TruckID);
                    table.ForeignKey(
                        name: "FK_Trucks_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trucks_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_Trucks_Toolboxes_ToolboxID",
                        column: x => x.ToolboxID,
                        principalTable: "Toolboxes",
                        principalColumn: "ToolboxID");
                });

            migrationBuilder.CreateTable(
                name: "DetailEqDamageLogs",
                columns: table => new
                {
                    DetailEqDamageLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EqDamageLogID = table.Column<int>(type: "int", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailEqDamageLogs", x => x.DetailEqDamageLogID);
                    table.ForeignKey(
                        name: "FK_DetailEqDamageLogs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_DetailEqDamageLogs_EqDamageLogs_EqDamageLogID",
                        column: x => x.EqDamageLogID,
                        principalTable: "EqDamageLogs",
                        principalColumn: "EqDamageLogID");
                    table.ForeignKey(
                        name: "FK_DetailEqDamageLogs_Equipment_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceActivities",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TruckID = table.Column<int>(type: "int", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceActivities", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK_MaintenanceActivities_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaintenanceActivities_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_MaintenanceActivities_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "TruckID");
                });

            migrationBuilder.CreateTable(
                name: "RestockLogs",
                columns: table => new
                {
                    RestockLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogState = table.Column<int>(type: "int", nullable: false),
                    RestockDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestockState = table.Column<int>(type: "int", nullable: false),
                    TruckID = table.Column<int>(type: "int", nullable: true),
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestockLogs", x => x.RestockLogID);
                    table.ForeignKey(
                        name: "FK_RestockLogs_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RestockLogs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_RestockLogs_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "TruckID");
                });

            migrationBuilder.CreateTable(
                name: "TruckStockItems",
                columns: table => new
                {
                    TruckStockItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckID = table.Column<int>(type: "int", nullable: true),
                    StockItemID = table.Column<int>(type: "int", nullable: true),
                    QuantityInTruck = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckStockItems", x => x.TruckStockItemID);
                    table.ForeignKey(
                        name: "FK_TruckStockItems_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_TruckStockItems_StockItems_StockItemID",
                        column: x => x.StockItemID,
                        principalTable: "StockItems",
                        principalColumn: "StockItemID");
                    table.ForeignKey(
                        name: "FK_TruckStockItems_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "TruckID");
                });

            migrationBuilder.CreateTable(
                name: "UsageLogs",
                columns: table => new
                {
                    UsageLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TruckID = table.Column<int>(type: "int", nullable: true),
                    AppUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageLogs", x => x.UsageLogID);
                    table.ForeignKey(
                        name: "FK_UsageLogs_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsageLogs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_UsageLogs_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "TruckID");
                });

            migrationBuilder.CreateTable(
                name: "DetailRestockLogs",
                columns: table => new
                {
                    DetailRestockLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockItemID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RestockLogID = table.Column<int>(type: "int", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailRestockLogs", x => x.DetailRestockLogID);
                    table.ForeignKey(
                        name: "FK_DetailRestockLogs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_DetailRestockLogs_RestockLogs_RestockLogID",
                        column: x => x.RestockLogID,
                        principalTable: "RestockLogs",
                        principalColumn: "RestockLogID");
                    table.ForeignKey(
                        name: "FK_DetailRestockLogs_StockItems_StockItemID",
                        column: x => x.StockItemID,
                        principalTable: "StockItems",
                        principalColumn: "StockItemID");
                });

            migrationBuilder.CreateTable(
                name: "DetailUsageLogs",
                columns: table => new
                {
                    DetailUsageLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockItemID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UsageLogID = table.Column<int>(type: "int", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailUsageLogs", x => x.DetailUsageLogID);
                    table.ForeignKey(
                        name: "FK_DetailUsageLogs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_DetailUsageLogs_StockItems_StockItemID",
                        column: x => x.StockItemID,
                        principalTable: "StockItems",
                        principalColumn: "StockItemID");
                    table.ForeignKey(
                        name: "FK_DetailUsageLogs_UsageLogs_UsageLogID",
                        column: x => x.UsageLogID,
                        principalTable: "UsageLogs",
                        principalColumn: "UsageLogID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyID",
                table: "AspNetUsers",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DetailEqDamageLogs_CompanyID",
                table: "DetailEqDamageLogs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailEqDamageLogs_EqDamageLogID",
                table: "DetailEqDamageLogs",
                column: "EqDamageLogID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailEqDamageLogs_EquipmentID",
                table: "DetailEqDamageLogs",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailRestockLogs_CompanyID",
                table: "DetailRestockLogs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailRestockLogs_RestockLogID",
                table: "DetailRestockLogs",
                column: "RestockLogID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailRestockLogs_StockItemID",
                table: "DetailRestockLogs",
                column: "StockItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailUsageLogs_CompanyID",
                table: "DetailUsageLogs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailUsageLogs_StockItemID",
                table: "DetailUsageLogs",
                column: "StockItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailUsageLogs_UsageLogID",
                table: "DetailUsageLogs",
                column: "UsageLogID");

            migrationBuilder.CreateIndex(
                name: "IX_EqDamageLogs_AppUserID",
                table: "EqDamageLogs",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EqDamageLogs_CompanyID",
                table: "EqDamageLogs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_EqDamageLogs_ToolboxID",
                table: "EqDamageLogs",
                column: "ToolboxID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CompanyID",
                table: "Equipment",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_AppUserID",
                table: "MaintenanceActivities",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_CompanyID",
                table: "MaintenanceActivities",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_TruckID",
                table: "MaintenanceActivities",
                column: "TruckID");

            migrationBuilder.CreateIndex(
                name: "IX_RestockLogs_AppUserID",
                table: "RestockLogs",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RestockLogs_CompanyID",
                table: "RestockLogs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_RestockLogs_TruckID",
                table: "RestockLogs",
                column: "TruckID");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_CompanyID",
                table: "StockItems",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ToolboxEquipment_CompanyID",
                table: "ToolboxEquipment",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ToolboxEquipment_EquipmentID",
                table: "ToolboxEquipment",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ToolboxEquipment_ToolboxID",
                table: "ToolboxEquipment",
                column: "ToolboxID");

            migrationBuilder.CreateIndex(
                name: "IX_Toolboxes_CompanyID",
                table: "Toolboxes",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_AppUserID",
                table: "Trucks",
                column: "AppUserID",
                unique: true,
                filter: "[AppUserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_CompanyID",
                table: "Trucks",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ToolboxID",
                table: "Trucks",
                column: "ToolboxID",
                unique: true,
                filter: "[ToolboxID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TruckStockItems_CompanyID",
                table: "TruckStockItems",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_TruckStockItems_StockItemID",
                table: "TruckStockItems",
                column: "StockItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TruckStockItems_TruckID",
                table: "TruckStockItems",
                column: "TruckID");

            migrationBuilder.CreateIndex(
                name: "IX_UsageLogs_AppUserID",
                table: "UsageLogs",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UsageLogs_CompanyID",
                table: "UsageLogs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_UsageLogs_TruckID",
                table: "UsageLogs",
                column: "TruckID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DetailEqDamageLogs");

            migrationBuilder.DropTable(
                name: "DetailRestockLogs");

            migrationBuilder.DropTable(
                name: "DetailUsageLogs");

            migrationBuilder.DropTable(
                name: "MaintenanceActivities");

            migrationBuilder.DropTable(
                name: "ToolboxEquipment");

            migrationBuilder.DropTable(
                name: "TruckStockItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EqDamageLogs");

            migrationBuilder.DropTable(
                name: "RestockLogs");

            migrationBuilder.DropTable(
                name: "UsageLogs");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Toolboxes");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
