using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class VehiclesService : BaseService<Vehicle, BaseVehicleDTO>
    {
        public VehiclesService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<BaseVehicleDTO>> CreateItem(BaseVehicleDTO modelDto)
        {
            Vehicle vehicle = new()
            {
                Brand = modelDto.Brand,
                Model = modelDto.Model,
                Production = modelDto.Production,
                Color = modelDto.Color ?? "",
                VehicleCategyId = modelDto.VehicleCategyId,
                IsActive = true,
                CreationDateTime = DateTime.UtcNow
            };

            _context.Vehicles.Add(vehicle);
            
            await _context.SaveChangesAsync();
            return new CreatedAtActionResult("GetVehicles", "Vehicles", new { id = vehicle.Id }, vehicle);
        }

        /// <summary>
        /// Method returns Item with specific Id in Base DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the Base DTO format</returns>
        public override async Task<ActionResult<BaseVehicleDTO>> GetDtoItemById(int id)
        {
            var vehicle = await _context.Vehicles
                .Where(item => item.Id == id)
                .Select(item => new BaseVehicleDTO()
                {
                    Id = item.Id,
                    Model = item.Model,
                    Brand = item.Brand,
                    Production = item.Production,
                    Color = item.Color,
                    VehicleCategyId = item.VehicleCategyId
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                return new NotFoundResult();
            }

            return vehicle;
        }

        /// <summary>
        /// Method returns Item with specific Id in Full DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the Full DTO format</returns>
        public async Task<ActionResult<VehicleDTO>> GetFullDtoItemById(int id)
        {
            var vehicle = await _context.Vehicles
                .Where(item => item.Id == id)
                .Select(item => new VehicleDTO()
                {
                    Id = item.Id,
                    Model = item.Model,
                    Brand = item.Brand,
                    Production = item.Production,
                    Color = item.Color,
                    VehicleCategyId = item.VehicleCategy.Id,
                    CategoryName = item.VehicleCategy.Title,

                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                return new NotFoundResult();
            }

            return vehicle;
        }

        /// <summary>
        /// Method returns List of items in Base DTO format (without categoryName)
        /// </summary>
        /// <returns>List of items in Base DTO format (without categoryName)</returns>
        public override async Task<ActionResult<IEnumerable<BaseVehicleDTO>>> GetDtoList()
        {
            return await _context.Vehicles
                .Where(item => item.IsActive)
                .Include(item => item.VehicleCategy)
                .Select(item => new BaseVehicleDTO()
                {
                    Id = item.Id,
                    Model = item.Model,
                    Brand = item.Brand,
                    Production = item.Production,
                    Color = item.Color,
                    VehicleCategyId = item.VehicleCategyId,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of items in Vehicle Selector DTO format (Id, VehicleName, Production)
        /// </summary>
        /// <returns>List of items in Vehicle Selector DTO format(Id, VehicleName, Production)</returns>
        public async Task<ActionResult<IEnumerable<VehicleSelectorDTO>>> GetVehiclesSelectorList()
        {
            return await _context.Vehicles
                .Where(item => item.IsActive)
                .Include(item => item.VehicleCategy)
                .Select(item => new VehicleSelectorDTO()
                {
                    Id = item.Id,
                    VehicleName = $"{item.Brand} {item.Model}",
                    Production = item.Production,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of items in Full DTO format (with CategoryName)
        /// </summary>
        /// <returns>List of items in Full DTO format (with CategoryName)</returns>
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetFullDtoList()
        {
            return await _context.Vehicles
                .Where(item => item.IsActive)
                .Include(item => item.VehicleCategy)
                .Select(item => new VehicleDTO()
                {
                    Id = item.Id,
                    Model = item.Model,
                    Brand = item.Brand,
                    Production = item.Production,
                    Color = item.Color,
                    VehicleCategyId = item.VehicleCategy.Id,
                    CategoryName = item.VehicleCategy.Title,
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
        public override async Task<IActionResult> UpdateItem(int id, BaseVehicleDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            Vehicle? existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle == null) return new NotFoundResult();

            existingVehicle.Brand = modelDto.Brand;
            existingVehicle.Model = modelDto.Model;
            existingVehicle.Production = modelDto.Production;
            existingVehicle.Production = modelDto.Production;
            existingVehicle.VehicleCategyId = modelDto.VehicleCategyId;
            existingVehicle.Color = modelDto.Color ?? "";

            existingVehicle.EditDateTime = DateTime.Now;

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
        /// Method returns List of Vehicles that was sevices with specific serviceId
        /// </summary>
        /// <param name="serviceId">Id of service which services vehicles will be found</param>
        /// <returns>List of Vehicles that was sevices with specific serviceId</returns>
        public async Task<ActionResult<IEnumerable<ServicedVehicleDTO>>> GetVehiclesServicesBySpecificOne(int serviceId)
        {
            return await _context.VehicleServices
                .Where(item => item.IsActive)
                .Where(item => item.ServiceId == serviceId)
                .Include(item => item.Vehicle)
                .Select(item => new ServicedVehicleDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    VehicleProduction = item.Vehicle.Production,
                    ServiceDate = item.ServiceDate
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }
    }
}
