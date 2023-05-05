using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class ToolboxEquipment : ITenantEntity
    {
        public int ToolboxEquipmentID { get; set; }
        public int? ToolboxID { get; set; }
        public Toolbox? Toolbox { get; set; }
        public int? EquipmentID { get; set; }
        public Equipment? Equipment { get; set; }
        public int QuantityInToolbox { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
