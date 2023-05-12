using AutoMapper;
using InventoryManagementApp.Data;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{page}/equipments")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Equipment>))]
        public IActionResult GetEquipments(int page)
        {
            var equipments = _equipmentRepository.GetEquipments();

            var pageResults = 5f;
            var pageCount = Math.Ceiling(equipments.Count() / pageResults);

            var equipmentsMap = _mapper.Map<List<EquipmentVM>>(equipments.Skip((page - 1) * (int)pageResults).Take((int)pageResults));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(equipmentsMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet("{equipmentID}")]
        [Authorize(Roles = "Manager")]
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

        [Authorize(Roles = "Manager")]
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
                return StatusCode(422, "This item is already exists");
            }

            var equipmentMap = _mapper.Map<Equipment>(equipmentCreate);

            if (!_equipmentRepository.CreateEquipment(equipmentMap))
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        [HttpPut("{equipmentID}")]
        [Authorize(Roles = "Manager")]
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
                return StatusCode(500, "Something went wrong updating");
            }

            return Ok("Updated successfully");
        }
    }
}
