using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementAppMVC.ViewModels
{
    public class TruckVM
    {
        public int TruckID { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Display(Name = "License-Plate")]
        public string LicensePlate { get; set; }
        public AppUserVM? AppUser { get; set; }
        public int? ToolboxID { get; set; }
        public ToolboxVM? Toolbox { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<TruckStockItemVM>? TruckStockItems { get; set; }
    }
}
