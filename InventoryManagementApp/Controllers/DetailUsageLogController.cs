using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailUsageLogController : Controller
    {
        private readonly IDetailUsageLogRepository _detailUsageLogRepository;
        private readonly IMapper _mapper;

        public DetailUsageLogController(IDetailUsageLogRepository detailUsageLogRepository, IMapper mapper)
        {
            this._detailUsageLogRepository = detailUsageLogRepository;
            this._mapper = mapper;
        }

        [HttpGet("{detailusagelogID}")]
        [ProducesResponseType(200, Type = typeof(DetailUsageLog))]
        [ProducesResponseType(400)]
        public IActionResult GetDetailUsageLog(int detailusagelogID)
        {
            if (!_detailUsageLogRepository.DetailUsageLogExists(detailusagelogID))
            {
                return NotFound();
            }

            var detailusagelog = _mapper.Map<DetailUsageLogVM>(_detailUsageLogRepository.GetDetailUsageLogById(detailusagelogID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(detailusagelog);
        }

        [HttpPost]
        public IActionResult CreateDetailUsageLog(List<DetailUsageLogVM> detailusageLogCreate)
        {
            if (detailusageLogCreate == null)
            {
                return BadRequest(ModelState);
            }

            var detailusageLogMap = _mapper.Map<List<DetailUsageLog>>(detailusageLogCreate);

            if (!_detailUsageLogRepository.CreateDetailUsageLogs(detailusageLogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{detailusagelogID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDetailUsagelog(int detailusagelogID, [FromBody] DetailUsageLogVM detailusagelogVM)
        {
            if (detailusagelogVM == null || detailusagelogID != detailusagelogVM.DetailUsageLogID)
            {
                return BadRequest(ModelState);
            }

            if (!_detailUsageLogRepository.DetailUsageLogExists(detailusagelogID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var detailusagelogMap = _mapper.Map<DetailUsageLog>(detailusagelogVM);

            if (!_detailUsageLogRepository.UpdateDetailUsageLog(detailusagelogMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
