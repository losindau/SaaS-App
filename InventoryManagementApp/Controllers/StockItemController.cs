using AutoMapper;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockItemController : Controller
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMapper _mapper;

        public StockItemController(IStockItemRepository stockItemRepository, IMapper mapper)
        {
            this._stockItemRepository = stockItemRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StockItem>))]
        public IActionResult GetStockItems()
        {
            var stockitems = _mapper.Map<List<StockItemVM>>(_stockItemRepository.GetStockItems());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(stockitems);
        }

        [HttpGet("{stockitemID}")]
        [ProducesResponseType(200, Type = typeof(StockItem))]
        [ProducesResponseType(400)]
        public IActionResult GetStockItem(int stockitemID)
        {
            if (!_stockItemRepository.StockItemExists(stockitemID))
            {
                return NotFound();
            }

            var stockitem = _mapper.Map<StockItemVM>(_stockItemRepository.GetStockItemById(stockitemID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(stockitem);
        }
    }
}
