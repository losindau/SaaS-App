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
    public class ToolboxController : Controller
    {
        private readonly IToolboxRepository _toolboxRepository;
        private readonly IMapper _mapper;

        public ToolboxController(IToolboxRepository toolboxRepository,IMapper mapper)
        {
            this._toolboxRepository = toolboxRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Toolbox>))]
        public IActionResult GetToolBoxes()
        {
            var toolboxes = _mapper.Map<List<ToolboxVM>>(_toolboxRepository.GetToolboxes());

            foreach (ToolboxVM us in toolboxes)
            {
                us.ToolboxEquipments = _mapper.Map<List<ToolboxEquipmentVM>>(_toolboxRepository.GetToolboxEquipments(us.ToolboxID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(toolboxes);
        }

        [HttpGet("{toolboxID}")]
        [ProducesResponseType(200, Type = typeof(Toolbox))]
        [ProducesResponseType(400)]
        public IActionResult GetToolbox(int toolboxID)
        {
            if (!_toolboxRepository.ToolboxExists(toolboxID))
            {
                return NotFound();
            }

            var toolbox = _mapper.Map<ToolboxVM>(_toolboxRepository.GetToolboxById(toolboxID));
            toolbox.ToolboxEquipments = _mapper.Map<List<ToolboxEquipmentVM>>(_toolboxRepository.GetToolboxEquipments(toolboxID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(toolbox);
        }

        [HttpPost]
        public IActionResult CreateToolbox(ToolboxVM toolboxCreate)
        {
            if (toolboxCreate == null)
            {
                return BadRequest(ModelState);
            }

            var toolboxMap = _mapper.Map<Toolbox>(toolboxCreate);

            if (!_toolboxRepository.CreateToolbox(toolboxMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(toolboxMap.ToolboxID); ;
        }

        [HttpPut("{toolboxID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateToolbox(int toolboxID, [FromBody] ToolboxVM toolboxVM)
        {
            if (toolboxVM == null || toolboxID != toolboxVM.ToolboxID)
            {
                return BadRequest(ModelState);
            }

            if (!_toolboxRepository.ToolboxExists(toolboxID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var toolboxMap = _mapper.Map<Toolbox>(toolboxVM);

            if (!_toolboxRepository.UpdateToolbox(toolboxMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
