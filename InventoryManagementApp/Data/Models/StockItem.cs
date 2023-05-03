using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class StockItem : ITenantEntity
    {
        public int StockItemID { get; set; }
        public string Name { get; set; }
        public StockItemType Type { get; set; }
        public int Quantity { get; set; }
        public QuantityState QuantityState { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }

        public ICollection<TruckStockItem>? TruckStockItems { get; set; }
        public ICollection<DetailRestockLog>? DetailRestockLogs { get; set; }
        public ICollection<DetailUsageLog>? DetailUsageLogs { get; set; }
    }
}
