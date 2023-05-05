using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class EqDamageLog : ITenantEntity
    {
        public int EqDamageLogID { get; set; }
        public DateTime ReportDate { get; set; }
        public LogState LogState { get; set; }
        public DateTime ReplaceDate { get; set; }
        public RestockState RestockState { get; set; }
        public int? ToolboxID { get; set; }
        public Toolbox? Toolbox { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<DetailEqDamageLog>? DetailEqDamageLogs { get; set; }
    }
}
