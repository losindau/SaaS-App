using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class TruckStockItem : ITenantEntity
    {
        public int TruckStockItemID { get; set; }
        public int? TruckID { get; set; }
        public Truck? Truck { get; set; }
        public int? StockItemID { get; set; }
        public StockItem? StockItem { get; set; }
        public int QuantityInTruck { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
