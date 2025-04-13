using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Service;
using CarRentalManagementAPI.Models.DTOs.Insurance;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServicesService _service;

        public ServicesController(DatabaseContext context)
        {
            _service = new ServicesService(context);
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices() => await _service.GetDtoList();

        // GET: api/Services/ServicesOfVehicle/5
        [HttpGet("ServicesOfVehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<ServiceOfVehicleDTO>>> GetServicesOfSpecificVehicle(int vehicleId) => await _service.GetServicesOfSpecificVehicle(vehicleId);

        // GET: api/Services/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<ServiceDTO>> GetServiceDTO(int id) => await _service.GetDtoItemById(id);

        // PUT: api/Services/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutService(int id, ServiceDTO serviceDTO) => await _service.UpdateItem(id, serviceDTO);

        // POST: api/Services/DTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("DTO")]
        public async Task<ActionResult<ServiceDTO>> PostService(ServiceDTO serviceDTO) => await _service.CreateItem(serviceDTO);

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id) => await _service.DeleteItem(id);
    }
}
