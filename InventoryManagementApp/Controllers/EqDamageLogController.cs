using AutoMapper;
using InventoryManagementApp.Data;
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
    public class EqDamageLogController : Controller
    {
        private readonly IEqDamageLogRepository _eqDamageLogRepository;
        private readonly IMapper _mapper;

        public EqDamageLogController(IEqDamageLogRepository eqDamageLogRepository,IMapper mapper)
        {
            this._eqDamageLogRepository = eqDamageLogRepository;
            this._mapper = mapper;
        }

        [HttpGet("{page}/eqdamagelogs")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EqDamageLog>))]
        public IActionResult GetEqDamageLog(int page)
        {
            var eqdamagelogs = _eqDamageLogRepository.GetEqDamageLogs();

            var pageResults = 5f;
            var pageCount = Math.Ceiling(eqdamagelogs.Count() / pageResults);

            var eqdamagelogsMap = _mapper.Map<List<EqDamageLogVM>>(eqdamagelogs.Skip((page - 1) * (int)pageResults).Take((int)pageResults));

            foreach (EqDamageLogVM us in eqdamagelogsMap)
            {
                us.DetailEqDamageLogs = _mapper.Map<List<DetailEqDamageLogVM>>(_eqDamageLogRepository.GetDetailEqDamageLogs(us.EqDamageLogID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(eqdamagelogsMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("{eqdamagelogID}")]
        [ProducesResponseType(200, Type = typeof(EqDamageLog))]
        [ProducesResponseType(400)]
        public IActionResult GetEqDamageLogByID(int eqdamagelogID)
        {
            if (!_eqDamageLogRepository.EqDamageLogExists(eqdamagelogID))
            {
                return NotFound();
            }

            var eqdamagelog = _mapper.Map<EqDamageLogVM>(_eqDamageLogRepository.GetEqDamageLogById(eqdamagelogID));
            eqdamagelog.DetailEqDamageLogs = _mapper.Map<List<DetailEqDamageLogVM>>(_eqDamageLogRepository.GetDetailEqDamageLogs(eqdamagelogID));


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(eqdamagelog);
        }

        [HttpGet("{page}/myeqdamagelogs/{userID}")]
        [ProducesResponseType(200, Type = typeof(EqDamageLog))]
        [ProducesResponseType(400)]
        public IActionResult GetUsageLogByUserID(int page, string userID)
        {
            var eqdamagelog = _eqDamageLogRepository.GetEqDamageLogByUserId(userID);

            var pageResults = 5f;
            var pageCount = Math.Ceiling(eqdamagelog.Count() / pageResults);

            var eqdamgelogsMap = _mapper.Map<List<EqDamageLogVM>>(eqdamagelog.Skip((page - 1) * (int)pageResults).Take((int)pageResults));

            foreach (EqDamageLogVM item in eqdamgelogsMap)
            {
                item.DetailEqDamageLogs = _mapper.Map<List<DetailEqDamageLogVM>>(_eqDamageLogRepository.GetDetailEqDamageLogs(item.EqDamageLogID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(eqdamgelogsMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateEqDamageLog(EqDamageLogVM eqDamageLogCreate)
        {
            if (eqDamageLogCreate == null)
            {
                return BadRequest(ModelState);
            }

            var eqDamageLogMap = _mapper.Map<EqDamageLog>(eqDamageLogCreate);

            if (!_eqDamageLogRepository.CreateEqDamageLog(eqDamageLogMap))
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            return Ok(eqDamageLogMap.EqDamageLogID);
        }

        [HttpPut("{eqdamagelogID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEqDamaggeLog(int eqdamagelogID, [FromBody] EqDamageLogVM eqdamagelogVM)
        {
            if (eqdamagelogVM == null || eqdamagelogID != eqdamagelogVM.EqDamageLogID)
            {
                return BadRequest(ModelState);
            }

            if (!_eqDamageLogRepository.EqDamageLogExists(eqdamagelogID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var eqdamagelogMap = _mapper.Map<EqDamageLog>(eqdamagelogVM);

            if (!_eqDamageLogRepository.UpdateEqDamageLog(eqdamagelogMap))
            {
                return StatusCode(500, "Something went wrong updating");
            }

            return Ok("Updated successfully");
        }
    }
}
