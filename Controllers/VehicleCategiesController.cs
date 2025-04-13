using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.VehicleCategory;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleCategiesController : ControllerBase
    {
        private readonly VehicleCategoriesService _service;

        public VehicleCategiesController(DatabaseContext context)
        {
            _service = new VehicleCategoriesService(context);
        }

        // GET: api/VehicleCategies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleCategoryDTO>>> GetVehicleCategies() => await _service.GetDtoList();

        // GET: api/VehicleCategies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleCategoryDTO>> GetVehicleCategy(int id) => await _service.GetDtoItemById(id);

        // PUT: api/VehicleCategies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleCategy(int id, VehicleCategoryDTO vehicleCategyDTO) => await _service.UpdateItem(id, vehicleCategyDTO);

        // POST: api/VehicleCategies/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<VehicleCategoryDTO>> PostVehicleCategy(VehicleCategoryDTO vehicleCategyDTO) => await _service.CreateItem(vehicleCategyDTO);

        // DELETE: api/VehicleCategies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleCategy(int id) => await _service.DeleteItem(id);
    }
}
