using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class PaymentsService : BaseService<Payment, PaymentDTO>
    {
        public enum PeriodOfIncomes
        {
            CurrentMonth,
            PreviousMonth,
        }

        public PaymentsService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<PaymentDTO>> CreateItem(PaymentDTO modelDto)
        {
            Payment payment = new()
            {
                InvoiceId = modelDto.InvoiceId,
                PaymentTotal = modelDto.PaymentTotal,
                IsFinalized = modelDto.IsFinalized,
                FinalizationDate = modelDto.FinalizationDate,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetPayments", "Payments", new { id = payment.Id }, payment);
        }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the Base DTO format to be created in databaase</param>
        /// <returns></returns>
        public async Task<ActionResult<BasePaymentDTO>> CreateItem(BasePaymentDTO modelDto)
        {
            Payment payment = new()
            {
                InvoiceId = modelDto.InvoiceId,
                PaymentTotal = modelDto.PaymentTotal,
                IsFinalized = modelDto.IsFinalized,
                FinalizationDate = modelDto.FinalizationDate,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetPayments", "Payments", new { id = payment.Id }, payment);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<PaymentDTO>> GetDtoItemById(int id)
        {
            var payment = await _context.Payments
                .Include(item => item.Invoice)
                .Where(item => item.IsActive)
                .Where(item => item.Id == id)
                .Select(item => new PaymentDTO()
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    InvoiceTitle = item.Invoice.Title,
                    PaymentTotal = item.PaymentTotal,
                    IsFinalized = item.IsFinalized,
                    FinalizationDate = item.FinalizationDate,
                })
                .FirstOrDefaultAsync();

            if (payment == null)
            {
                return new NotFoundResult();
            }

            return payment;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<PaymentDTO>>> GetDtoList()
        {
            return await _context.Payments
                .Include(item => item.Invoice)
                .Where(item => item.IsActive)
                .Select(item => new PaymentDTO()
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    InvoiceTitle = item.Invoice.Title,
                    PaymentTotal = item.PaymentTotal,
                    IsFinalized = item.IsFinalized,
                    FinalizationDate = item.FinalizationDate,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of Payments in DTO format with desired IsFinalized status
        /// </summary>
        /// <param name="isFinalized">Boolean status of IsFinalized payment</param>
        /// <returns>List of Payments in DTO format with desired IsFinalized status</returns>
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsWithSpecificStatus(bool isFinalized)
        {
            return await _context.Payments
                .Include(item => item.Invoice)
                .Where(item => item.IsActive)
                .Where(item => item.IsFinalized == isFinalized)
                .Select(item => new PaymentDTO()
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    InvoiceTitle = item.Invoice.Title,
                    PaymentTotal = item.PaymentTotal,
                    IsFinalized = item.IsFinalized,
                    FinalizationDate = item.FinalizationDate,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of Specific Invoice Payments in DTO format with desired IsFinalized status
        /// </summary>
        /// <param name="invoiceId">Id of invoile which Payments will be found</param>
        /// <param name="isFinalized">Boolean status of IsFinalized payment</param>
        /// <returns>List of Specific Invoice Payments in DTO format with desired IsFinalized status</returns>
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsOfSpecificInvoiceWithSpecificStatus(int invoiceId, bool isFinalized)
        {
            return await _context.Payments
                .Include(item => item.Invoice)
                .Where(item => item.IsActive)
                .Where(item => item.InvoiceId == invoiceId)
                .Where(item => item.IsFinalized == isFinalized)
                .Select(item => new PaymentDTO()
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    InvoiceTitle = item.Invoice.Title,
                    PaymentTotal = item.PaymentTotal,
                    IsFinalized = item.IsFinalized,
                    FinalizationDate = item.FinalizationDate,
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
        public override async Task<IActionResult> UpdateItem(int id, PaymentDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingPayment = await _context.Payments.FindAsync(id);

            if (existingPayment == null)
            {
                return new NotFoundResult();
            }

            existingPayment.PaymentTotal = modelDto.PaymentTotal;
            existingPayment.IsFinalized = modelDto.IsFinalized;
            existingPayment.FinalizationDate = modelDto.FinalizationDate;

            existingPayment.EditDateTime = DateTime.Now;

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
        /// Method that returns value of incomes in specific period
        /// </summary>
        /// <param name="periodOfIncomes">Period of incomes from enumeration</param>
        /// <returns>Decimal value of incomes in specific period</returns>
        public async Task<ActionResult<decimal>> GetIncomes(PeriodOfIncomes periodOfIncomes)
        {
            int monthNo = DateTime.Now.Month;
            int yearNo = DateTime.Now.Year;

            if (periodOfIncomes == PeriodOfIncomes.PreviousMonth)
            {
                if(monthNo == 1)
                {
                    monthNo = 12;
                    yearNo--;
                }
                else
                {
                    monthNo--;
                }
            }

            return await _context.Payments
                .Where(item => item.IsActive)
                .Where(item => item.IsFinalized)
                .Where(item => item.FinalizationDate.HasValue && item.FinalizationDate.Value.Month == monthNo && item.FinalizationDate.Value.Year == yearNo)
                .SumAsync(item => item.PaymentTotal);
        }

        /// <summary>
        /// Method that change IsFinalize status to true
        /// </summary>
        /// <param name="id">Id of the Payment to change IsFinalize to true</param>
        /// <returns></returns>
        public async Task<IActionResult> FinalizePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return new NotFoundResult();
            }

            payment.IsFinalized = true;
            payment.FinalizationDate = DateOnly.FromDateTime(DateTime.Now);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new NotFoundResult();
            }

            return new NoContentResult();

        }
    }
}
