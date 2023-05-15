using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IToolboxEquipmentRepository
    {
        ToolboxEquipment GetToolboxEquipmentById(int toolboxEquipmentID);
        ToolboxEquipment GetToolboxEquipmentByEqId(int equipmentID);
        bool ToolboxEquipmentExists(int toolboxEquipmentID);
        bool CreateToolboxEquipments(List<ToolboxEquipment> toolboxEquipment);
        bool UpdateToolboxEquipment(ToolboxEquipment toolboxEquipment);
        bool Save();
    }
}
