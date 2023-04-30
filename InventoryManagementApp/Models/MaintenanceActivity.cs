using InventoryManagementApp.Data.Enum;

namespace InventoryManagementApp.Models
{
    public class MaintenanceActivity
    {
        public int ActivityID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public ActivityState State { get; set; }
        public int StaffID { get; set; }
        public Staff Staff { get; set; }
        public int TruckID { get; set; }
        public Truck Truck { get; set; }
    }
}
