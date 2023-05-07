﻿using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementAppMVC.ViewModels
{
    public class UsageLogVM
    {
        public int UsageLogID { get; set; }
        public DateTime Date { get; set; }
        public int? TruckID { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<DetailUsageLogVM>? DetailUsageLogs { get; set; }
    }
}
