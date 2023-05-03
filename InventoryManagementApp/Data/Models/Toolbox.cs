using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class Toolbox : ITenantEntity
    {
        public int ToolboxID { get; set; }
        public Truck? Truck { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }

        public ICollection<ToolboxEquipment>? ToolboxEquipments { get; set; }
        public ICollection<EqDamageLog>? EqDamageLogs { get; set; }
    }
}
