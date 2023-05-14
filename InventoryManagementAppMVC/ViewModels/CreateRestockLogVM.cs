namespace InventoryManagementAppMVC.ViewModels
{
    public class CreateRestockLogVM
    {
        public int? TruckID { get; set; }
        public string LicensePlate { get; set; }
        public List<TruckStockItemVM> TruckStockItems { get; set; }
        public Dictionary<int, int> StockItemQuantities { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, string> StockItemNames { get; set; } = new Dictionary<int, string>();
    }
}
