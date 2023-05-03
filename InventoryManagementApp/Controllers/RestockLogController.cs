using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
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

            var usagelog = _mapper.Map<RestockLogVM>(_restockLogRepository.RestockLogExists(restocklogID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(usagelog);
        }
    }
}
