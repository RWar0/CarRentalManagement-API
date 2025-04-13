using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Payment;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsService _service;

        public PaymentsController(DatabaseContext context)
        {
            _service = new PaymentsService(context);
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments() => await _service.GetDtoList();

        // GET: api/Payments/Finalized
        [HttpGet("Finalized")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetFinalizedPayments() => await _service.GetPaymentsWithSpecificStatus(true);

        // GET: api/Payments/NotFinalized
        [HttpGet("NotFinalized")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetNotFinalizedPayments() => await _service.GetPaymentsWithSpecificStatus(false);

        // GET: api/Payments/PaymentsOfInvoice/5/Finalized
        [HttpGet("PaymentsOfInvoice/{invoiceId}/Finalized")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetFinalizedPaymentsOfSpecificInvoice(int invoiceId) => await _service.GetPaymentsOfSpecificInvoiceWithSpecificStatus(invoiceId, true);

        // GET: api/Payments/PaymentsOfInvoice/5/NotFinalized
        [HttpGet("PaymentsOfInvoice/{invoiceId}/NotFinalized")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetNotFinalizedPaymentsOfSpecificInvoice(int invoiceId) => await _service.GetPaymentsOfSpecificInvoiceWithSpecificStatus(invoiceId, false);


        // GET: api/Payments/CurrentIncomes
        [HttpGet("CurrentIncomes")]
        public async Task<ActionResult<decimal>> GetCurrentIncomes() => await _service.GetIncomes(PaymentsService.PeriodOfIncomes.CurrentMonth);

        // GET: api/Payments/PreviousIncomes
        [HttpGet("PreviousIncomes")]
        public async Task<ActionResult<decimal>> GetPreviousIncomes() => await _service.GetIncomes(PaymentsService.PeriodOfIncomes.PreviousMonth);

        // GET: api/Payments/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentDTO(int id) => await _service.GetDtoItemById(id);

        // PUT: api/Payments/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDTO paymentDTO) => await _service.UpdateItem(id, paymentDTO);


        // PUT: api/Payments/Finalize/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Finalize/{id}")]
        public async Task<IActionResult> FinalizePayment(int id) => await _service.FinalizePayment(id);

        // POST: api/Payments/DTO
        [HttpPost("DTO")]
        public async Task<ActionResult<BasePaymentDTO>> PostPayment(BasePaymentDTO paymentDTO) => await _service.CreateItem(paymentDTO);

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id) => await _service.DeleteItem(id);
    }
}
