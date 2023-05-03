using InventoryManagementApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace InventoryManagementApp.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
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
        }
    }
}
