using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
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

        [HttpPost]
        public IActionResult CreateEquipment([FromBody] EquipmentVM equipmentCreate)
        {
            if (equipmentCreate == null)
            {
                return BadRequest(ModelState);
            }

            var equipments = _equipmentRepository.GetEquipments()
                .Where(i => i.Name.Trim().ToLower().Equals(equipmentCreate.Name.Trim().ToLower()))
                .FirstOrDefault();

            if (equipments != null)
            {
                ModelState.AddModelError("", "This item is already exists");
                return StatusCode(422, ModelState);
            }

            var equipmentMap = _mapper.Map<Equipment>(equipmentCreate);

            if (!_equipmentRepository.CreateEquipment(equipmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{equipmentID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEquipment(int equipmentID, [FromBody] EquipmentVM equipmentVM)
        {
            if (equipmentVM == null || equipmentID != equipmentVM.EquipmentID)
            {
                return BadRequest(ModelState);
            }

            if (!_equipmentRepository.EquipmentExists(equipmentID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var equipmentMap = _mapper.Map<Equipment>(equipmentVM);

            if (!_equipmentRepository.UpdateEquipment(equipmentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
