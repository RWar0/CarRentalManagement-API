using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Vehicle;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesService _service;

        public VehiclesController(DatabaseContext context)
        {
            _service = new VehiclesService(context);
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles() => await _service.GetFullDtoList();

        // GET: api/Vehicles/ServicedVehicles/4
        [HttpGet("ServicedVehicles/{serviceId}")]
        public async Task<ActionResult<IEnumerable<ServicedVehicleDTO>>> GetVehiclesServicesBySpecificOne(int serviceId) => await _service.GetVehiclesServicesBySpecificOne(serviceId);

        // GET: api/Vehicles/SelectorList
        [HttpGet("SelectorList")]
        public async Task<ActionResult<IEnumerable<VehicleSelectorDTO>>> GetVehiclesSelectorList() => await _service.GetVehiclesSelectorList();

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseVehicleDTO>> GetVehicle(int id) => await _service.GetDtoItemById(id);
        
        // GET: api/Vehicles/5
        [HttpGet("FullDTO/{id}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicleFullDTO(int id) => await _service.GetFullDtoItemById(id);

        // PUT: api/Vehicles/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutVehicle(int id, BaseVehicleDTO vehicleDTO) => await _service.UpdateItem(id, vehicleDTO);

        // POST: api/Vehicles/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<BaseVehicleDTO>> PostVehicle(BaseVehicleDTO vehicleDto) => await _service.CreateItem(vehicleDto);

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id) => await _service.DeleteItem(id);

    }
}
