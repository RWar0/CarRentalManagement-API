using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.VehicleService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class VehicleServicesService : BaseService<VehicleService, VehicleServiceDTO>
    {
        public VehicleServicesService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<VehicleServiceDTO>> CreateItem(VehicleServiceDTO modelDto)
        {
            VehicleService vehicleService = new()
            {
                VehicleId = modelDto.VehicleId,
                ServiceId = modelDto.ServiceId,
                ServiceDate = modelDto.ServiceDate,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.VehicleServices.Add(vehicleService);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetVehicleServices", "VehicleServices", new { id = vehicleService.Id }, vehicleService);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<VehicleServiceDTO>> GetDtoItemById(int id)
        {
            var vehicleService = await _context.VehicleServices
                .Where(item => item.Id == id)
                .Select(item => new VehicleServiceDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    ServiceId = item.ServiceId,
                    ServiceDate = item.ServiceDate,
                })
                .FirstOrDefaultAsync();

            if (vehicleService == null)
            {
                return new NotFoundResult();
            }

            return vehicleService;
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format with VehicleName
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format with VehicleName</returns>
        public async Task<ActionResult<VehicleServiceDTOWithVehicleName>> GetVehicleServiceDTOWithVehicleName(int id)
        {
            var vehicleService = await _context.VehicleServices
                .Include(item => item.Vehicle)
                .Where(item => item.Id == id)
                .Select(item => new VehicleServiceDTOWithVehicleName()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    ServiceId = item.ServiceId,
                    ServiceDate = item.ServiceDate,
                })
                .FirstOrDefaultAsync();

            if (vehicleService == null)
            {
                return new NotFoundResult();
            }

            return vehicleService;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<VehicleServiceDTO>>> GetDtoList()
        {
            return await _context.VehicleServices
                .Where(item => item.IsActive)
                .Select(item => new VehicleServiceDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    ServiceId = item.ServiceId,
                    ServiceDate = item.ServiceDate,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method performs updating fiels of the model.
        /// It also assign EditDateTime to now
        /// </summary>
        /// <param name="id">Id of the model in the database to be updated</param>
        /// <param name="modelDto">Updated model in the DTO format</param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateItem(int id, VehicleServiceDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var vehicleService = await _context.VehicleServices.FindAsync(id);
            if (vehicleService == null) return new NotFoundResult();

            vehicleService.VehicleId = modelDto.VehicleId;
            vehicleService.ServiceId = modelDto.ServiceId;
            vehicleService.ServiceDate = modelDto.ServiceDate;
            vehicleService.EditDateTime = DateTime.Now;

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
