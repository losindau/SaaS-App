﻿using InventoryManagementApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.ViewModels
{
    public class TruckVM
    {
        public int TruckID { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int? ToolboxID { get; set; }
        public Toolbox? Toolbox { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        public int? CompanyID { get; set; }

        public ICollection<TruckStockItemVM>? TruckStockItems { get; set; }
    }
}
