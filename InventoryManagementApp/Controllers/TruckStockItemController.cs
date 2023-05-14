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
    public class TruckStockItemController : Controller
    {
        private readonly ITruckStockItemRepository _truckStockItemRepository;
        private readonly IMapper _mapper;

        public TruckStockItemController(ITruckStockItemRepository truckStockItemRepository, IMapper mapper)
        {
            this._truckStockItemRepository = truckStockItemRepository;
            this._mapper = mapper;
        }

        [HttpGet("{truckStockItemID}/id")]
        [ProducesResponseType(200, Type = typeof(TruckStockItem))]
        [ProducesResponseType(400)]
        public IActionResult GetTruckStockItemById(int truckStockItemID)
        {
            if (!_truckStockItemRepository.TruckStockItemExists(truckStockItemID))
            {
                return NotFound();
            }

            var truckStockItem = _mapper.Map<TruckStockItemVM>(_truckStockItemRepository.GetTruckStockItemById(truckStockItemID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(truckStockItem);
        }

        [HttpGet("{itemID}/itemid")]
        [ProducesResponseType(200, Type = typeof(TruckStockItem))]
        [ProducesResponseType(400)]
        public IActionResult GetTruckStockItemByItemId(int itemID)
        {
            var truckStockItem = _mapper.Map<TruckStockItemVM>(_truckStockItemRepository.GetTruckStockItemByItemId(itemID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(truckStockItem);
        }

        [HttpPost]
        public IActionResult CreateTruckStockItem(List<TruckStockItemVM> truckStockItemCreate)
        {
            if (truckStockItemCreate == null)
            {
                return BadRequest(ModelState);
            }

            var truckStockItemMap = _mapper.Map<List<TruckStockItem>>(truckStockItemCreate);

            if (!_truckStockItemRepository.CreateTruckStockItems(truckStockItemMap))
            {
                return StatusCode(500, "Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        [HttpPut("{truckstockitemID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTruckStockItem(int truckstockitemID, [FromBody] TruckStockItemVM truckstockitemVM)
        {
            if (truckstockitemVM == null || truckstockitemID != truckstockitemVM.TruckStockItemID)
            {
                return BadRequest(ModelState);
            }

            if (!_truckStockItemRepository.TruckStockItemExists(truckstockitemID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var truckstockitemMap = _mapper.Map<TruckStockItem>(truckstockitemVM);

            if (!_truckStockItemRepository.UpdateTruckStockItem(truckstockitemMap))
            {
                return StatusCode(500, "Something went wrong updating");
            }

            return Ok("Updated successfully");
        }
    }
}
