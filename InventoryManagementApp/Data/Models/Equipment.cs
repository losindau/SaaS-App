using InventoryManagementApp.Data.Enum;
using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class Equipment : ITenantEntity
    {
        public int EquipmentID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public EquipmentType Type { get; set; }
        public QualityState QualityState { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<ToolboxEquipment>? ToolboxEquipments { get; set; }
        public ICollection<DetailEqDamageLog>? DetailEqDamageLogs { get; set; }
    }
}
