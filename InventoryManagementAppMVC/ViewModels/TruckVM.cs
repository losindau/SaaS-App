﻿using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementAppMVC.ViewModels
{
    public class TruckVM
    {
        public int TruckID { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int? ToolboxID { get; set; }
        public ToolboxVM? Toolbox { get; set; }
        [ForeignKey("AppUser")]
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<TruckStockItemVM>? TruckStockItems { get; set; }
    }
}