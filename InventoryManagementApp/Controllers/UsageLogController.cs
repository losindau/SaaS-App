using AutoMapper;
using InventoryManagementApp.Data;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;

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

        [HttpGet("{page}/usagelogs")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsageLog>))]
        public IActionResult GetUsageLog(int page)
        {
            var usagelogs = _usageLogRepository.GetUsageLogs();

            var pageResults = 5f;
            var pageCount = Math.Ceiling(usagelogs.Count() / pageResults);

            var usagelogsMap = _mapper.Map<List<UsageLogVM>>(usagelogs.Skip((page - 1) * (int)pageResults).Take((int)pageResults));

            foreach (UsageLogVM us in usagelogsMap)
            {
                us.DetailUsageLogs = _mapper.Map<List<DetailUsageLogVM>>(_usageLogRepository.GetDetailUsageLogs(us.UsageLogID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(usagelogsMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet("{usagelogID}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(UsageLog))]
        [ProducesResponseType(400)]
        public IActionResult GetUsageLogByID(int usagelogID)
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

        [HttpGet("{page}/myusagelogs/{userID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsageLog>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsageLogByUserID(int page, string userID)
        {
            var usagelogs = _usageLogRepository.GetUsageLogByUserId(userID);

            var pageResults = 5f;
            var pageCount = Math.Ceiling(usagelogs.Count() / pageResults);

            var usagelogsMap = _mapper.Map<List<UsageLogVM>>(usagelogs.Skip((page - 1) * (int)pageResults).Take((int)pageResults));

            foreach (UsageLogVM us in usagelogsMap)
            {
                us.DetailUsageLogs = _mapper.Map<List<DetailUsageLogVM>>(_usageLogRepository.GetDetailUsageLogs(us.UsageLogID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(usagelogsMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateUsageLog(UsageLogVM usageLogCreate)
        {
            if (usageLogCreate == null)
            {
                return BadRequest(ModelState);
            }

            var usageLogMap = _mapper.Map<UsageLog>(usageLogCreate);

            if (!_usageLogRepository.CreateUsageLog(usageLogMap))
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            return Ok(usageLogMap.UsageLogID);
        }

        [HttpPut("{usagelogID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUsageLog(int usagelogID, [FromBody] UsageLogVM usagelogVM)
        {
            if (usagelogVM == null || usagelogID != usagelogVM.UsageLogID)
            {
                return BadRequest(ModelState);
            }

            if (!_usageLogRepository.UsageLogExists(usagelogID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usagelogMap = _mapper.Map<UsageLog>(usagelogVM);

            if (!_usageLogRepository.UpdateUsageLog(usagelogMap))
            {
                return StatusCode(500, "Something went wrong updating");
            }

            return Ok("Updated successfully");
        }
    }
}
