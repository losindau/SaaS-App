using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IEquipmentRepository
    {
        ICollection<Equipment> GetEquipments();
        Equipment GetEquipmnetById(int equipmentID);
        bool EquipmentExists(int equipmentID);
    }
}
