using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsageLogController : Controller
    {
        private readonly IUsageLogRepository _usageLogRepository;
        private readonly IMapper _mapper;

        public UsageLogController(IUsageLogRepository usageLogRepository, IMapper mapper)
        {
            this._usageLogRepository = usageLogRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsageLog>))]
        public IActionResult GetUsageLog()
        {
            var usagelogs = _mapper.Map<List<UsageLogVM>>(_usageLogRepository.GetUsageLogs());

            foreach (UsageLogVM us in usagelogs)
            {
                us.DetailUsageLogs = _mapper.Map<List<DetailUsageLogVM>>(_usageLogRepository.GetDetailUsageLogs(us.UsageLogID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(usagelogs);
        }

        [HttpGet("{usagelogID}")]
        [ProducesResponseType(200, Type = typeof(UsageLog))]
        [ProducesResponseType(400)]
        public IActionResult GetUsageLog(int usagelogID)
        {
            if (!_usageLogRepository.UsageLogExists(usagelogID))
            {
                return NotFound();
            }

            var usagelog = _mapper.Map<UsageLogVM>(_usageLogRepository.GetUsageLogById(usagelogID));
            usagelog.DetailUsageLogs = _mapper.Map<List<DetailUsageLogVM>>(_usageLogRepository.GetDetailUsageLogs(usagelogID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(usagelog);
        }
    }
}
