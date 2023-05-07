using InventoryManagementAppMVC.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementAppMVC.ViewModels
{
    public class RestockLogVM
    {
        public int RestockLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public LogState LogState { get; set; }
        public DateTime RestockDate { get; set; }
        public RestockState RestockState { get; set; }
        public int? TruckID { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUserVM? AppUser { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<DetailRestockLogVM>? DetailRestockLogs { get; set; }
    }
}