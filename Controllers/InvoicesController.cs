using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Invoice;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoicesService _service;

        public InvoicesController(DatabaseContext context)
        {
            _service = new InvoicesService(context);
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoices() => await _service.GetDtoList();
        
        // GET: api/Invoices/SelectorList
        [HttpGet("SelectorList")]
        public async Task<ActionResult<IEnumerable<InvoiceSelectorDTO>>> GetInvoicesSelectorList() => await _service.GetInvoiceSelectorList();

        // GET: api/Invoices/InvoicesOfRental/4
        [HttpGet("InvoicesOfRental/{rentalId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoicesOfRental(int rentalId) => await _service.GetInvoicesOfSpecificRental(rentalId);

        // GET: api/Invoices/MaximumId
        [HttpGet("MaximumId")]
        public async Task<ActionResult<int>> GetLastInvoiceId() => await _service.GetLastInvoiceId();

        // GET: api/Invoices/Title/5
        [HttpGet("Title/{id}")]
        public async Task<ActionResult<string>> GetInvoiceTitle(int id) => await _service.GetInvoiceTitle(id);

        // GET: api/Invoices/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceDTO(int id) => await _service.GetDtoItemById(id);

        // PUT: api/Invoices/DTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutInvoiceDTO(int id, InvoiceDTO invoiceDTO) => await _service.UpdateItem(id, invoiceDTO);

        // POST: api/Invoices/DTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("DTO")]
        public async Task<ActionResult<InvoiceDTO>> PostInvoice(InvoiceDTO invoiceDTO) => await _service.CreateItem(invoiceDTO);

        // POST: api/Invoices/DTOAndPayment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("DTOAndPayment")]
        public async Task<ActionResult<InvoiceDTO>> PostInvoiceAndPayment(InvoiceDTO invoiceDTO) => await _service.CreateInvoiceAndPayment(invoiceDTO);

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id) => await _service.DeleteItem(id);
    }
}
