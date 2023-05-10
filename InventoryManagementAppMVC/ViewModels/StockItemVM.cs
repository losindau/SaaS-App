using InventoryManagementAppMVC.Enum;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAppMVC.ViewModels
{
    public class StockItemVM
    {
        public int StockItemID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public StockItemType Type { get; set; }
        [Required]
        public int Quantity { get; set; }
        public QuantityState QuantityState { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
