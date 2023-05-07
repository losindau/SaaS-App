using Azure.Core;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Helper;
using InventoryManagementApp.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace InventoryManagementApp.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public int TenantID { get; set; } = 0;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<MaintenanceActivity> MaintenanceActivities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Toolbox> Toolboxes { get; set; }
        public DbSet<ToolboxEquipment> ToolboxEquipment { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckStockItem> TruckStockItems { get; set; }
        public DbSet<UsageLog> UsageLogs { get; set; }
        public DbSet<DetailUsageLog> DetailUsageLogs { get; set; }
        public DbSet<RestockLog> RestockLogs { get; set; }
        public DbSet<DetailRestockLog> DetailRestockLogs { get; set; }
        public DbSet<EqDamageLog> EqDamageLogs { get; set; }
        public DbSet<DetailEqDamageLog> DetailEqDamageLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Many to many relationship
            modelBuilder.Entity<ToolboxEquipment>()
                .HasOne(t => t.Toolbox)
                .WithMany(te => te.ToolboxEquipments)
                .HasForeignKey(ti => ti.ToolboxID);

            modelBuilder.Entity<ToolboxEquipment>()
                .HasOne(eq => eq.Equipment)
                .WithMany(te => te.ToolboxEquipments)
                .HasForeignKey(eq => eq.EquipmentID);

            modelBuilder.Entity<TruckStockItem>()
                .HasOne(t => t.Truck)
                .WithMany(ts => ts.TruckStockItems)
                .HasForeignKey(t => t.TruckID);

            modelBuilder.Entity<TruckStockItem>()
                .HasOne(s => s.StockItem)
                .WithMany(ts => ts.TruckStockItems)
                .HasForeignKey(s => s.StockItemID);

            // define your filter expression tree
            Expression<Func<ITenantEntity, bool>> filterExpr = bm => bm.CompanyID == TenantID;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(ITenantEntity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
        }
    }
}
