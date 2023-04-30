namespace InventoryManagementApp.Models
{
    public class UsageLog
    {
        public int UsageLogID { get; set; }
        public DateTime Date { get; set; }
        public int TruckID { get; set; }
        public Truck Truck { get; set; }
        public int StaffID { get; set; }
        public Staff Staff { get; set; }
        public ICollection<DetailUsageLog> DetailUsageLogs { get; set; }
    }
}
