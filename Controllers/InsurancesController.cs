using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Insurance;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly InsurancesService _service;

        public InsurancesController(DatabaseContext context)
        {
            _service = new InsurancesService(context);
        }

        // GET: api/Insurances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceDTO>>> GetInsurances() => await _service.GetDtoList();

        // GET: api/Insurances/InsurancesOfVehicle/4
        [HttpGet("InsurancesOfVehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<InsuranceDTO>>> GetInsurancesOfSpecificVehicle(int vehicleId) => await _service.GetInsurancesOfSpecificVehicle(vehicleId);

        // GET: api/Insurances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceDTO>> GetInsurance(int id) => await _service.GetDtoItemById(id);

        // PUT: api/Insurances/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutInsurance(int id, BaseInsuranceDTO insuranceDTO) => await _service.UpdateItem(id, insuranceDTO);

        // POST: api/Insurances/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<InsuranceDTO>> PostInsurance(BaseInsuranceDTO insuranceDTO) => await _service.CreateItem(insuranceDTO);

        // DELETE: api/Insurances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id) => await _service.DeleteItem(id);
    }
}
