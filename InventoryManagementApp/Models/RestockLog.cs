using InventoryManagementApp.Data.Enum;

namespace InventoryManagementApp.Models
{
    public class RestockLog
    {
        public int RestockLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public LogState LogState { get; set; }
        public DateTime RestockDate { get; set; }
        public RestockState RestockState { get; set; }
        public int TruckID { get; set; }
        public Truck Truck { get; set; }
        public int StaffID { get; set; }
        public Staff Staff { get; set; }
        public ICollection<DetailRestockLog> DetailRestockLogs { get; set; }
    }
}
