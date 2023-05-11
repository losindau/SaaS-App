using InventoryManagementAppMVC.Enum;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAppMVC.ViewModels
{
    public class EquipmentVM
    {
        public int EquipmentID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public EquipmentType Type { get; set; }
        public QualityState QualityState { get; set; }
        public int? CompanyID { get; set; }
        public bool isDeleted { get; set; }
    }
}
