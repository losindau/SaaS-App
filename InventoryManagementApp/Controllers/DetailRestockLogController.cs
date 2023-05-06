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
    public class DetailRestockLogController : Controller
    {
        private readonly IDetailRestockLogRepository _detailRestockLogRepository;
        private readonly IMapper _mapper;

        public DetailRestockLogController(IDetailRestockLogRepository detailRestockLogRepository, IMapper mapper)
        {
            this._detailRestockLogRepository = detailRestockLogRepository;
            this._mapper = mapper;
        }

        [HttpGet("{detailRestocklogID}")]
        [ProducesResponseType(200, Type = typeof(DetailRestockLog))]
        [ProducesResponseType(400)]
        public IActionResult GetDetailRestockLog(int detailRestocklogID)
        {
            if (!_detailRestockLogRepository.DetailRestockLogExists(detailRestocklogID))
            {
                return NotFound();
            }

            var restocklog = _mapper.Map<DetailRestockLogVM>(_detailRestockLogRepository.GetDetailRestockLogById(detailRestocklogID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(restocklog);
        }

        [HttpPost]
        public IActionResult CreateDetailRestockLog(List<DetailRestockLogVM> detailRestockLogCreate)
        {
            if (detailRestockLogCreate == null)
            {
                return BadRequest(ModelState);
            }

            var restockLogMap = _mapper.Map<List<DetailRestockLog>>(detailRestockLogCreate);

            if (!_detailRestockLogRepository.CreateDetailRestockLogs(restockLogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{detailrestocklogID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDetailRestocklog(int detailrestocklogID, [FromBody] DetailRestockLogVM detailrestocklogVM)
        {
            if (detailrestocklogVM == null || detailrestocklogID != detailrestocklogVM.DetailRestockLogID)
            {
                return BadRequest(ModelState);
            }

            if (!_detailRestockLogRepository.DetailRestockLogExists(detailrestocklogID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var detailrestocklogMap = _mapper.Map<DetailRestockLog>(detailrestocklogVM);

            if (!_detailRestockLogRepository.UpdateDetailRestockLog(detailrestocklogMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
