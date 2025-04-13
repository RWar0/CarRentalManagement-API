using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Fueling;
using CarRentalManagementAPI.Models.DTOs.Service;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelingsController : ControllerBase
    {
        private readonly FuelingsService _service;

        public FuelingsController(DatabaseContext context)
        {
            _service = new FuelingsService(context);
        }

        // GET: api/Fuelings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelingDTO>>> GetFuelings() => await _service.GetFullDtoList();

        // GET: api/Fuelings/FuelingsOfVehicle/5
        [HttpGet("FuelingsOfVehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<FuelingDTO>>> GetFuelingsOfSpecificVehicle(int vehicleId) => await _service.GetFuelingsOfSpecificVehicle(vehicleId);

        // GET: api/Fuelings/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<BaseFuelingDTO>> GetFuelingDto(int id) => await _service.GetDtoItemById(id);

        // PUT: api/Fuelings/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutFueling(int id, BaseFuelingDTO fuelingDTO) => await _service.UpdateItem(id, fuelingDTO);

        // POST: api/DTO/Fuelings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("DTO")]
        public async Task<ActionResult<BaseFuelingDTO>> PostFueling(BaseFuelingDTO fuelingDTO) => await _service.CreateItem(fuelingDTO);

        // DELETE: api/Fuelings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFueling(int id) => await _service.DeleteItem(id);
    }
}
