using AutoMapper;
using InventoryManagementApp.Data;
using InventoryManagementApp.Data.Interfaces;
using InventoryManagementApp.Data.Models;
using InventoryManagementApp.Data.Repository;
using InventoryManagementApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [HttpGet("{page}/stockitems")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StockItem>))]
        public IActionResult GetStockItems(int page)
        {
            var stockitems = _stockItemRepository.GetStockItems();

            var pageResults = 5f;
            var pageCount = Math.Ceiling(stockitems.Count() / pageResults);

            var stockitemsMap = _mapper.Map<List<StockItemVM>>(stockitems.Skip((page - 1) * (int)pageResults).Take((int)pageResults));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponsePagination()
            {
                Entities = new List<object>(stockitemsMap),
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet("{stockitemID}")]
        [Authorize(Roles = "Manager")]
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

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult CreateStockItem([FromBody] StockItemVM stockitemCreate)
        {
            if (stockitemCreate == null)
            {
                return BadRequest(ModelState);                
            }

            var stockitems = _stockItemRepository.GetStockItems()
                .Where(i => i.Name.Trim().ToLower().Equals(stockitemCreate.Name.Trim().ToLower()))
                .FirstOrDefault();

            if (stockitems != null)
            {
                ModelState.AddModelError("", "This item is already exists");
                return StatusCode(422, ModelState);
            }

            var stockitemMap = _mapper.Map<StockItem>(stockitemCreate);

            if (!_stockItemRepository.CreateStockItem(stockitemMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{stockitemID}")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStockItem(int stockitemID, [FromBody] StockItemVM stockItemVM)
        {
            if (stockItemVM == null || stockitemID != stockItemVM.StockItemID)
            {
                return BadRequest(ModelState);
            }

            if (!_stockItemRepository.StockItemExists(stockitemID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stockitemMap = _mapper.Map<StockItem>(stockItemVM);

            if (!_stockItemRepository.UpdateStockItem(stockitemMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
