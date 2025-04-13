using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class ServicesService : BaseService<Service, ServiceDTO>
    {
        public ServicesService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<ServiceDTO>> CreateItem(ServiceDTO modelDto)
        {
            Service service = new()
            {
                Title = modelDto.Title,
                Description = string.IsNullOrEmpty(modelDto.Description) ? null : modelDto.Description,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetServices", "Services", new { id = service.Id }, service);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<ServiceDTO>> GetDtoItemById(int id)
        {
            var service = await _context.Services
                .Where(item => item.Id == id)
                .Select(item => new ServiceDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                })
                .FirstOrDefaultAsync();

            if (service == null)
            {
                return new NotFoundResult();
            }

            return service;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<ServiceDTO>>> GetDtoList()
        {
            return await _context.Services
                .Where(item => item.IsActive)
                .Select(item => new ServiceDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of Specific Vehicle Services in DTO format
        /// </summary>
        /// <param name="vehicleId">Id of Vehicle which Services will be found</param>
        /// <returns>List of Specific Vehicle Services in DTO format</returns>
        public async Task<ActionResult<IEnumerable<ServiceOfVehicleDTO>>> GetServicesOfSpecificVehicle(int vehicleId)
        {
            return await _context.VehicleServices
                .Include(item => item.Service)
                .Where(item => item.IsActive)
                .Where(item => item.VehicleId == vehicleId)
                .Select(item => new ServiceOfVehicleDTO()
                {
                    Id = item.Id,
                    ServiceId = item.ServiceId,
                    Title = item.Service.Title,
                    Description = item.Service.Description,
                    ServiceDate = item.ServiceDate,
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
        public override async Task<IActionResult> UpdateItem(int id, ServiceDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingService = await _context.Services.FindAsync(id);
            if (existingService == null) return new NotFoundResult();

            existingService.Title = modelDto.Title;
            existingService.Description = string.IsNullOrEmpty(modelDto.Description) ? null : modelDto.Description;

            existingService.EditDateTime = DateTime.Now;

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
