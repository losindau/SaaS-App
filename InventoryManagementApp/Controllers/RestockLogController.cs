using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestockLogController : Controller
    {
        private readonly IRestockLogRepository _restockLogRepository;
        private readonly IMapper _mapper;

        public RestockLogController(IRestockLogRepository restockLogRepository, IMapper mapper)
        {
            this._restockLogRepository = restockLogRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestockLog>))]
        public IActionResult GetRestockLog()
        {
            var restocklogs = _mapper.Map<List<RestockLogVM>>(_restockLogRepository.GetRestockLogs());

            foreach (RestockLogVM us in restocklogs)
            {
                us.DetailRestockLogs = _mapper.Map<List<DetailRestockLogVM>>(_restockLogRepository.GetDetailRestockLogs(us.RestockLogID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(restocklogs);
        }

        [HttpGet("{restocklogID}")]
        [ProducesResponseType(200, Type = typeof(RestockLog))]
        [ProducesResponseType(400)]
        public IActionResult GetRestockLog(int restocklogID)
        {
            if (!_restockLogRepository.RestockLogExists(restocklogID))
            {
                return NotFound();
            }

            var restocklog = _mapper.Map<RestockLogVM>(_restockLogRepository.GetRestockLogById(restocklogID));
            restocklog.DetailRestockLogs = _mapper.Map<List<DetailRestockLogVM>>(_restockLogRepository.GetDetailRestockLogs(restocklogID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(restocklog);
        }

        [HttpPost]
        public IActionResult CreateRestockLog(RestockLogVM restockLogCreate)
        {
            if (restockLogCreate == null || restockLogCreate.DetailRestockLogs == null)
            {
                return BadRequest(ModelState);
            }

            var restockLogMap = _mapper.Map<RestockLog>(restockLogCreate);

            if (!_restockLogRepository.CreateRestockLog(restockLogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{restocklogID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRestockLog(int restocklogID, [FromBody] RestockLogVM restocklogVM)
        {
            if (restocklogVM == null || restocklogID != restocklogVM.RestockLogID)
            {
                return BadRequest(ModelState);
            }

            if (!_restockLogRepository.RestockLogExists(restocklogID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var restocklogMap = _mapper.Map<RestockLog>(restocklogVM);

            if (!_restockLogRepository.UpdateRestockLog(restocklogMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
