﻿using AutoMapper;
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
    public class TruckController : Controller
    {
        private readonly ITruckRepository _truckRepository;
        private readonly IMapper _mapper;

        public TruckController(ITruckRepository truckRepository, IMapper mapper)
        {
            this._truckRepository = truckRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Truck>))]
        public IActionResult GetTruck()
        {
            var trucks = _mapper.Map<List<TruckVM>>(_truckRepository.GetTrucks());

            foreach (TruckVM us in trucks)
            {
                us.TruckStockItems = _mapper.Map<List<TruckStockItemVM>>(_truckRepository.GetTruckStockItems(us.TruckID));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(trucks);
        }

        [HttpGet("{truckID}")]
        [ProducesResponseType(200, Type = typeof(Truck))]
        [ProducesResponseType(400)]
        public IActionResult GetTruck(int truckID)
        {
            if (!_truckRepository.TruckExists(truckID))
            {
                return NotFound();
            }

            var truck = _mapper.Map<TruckVM>(_truckRepository.GetTruckId(truckID));
            truck.TruckStockItems = _mapper.Map<List<TruckStockItemVM>>(_truckRepository.GetTruckStockItems(truckID));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(truck);
        }

        [HttpPost]
        public IActionResult CreateTruck(TruckVM truckCreate)
        {
            if (truckCreate == null || truckCreate.TruckStockItems == null)
            {
                return BadRequest(ModelState);
            }

            var truckMap = _mapper.Map<Truck>(truckCreate);

            if (!_truckRepository.CreateTruck(truckMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{truckID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTruck(int truckID, [FromBody] TruckVM truckVM)
        {
            if (truckVM == null || truckID != truckVM.TruckID)
            {
                return BadRequest(ModelState);
            }

            if (!_truckRepository.TruckExists(truckID))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var truckMap = _mapper.Map<Truck>(truckVM);

            if (!_truckRepository.UpdateTruck(truckMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }
    }
}
