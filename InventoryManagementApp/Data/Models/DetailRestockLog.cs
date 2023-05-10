using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class DetailRestockLog : ITenantEntity
    {
        public int DetailRestockLogID { get; set; }
        public int? StockItemID { get; set; }
        public string? StockItemName { get; set; }
        public StockItem? StockItem { get; set; }
        public int Quantity { get; set; }
        public int? RestockLogID { get; set; }
        public RestockLog? RestockLog { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
