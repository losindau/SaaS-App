using InventoryManagementApp.Data.Enum;

namespace InventoryManagementApp.Models
{
    public class EqDamageLog
    {
        public int EqDamageLogID { get; set; }
        public DateTime ReportDate { get; set; }
        public LogState LogState { get; set; }
        public DateTime ReplaceDate { get; set; }
        public RestockState RestockState { get; set; }
        public int ToolboxID { get; set; }
        public Toolbox Toolbox { get; set; }
        public int StaffID { get; set; }
        public Staff Staff { get; set; }
        public ICollection <DetailEqDamageLog> DetailEqDamageLogs { get; set; }
    }
}
