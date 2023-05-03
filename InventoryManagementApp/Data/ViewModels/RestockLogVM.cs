﻿using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.ViewModels
{
    public class RestockLogVM
    {
        public int RestockLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public LogState LogState { get; set; }
        public DateTime RestockDate { get; set; }
        public RestockState RestockState { get; set; }
        public int? TruckID { get; set; }
        public Truck? Truck { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        public int? CompanyID { get; set; }

        public ICollection<DetailRestockLogVM>? DetailRestockLogs { get; set; }
    }
}