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
    public class DetailEqDamageLogController : Controller
    {
        private readonly IDetailEqDamageLogRepository _detailEqDamageLogRepository;
        private readonly IMapper _mapper;

        public DetailEqDamageLogController(IDetailEqDamageLogRepository detaileqDamageLogRepository, IMapper mapper)
        {
            this._detailEqDamageLogRepository = detaileqDamageLogRepository;
            this._mapper = mapper;
        }

        [HttpGet("{detailEqDamagelogID}")]
        [ProducesResponseType(200, Type = typeof(DetailEqDamageLog))]
        [ProducesResponseType(400)]
        public IActionResult GetDetailEqDamageLog(int detailEqDamagelogID)
        {
            if (!_detailEqDamageLogRepository.DetailEqDamageLogExists(detailEqDamagelogID))
            {
                return NotFound();
            }

            var eqdamagelog = _mapper.Map<DetailEqDamageLogVM>(_detailEqDamageLogRepository.GetDetailEqDamageLogById(detailEqDamagelogID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(eqdamagelog);
        }

        [HttpPost]
        public IActionResult CreateDetailEqDamageLog(List<DetailEqDamageLogVM> eqDamageLogCreate)
        {
            if (eqDamageLogCreate == null)
            {
                return BadRequest(ModelState);
            }

            var eqDamageLogMap = _mapper.Map<List<DetailEqDamageLog>>(eqDamageLogCreate);

            if (!_detailEqDamageLogRepository.CreateDetailEqDamageLogs(eqDamageLogMap))
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        [HttpPut("{detailEqDamagelogID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDetailEqDamageLog(int detailEqDamagelogID, [FromBody] DetailEqDamageLogVM detaileqdamagelogVM)
        {
            if (detaileqdamagelogVM == null || detailEqDamagelogID != detaileqdamagelogVM.DetailEqDamageLogID)
            {
                return BadRequest(ModelState);
            }

            if (!_detailEqDamageLogRepository.DetailEqDamageLogExists(detailEqDamagelogID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var detaileqdamagelogMap = _mapper.Map<DetailEqDamageLog>(detaileqdamagelogVM);

            if (!_detailEqDamageLogRepository.UpdateDetailEqDamageLog(detaileqdamagelogMap))
            {
                return StatusCode(500, "Something went wrong updating");
            }

            return Ok("Updated successfully");
        }
    }
}
