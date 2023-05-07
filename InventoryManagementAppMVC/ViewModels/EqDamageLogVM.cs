﻿using InventoryManagementAppMVC.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementAppMVC.ViewModels
{
    public class EqDamageLogVM
    {
        public int EqDamageLogID { get; set; }
        public DateTime ReportDate { get; set; }
        public LogState LogState { get; set; }
        public DateTime ReplaceDate { get; set; }
        public RestockState RestockState { get; set; }
        public int? ToolboxID { get; set; }
        //public ToolboxVM? Toolbox { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        //public AppUserVM? AppUser { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<DetailEqDamageLogVM>? DetailEqDamageLogs { get; set; }
    }
}
