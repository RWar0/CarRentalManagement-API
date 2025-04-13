using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Invoice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class InvoicesService : BaseService<Invoice, InvoiceDTO>
    {
        public InvoicesService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<InvoiceDTO>> CreateItem(InvoiceDTO modelDto)
        {
            Invoice invoice = new()
            {
                Title = modelDto.Title,
                RentalId = modelDto.RentalId,
                InvoiceDate = modelDto.InvoiceDate,
                Price = modelDto.Price,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetInvoices", "Invoices", new { id = invoice.Id }, invoice);
        }

        /// <summary>
        /// Method performs adding new item to the database and simultaneously creating Payment record for the Invoice
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public async Task<ActionResult<InvoiceDTO>> CreateInvoiceAndPayment(InvoiceDTO invoiceDTO)
        {
            Invoice invoice = new()
            {
                Title = invoiceDTO.Title,
                RentalId = invoiceDTO.RentalId,
                InvoiceDate = invoiceDTO.InvoiceDate,
                Price = invoiceDTO.Price,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            Payment payment = new()
            {
                InvoiceId = invoice.Id,
                PaymentTotal = invoice.Price,
                IsFinalized = false,
                FinalizationDate = null,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetInvoices", "Invoices", new { id = invoice.Id }, new InvoiceDTO
            {
                Id = invoice.Id,
                Title = invoice.Title,
                Price = invoice.Price
            });
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<InvoiceDTO>> GetDtoItemById(int id)
        {
            var invoice = await _context.Invoices
                .Where(item => item.Id == id)
                .Select(item => new InvoiceDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    InvoiceDate = item.InvoiceDate,
                    RentalId = item.RentalId,
                    Price = item.Price,
                })
                .FirstOrDefaultAsync();

            if (invoice == null)
            {
                return new NotFoundResult();
            }

            return invoice;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetDtoList()
        {
            return await _context.Invoices
                .Where(item => item.IsActive)
                .Select(item => new InvoiceDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    InvoiceDate = item.InvoiceDate,
                    RentalId = item.RentalId,
                    Price = item.Price,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of active items in format of Invoice Selector DTO
        /// </summary>
        /// <returns>List of active items in format of Invoice Selector DTO</returns>
        public async Task<ActionResult<IEnumerable<InvoiceSelectorDTO>>> GetInvoiceSelectorList()
        {
            return await _context.Invoices
                .Where(item => item.IsActive)
                .Select(item => new InvoiceSelectorDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method performs updating fiels of the model.
        /// It also assign EditDateTime to now
        /// </summary>
        /// <param name="id">Id of the model in the database to be updated</param>
        /// <param name="modelDto">Updated model in the DTO format</param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateItem(int id, InvoiceDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingInvoice = await _context.Invoices.FindAsync(id);

            if (existingInvoice == null)
            {
                return new NotFoundResult();
            }

            existingInvoice.Title = modelDto.Title;
            existingInvoice.InvoiceDate = modelDto.InvoiceDate;
            existingInvoice.Price = modelDto.Price;
            existingInvoice.RentalId = modelDto.RentalId;

            existingInvoice.EditDateTime = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckIfExist(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Method returns Id of the last invoice (maximum Id)
        /// </summary>
        /// <returns>Id of the last invoice (maximum Id)</returns>
        public async Task<ActionResult<int>> GetLastInvoiceId()
        {
            int? maximumId = await _context.Invoices
                .OrderByDescending(item => item.Id)
                .Select(item => item.Id)
                .FirstOrDefaultAsync();

            if (maximumId == null)
            {
                return 0;
            }

            return maximumId;
        }

        /// <summary>
        /// Method returns Title of the Invoice with specific Id
        /// </summary>
        /// <param name="id">Id of the Invoice to get title</param>
        /// <returns>Title of the Invoice with specific Id</returns>
        public async Task<ActionResult<string>> GetInvoiceTitle(int id)
        {
            string? invoiceTitle = await _context.Invoices.Where(item => item.Id == id).Select(item => item.Title).FirstOrDefaultAsync();

            if (invoiceTitle == null)
            {
                return "";
            }

            return invoiceTitle;
        }

        /// <summary>
        /// Method returns List of Specific Invoice Rentals in DTO format
        /// </summary>
        /// <param name="rentalId">Id of Rental which Invoices will be found</param>
        /// <returns>List of Specific Invoice Rentals in DTO format</returns>
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoicesOfSpecificRental(int rentalId)
        {
            return await _context.Invoices
                .Where(item => item.IsActive)
                .Where(item => item.RentalId == rentalId)
                .Select(item => new InvoiceDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    InvoiceDate = item.InvoiceDate,
                    RentalId = item.RentalId,
                    Price = item.Price,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }
    }
}
