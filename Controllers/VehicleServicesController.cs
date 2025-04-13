using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.VehicleService;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleServicesController : ControllerBase
    {
        private readonly VehicleServicesService _service;

        public VehicleServicesController(DatabaseContext context)
        {
            _service = new VehicleServicesService(context);
        }

        // GET: api/VehicleServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleServiceDTO>>> GetVehicleServices() => await _service.GetDtoList();

        // GET: api/VehicleServices/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<VehicleServiceDTO>> GetVehicleServiceDTO(int id) => await _service.GetDtoItemById(id);

        // GET: api/VehicleServices/DTOWithVehicleName/5
        [HttpGet("DTOWithVehicleName/{id}")]
        public async Task<ActionResult<VehicleServiceDTOWithVehicleName>> GetVehicleServiceDTOWithVehicleName(int id) => await _service.GetVehicleServiceDTOWithVehicleName(id);

        // PUT: api/VehicleServices/DTO/5
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutVehicleService(int id, VehicleServiceDTO vehicleServiceDTO) => await _service.UpdateItem(id, vehicleServiceDTO);

        // POST: api/VehicleServices/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<VehicleServiceDTO>> PostVehicleService(VehicleServiceDTO vehicleServiceDTO) => await _service.CreateItem(vehicleServiceDTO);

        // DELETE: api/VehicleServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleService(int id) => await _service.DeleteItem(id);
    }
}
