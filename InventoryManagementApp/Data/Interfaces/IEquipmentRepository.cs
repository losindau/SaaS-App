using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IEquipmentRepository
    {
        ICollection<Equipment> GetEquipments();
        ICollection<Equipment> GetEquipmentsNoPage();
        Equipment GetEquipmnetById(int equipmentID);
        bool EquipmentExists(int equipmentID);
        bool CreateEquipment(Equipment equipment);
        bool UpdateEquipment(Equipment equipment);
        bool Save();
    }
}
