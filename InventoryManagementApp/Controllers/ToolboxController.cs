using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
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
    }
}
