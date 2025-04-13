using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Warning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class WarningsService : BaseService<Warning, BaseWaringDTO>
    {
        public WarningsService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the Base DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<BaseWaringDTO>> CreateItem(BaseWaringDTO modelDto)
        {
            Warning warning = new()
            {
                CustomerId = modelDto.CustomerId,
                Description = modelDto.Description,
                WarningDate = modelDto.WarningDate,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Warnings.Add(warning);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetWarnings", "Warnings", new { id = warning.Id }, warning);
        }

        /// <summary>
        /// Method returns Item with specific Id in Base DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the Base DTO format</returns>
        public override async Task<ActionResult<BaseWaringDTO>> GetDtoItemById(int id)
        {
            var warning = await _context.Warnings
                .Where(item => item.Id == id)
                .Select(item => new BaseWaringDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    Description = item.Description,
                    WarningDate = item.WarningDate,
                })
                .FirstOrDefaultAsync();

            if (warning == null)
            {
                return new NotFoundResult();
            }

            return warning;
        }

        /// <summary>
        /// Method returns List of items in Base DTO format
        /// </summary>
        /// <returns>List of items in Base DTO format</returns>
        public override async Task<ActionResult<IEnumerable<BaseWaringDTO>>> GetDtoList()
        {
            return await _context.Warnings
                .Where(item => item.IsActive)
                .Select(item => new BaseWaringDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    Description = item.Description,
                    WarningDate = item.WarningDate,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of items in Full DTO format (with CustomerName)
        /// </summary>
        /// <returns>List of items in Full DTO format (with CustomerName)</returns>
        public async Task<ActionResult<IEnumerable<WarningDTO>>> GetFullDtoList()
        {
            return await _context.Warnings
                .Include(item => item.Customer)
                .Where(item => item.IsActive)
                .Select(item => new WarningDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    CustomerName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    Description = item.Description,
                    WarningDate = item.WarningDate,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }


        /// <summary>
        /// Method returns List of Specific Customer Warnings in Base DTO format
        /// </summary>
        /// <param name="customerId">Id of customer which warnings will be found</param>
        /// <returns>List of Specific Customer Warnings in Base DTO format</returns>
        public async Task<ActionResult<IEnumerable<WarningDTO>>> GetWarningsOfCustomer(int customerId)
        {
            var warnings = await _context.Warnings
                .Include(item => item.Customer)
                .Where(item => item.IsActive)
                .Where(item => item.CustomerId == customerId)
                .Select(item => new WarningDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    CustomerName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    Description = item.Description,
                    WarningDate = item.WarningDate,
                })
                .ToListAsync();

            return warnings;
        }

        /// <summary>
        /// Method performs updating fiels of the model.
        /// It also assign EditDateTime to now
        /// </summary>
        /// <param name="id">Id of the model in the database to be updated</param>
        /// <param name="modelDto">Updated model in the Base DTO format</param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateItem(int id, BaseWaringDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingWarning = await _context.Warnings.FindAsync(id);
            if (existingWarning == null) return new NotFoundResult();

            existingWarning.CustomerId = modelDto.CustomerId;
            existingWarning.Description = modelDto.Description;
            existingWarning.WarningDate = modelDto.WarningDate;
            existingWarning.EditDateTime = DateTime.Now;

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

    }
}
