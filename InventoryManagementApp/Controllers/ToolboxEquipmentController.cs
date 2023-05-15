using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolboxEquipmentController : Controller
    {
        private readonly IToolboxEquipmentRepository _toolboxRepository;
        private readonly IMapper _mapper;

        public ToolboxEquipmentController(IToolboxEquipmentRepository toolboxEquipmentRepository, IMapper mapper)
        {
            this._toolboxRepository = toolboxEquipmentRepository;
            this._mapper = mapper;
        }

        [HttpGet("{toolboxEquipmentID}")]
        [ProducesResponseType(200, Type = typeof(ToolboxEquipment))]
        [ProducesResponseType(400)]
        public IActionResult GetToolboxEquipmentById(int toolboxEquipmentID)
        {
            if (!_toolboxRepository.ToolboxEquipmentExists(toolboxEquipmentID))
            {
                return NotFound();
            }

            var toolbox = _mapper.Map<ToolboxEquipmentVM>(_toolboxRepository.GetToolboxEquipmentById(toolboxEquipmentID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(toolbox);
        }

        [HttpGet("{equipmentID}/equipmentid")]
        [ProducesResponseType(200, Type = typeof(ToolboxEquipment))]
        [ProducesResponseType(400)]
        public IActionResult GetToolboxEquipmentByEqId(int equipmentID)
        {
            var toolboxEquipment = _mapper.Map<ToolboxEquipmentVM>(_toolboxRepository.GetToolboxEquipmentByEqId(equipmentID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(toolboxEquipment);
        }

        [HttpPost]
        public IActionResult CreateToolboxEquipment(List<ToolboxEquipmentVM> toolboxCreate)
        {
            if (toolboxCreate == null)
            {
                return BadRequest(ModelState);
            }

            var toolboxMap = _mapper.Map<List<ToolboxEquipment>>(toolboxCreate);

            if (!_toolboxRepository.CreateToolboxEquipments(toolboxMap))
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        [HttpPut("{toolboxequipmentID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateToolboxEquipment(int toolboxequipmentID, [FromBody] ToolboxEquipmentVM toolboxEqVM)
        {
            if (toolboxEqVM == null || toolboxequipmentID != toolboxEqVM.ToolboxEquipmentID)
            {
                return BadRequest(ModelState);
            }

            if (!_toolboxRepository.ToolboxEquipmentExists(toolboxequipmentID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var toolboxEqMap = _mapper.Map<ToolboxEquipment>(toolboxEqVM);

            if (!_toolboxRepository.UpdateToolboxEquipment(toolboxEqMap))
            {
                return StatusCode(500, "Something went wrong updating");
            }

            return Ok("Updated successfully");
        }
    }
}
