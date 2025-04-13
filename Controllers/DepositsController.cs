using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Deposit;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly DepositsService _service;

        public DepositsController(DatabaseContext context)
        {
            _service = new DepositsService(context);
        }

        // GET: api/Deposits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepositDTO>>> GetDeposits() => await _service.GetDtoList();

        // GET: api/Deposits/DepositsOfRental/5
        [HttpGet("DepositsOfRental/{rentalId}")]
        public async Task<ActionResult<IEnumerable<DepositDTO>>> GetDepositsOfSpecificRental(int rentalId) => await _service.GetDepositsOfSpecificRental(rentalId);

        // GET: api/Deposits/Active
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<DepositDTO>>> GetActiveDeposits() => await _service.GetActiveItemsDtoList();

        // GET: api/Deposits/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<DepositDTO>> GetDepositDTO(int id) => await _service.GetDtoItemById(id);

        // GET: api/Deposits/BaseDTO/5
        [HttpGet("BaseDTO/{id}")]
        public async Task<ActionResult<BaseDepositDTO>> GetDepositBaseDTO(int id) => await _service.GetBaseDtoItemById(id);

        // PUT: api/Deposits/5/keep
        [HttpPut("{id}/keep")]
        public async Task<IActionResult> KeepDeposit(int id) => await _service.ChangeDepositStatus(id, DepositsService.DepositStatus.Kept);

        // PUT: api/Deposits/5/return
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnDeposit(int id) => await _service.ChangeDepositStatus(id, DepositsService.DepositStatus.Returned);

        // PUT: api/Deposits/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutDeposit(int id, BaseDepositDTO depositDTO) => await _service.UpdateItem(id, depositDTO);

        // POST: api/Deposits/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<BaseDepositDTO>> PostDeposit(BaseDepositDTO depositDTO) => await _service.CreateItem(depositDTO);

        // DELETE: api/Deposits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeposit(int id) => await _service.DeleteItem(id);
    }
}
