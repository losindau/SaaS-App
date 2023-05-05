using InventoryManagementApp.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementApp.Data.Models
{
    public class DetailEqDamageLog : ITenantEntity
    {
        public int DetailEqDamageLogID { get; set; }
        public int? EquipmentID { get; set; }
        public Equipment? Equipment { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public int? EqDamageLogID { get; set; }
        public EqDamageLog? EqDamageLog { get; set; }
        public int? CompanyID { get; set; }
        public Company? Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
