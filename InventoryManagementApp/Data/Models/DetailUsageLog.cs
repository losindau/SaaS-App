using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class DetailUsageLog : ITenantEntity
    {
        public int DetailUsageLogID { get; set; }
        public int? StockItemID { get; set; }
        public StockItem? StockItem { get; set; }
        public int Quantity { get; set; }
        public int? UsageLogID { get; set; }
        public UsageLog? UsageLog { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
    }
}
