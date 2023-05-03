using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;

        public EquipmentController(IEquipmentRepository equipmentRepository, IMapper mapper)
        {
            this._equipmentRepository = equipmentRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Equipment>))]
        public IActionResult GetEquipments()
        {
            var equipments = _mapper.Map<List<EquipmentVM>>(_equipmentRepository.GetEquipments());
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(equipments);
        }

        [HttpGet("{equipmentID}")]
        [ProducesResponseType(200,Type = typeof(Equipment))]
        [ProducesResponseType(400)]
        public IActionResult GetEquipment(int equipmentID) 
        {
            if (!_equipmentRepository.EquipmentExists(equipmentID))
            {
                return NotFound();
            }

            var equipment = _mapper.Map<EquipmentVM>(_equipmentRepository.GetEquipmnetById(equipmentID));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(equipment);
        }
    }
}
