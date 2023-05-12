﻿// <auto-generated />
using System;
using InventoryManagementApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagementApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryManagementApp.Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TruckID")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.DetailEqDamageLog", b =>
                {
                    b.Property<int>("DetailEqDamageLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailEqDamageLogID"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("EqDamageLogID")
                        .HasColumnType("int");

                    b.Property<int?>("EquipmentID")
                        .HasColumnType("int");

                    b.Property<string>("EquipmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("DetailEqDamageLogID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("EqDamageLogID");

                    b.HasIndex("EquipmentID");

                    b.ToTable("DetailEqDamageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.DetailRestockLog", b =>
                {
                    b.Property<int>("DetailRestockLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailRestockLogID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("RestockLogID")
                        .HasColumnType("int");

                    b.Property<int?>("StockItemID")
                        .HasColumnType("int");

                    b.Property<string>("StockItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("DetailRestockLogID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("RestockLogID");

                    b.HasIndex("StockItemID");

                    b.ToTable("DetailRestockLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.DetailUsageLog", b =>
                {
                    b.Property<int>("DetailUsageLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailUsageLogID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("StockItemID")
                        .HasColumnType("int");

                    b.Property<string>("StockItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsageLogID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("DetailUsageLogID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("StockItemID");

                    b.HasIndex("UsageLogID");

                    b.ToTable("DetailUsageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.EqDamageLog", b =>
                {
                    b.Property<int>("EqDamageLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EqDamageLogID"));

                    b.Property<string>("AppUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("LogState")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReplaceDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestockState")
                        .HasColumnType("int");

                    b.Property<int?>("ToolboxID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("EqDamageLogID");

                    b.HasIndex("AppUserID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("ToolboxID");

                    b.ToTable("EqDamageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Equipment", b =>
                {
                    b.Property<int>("EquipmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EquipmentID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QualityState")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("EquipmentID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.RestockLog", b =>
                {
                    b.Property<int>("RestockLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestockLogID"));

                    b.Property<string>("AppUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LogState")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RestockDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestockState")
                        .HasColumnType("int");

                    b.Property<int?>("TruckID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("RestockLogID");

                    b.HasIndex("AppUserID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("TruckID");

                    b.ToTable("RestockLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.StockItem", b =>
                {
                    b.Property<int>("StockItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockItemID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("QuantityState")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("StockItemID");

                    b.HasIndex("CompanyID");

                    b.ToTable("StockItems");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Toolbox", b =>
                {
                    b.Property<int>("ToolboxID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ToolboxID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ToolboxID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Toolboxes");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.ToolboxEquipment", b =>
                {
                    b.Property<int>("ToolboxEquipmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ToolboxEquipmentID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("EquipmentID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInToolbox")
                        .HasColumnType("int");

                    b.Property<int?>("ToolboxID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ToolboxEquipmentID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("EquipmentID");

                    b.HasIndex("ToolboxID");

                    b.ToTable("ToolboxEquipment");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Truck", b =>
                {
                    b.Property<int>("TruckID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TruckID"));

                    b.Property<string>("AppUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ToolboxID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("TruckID");

                    b.HasIndex("AppUserID")
                        .IsUnique()
                        .HasFilter("[AppUserID] IS NOT NULL");

                    b.HasIndex("CompanyID");

                    b.HasIndex("ToolboxID")
                        .IsUnique()
                        .HasFilter("[ToolboxID] IS NOT NULL");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.TruckStockItem", b =>
                {
                    b.Property<int>("TruckStockItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TruckStockItemID"));

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInTruck")
                        .HasColumnType("int");

                    b.Property<int?>("StockItemID")
                        .HasColumnType("int");

                    b.Property<int?>("TruckID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("TruckStockItemID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("StockItemID");

                    b.HasIndex("TruckID");

                    b.ToTable("TruckStockItems");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.UsageLog", b =>
                {
                    b.Property<int>("UsageLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsageLogID"));

                    b.Property<string>("AppUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TruckID")
                        .HasColumnType("int");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("UsageLogID");

                    b.HasIndex("AppUserID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("TruckID");

                    b.ToTable("UsageLogs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.AppUser", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("AppUsers")
                        .HasForeignKey("CompanyID");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.DetailEqDamageLog", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("DetailEqDamageLogs")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.EqDamageLog", "EqDamageLog")
                        .WithMany("DetailEqDamageLogs")
                        .HasForeignKey("EqDamageLogID");

                    b.HasOne("InventoryManagementApp.Data.Models.Equipment", "Equipment")
                        .WithMany("DetailEqDamageLogs")
                        .HasForeignKey("EquipmentID");

                    b.Navigation("Company");

                    b.Navigation("EqDamageLog");

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.DetailRestockLog", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("DetailRestockLogs")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.RestockLog", "RestockLog")
                        .WithMany("DetailRestockLogs")
                        .HasForeignKey("RestockLogID");

                    b.HasOne("InventoryManagementApp.Data.Models.StockItem", "StockItem")
                        .WithMany("DetailRestockLogs")
                        .HasForeignKey("StockItemID");

                    b.Navigation("Company");

                    b.Navigation("RestockLog");

                    b.Navigation("StockItem");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.DetailUsageLog", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("DetailUsageLogs")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.StockItem", "StockItem")
                        .WithMany("DetailUsageLogs")
                        .HasForeignKey("StockItemID");

                    b.HasOne("InventoryManagementApp.Data.Models.UsageLog", "UsageLog")
                        .WithMany("DetailUsageLogs")
                        .HasForeignKey("UsageLogID");

                    b.Navigation("Company");

                    b.Navigation("StockItem");

                    b.Navigation("UsageLog");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.EqDamageLog", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", "AppUser")
                        .WithMany("EqDamageLogs")
                        .HasForeignKey("AppUserID");

                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("EqDamageLogs")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.Toolbox", "Toolbox")
                        .WithMany("EqDamageLogs")
                        .HasForeignKey("ToolboxID");

                    b.Navigation("AppUser");

                    b.Navigation("Company");

                    b.Navigation("Toolbox");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Equipment", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("Equipment")
                        .HasForeignKey("CompanyID");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.RestockLog", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", "AppUser")
                        .WithMany("RestockLogs")
                        .HasForeignKey("AppUserID");

                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("RestockLogs")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.Truck", "Truck")
                        .WithMany("RestockLogs")
                        .HasForeignKey("TruckID");

                    b.Navigation("AppUser");

                    b.Navigation("Company");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.StockItem", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("StockItems")
                        .HasForeignKey("CompanyID");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Toolbox", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("Toolboxes")
                        .HasForeignKey("CompanyID");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.ToolboxEquipment", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("ToolboxEquipment")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.Equipment", "Equipment")
                        .WithMany("ToolboxEquipments")
                        .HasForeignKey("EquipmentID");

                    b.HasOne("InventoryManagementApp.Data.Models.Toolbox", "Toolbox")
                        .WithMany("ToolboxEquipments")
                        .HasForeignKey("ToolboxID");

                    b.Navigation("Company");

                    b.Navigation("Equipment");

                    b.Navigation("Toolbox");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Truck", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", "AppUser")
                        .WithOne("Truck")
                        .HasForeignKey("InventoryManagementApp.Data.Models.Truck", "AppUserID");

                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("Trucks")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.Toolbox", "Toolbox")
                        .WithOne("Truck")
                        .HasForeignKey("InventoryManagementApp.Data.Models.Truck", "ToolboxID");

                    b.Navigation("AppUser");

                    b.Navigation("Company");

                    b.Navigation("Toolbox");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.TruckStockItem", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("TruckStockItems")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.StockItem", "StockItem")
                        .WithMany("TruckStockItems")
                        .HasForeignKey("StockItemID");

                    b.HasOne("InventoryManagementApp.Data.Models.Truck", "Truck")
                        .WithMany("TruckStockItems")
                        .HasForeignKey("TruckID");

                    b.Navigation("Company");

                    b.Navigation("StockItem");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.UsageLog", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", "AppUser")
                        .WithMany("UsageLogs")
                        .HasForeignKey("AppUserID");

                    b.HasOne("InventoryManagementApp.Data.Models.Company", "Company")
                        .WithMany("UsageLogs")
                        .HasForeignKey("CompanyID");

                    b.HasOne("InventoryManagementApp.Data.Models.Truck", "Truck")
                        .WithMany("UsageLogs")
                        .HasForeignKey("TruckID");

                    b.Navigation("AppUser");

                    b.Navigation("Company");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InventoryManagementApp.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.AppUser", b =>
                {
                    b.Navigation("EqDamageLogs");

                    b.Navigation("RestockLogs");

                    b.Navigation("Truck");

                    b.Navigation("UsageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Company", b =>
                {
                    b.Navigation("AppUsers");

                    b.Navigation("DetailEqDamageLogs");

                    b.Navigation("DetailRestockLogs");

                    b.Navigation("DetailUsageLogs");

                    b.Navigation("EqDamageLogs");

                    b.Navigation("Equipment");

                    b.Navigation("RestockLogs");

                    b.Navigation("StockItems");

                    b.Navigation("ToolboxEquipment");

                    b.Navigation("Toolboxes");

                    b.Navigation("TruckStockItems");

                    b.Navigation("Trucks");

                    b.Navigation("UsageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.EqDamageLog", b =>
                {
                    b.Navigation("DetailEqDamageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Equipment", b =>
                {
                    b.Navigation("DetailEqDamageLogs");

                    b.Navigation("ToolboxEquipments");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.RestockLog", b =>
                {
                    b.Navigation("DetailRestockLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.StockItem", b =>
                {
                    b.Navigation("DetailRestockLogs");

                    b.Navigation("DetailUsageLogs");

                    b.Navigation("TruckStockItems");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Toolbox", b =>
                {
                    b.Navigation("EqDamageLogs");

                    b.Navigation("ToolboxEquipments");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.Truck", b =>
                {
                    b.Navigation("RestockLogs");

                    b.Navigation("TruckStockItems");

                    b.Navigation("UsageLogs");
                });

            modelBuilder.Entity("InventoryManagementApp.Data.Models.UsageLog", b =>
                {
                    b.Navigation("DetailUsageLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
