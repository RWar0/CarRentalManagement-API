using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Warning;
using CarRentalManagementAPI.Models.DTOs.Insurance;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarningsController : ControllerBase
    {
        private readonly WarningsService _service;

        public WarningsController(DatabaseContext context)
        {
            _service = new WarningsService(context);
        }

        // GET: api/Warnings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarningDTO>>> GetWarnings() => await _service.GetFullDtoList();

        // GET: api/Warnings/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<BaseWaringDTO>> GetWarningDto(int id) => await _service.GetDtoItemById(id);

        // GET: api/Warnings/CustomerWarnings/5
        [HttpGet("CustomerWarnings/{customerId}")]
        public async Task<ActionResult<IEnumerable<WarningDTO>>> GetWarningsOfCustomer(int customerId) => await _service.GetWarningsOfCustomer(customerId);

        // PUT: api/Warnings/DTO/5
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutWarning(int id, BaseWaringDTO warningDTO) => await _service.UpdateItem(id, warningDTO);

        // POST: api/Warnings/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<BaseWaringDTO>> PostWarning(BaseWaringDTO warningDTO) => await _service.CreateItem(warningDTO);

        // DELETE: api/Warnings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarning(int id) => await _service.DeleteItem(id);
    }
}
