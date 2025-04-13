using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Fueling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class FuelingsService : BaseService<Fueling, BaseFuelingDTO>
    {
        public FuelingsService(DatabaseContext dbContext) : base(dbContext)
        {
            
        }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<BaseFuelingDTO>> CreateItem(BaseFuelingDTO modelDto)
        {
            Fueling fueling = new()
            {
                VehicleId = modelDto.VehicleId,
                FuelingDate = modelDto.FuelingDate,
                Quantity = modelDto.Quantity,
                Price = (string.IsNullOrEmpty(modelDto.Price + "") || modelDto.Price == -1M) ? null : modelDto.Price,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Fuelings.Add(fueling);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetFuelings", "Fuelings", new { id = fueling.Id }, fueling);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<BaseFuelingDTO>> GetDtoItemById(int id)
        {
            var fueling = await _context.Fuelings
                .Where(item => item.Id == id)
                .Select(item => new BaseFuelingDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    FuelingDate = item.FuelingDate,
                    Quantity = item.Quantity,
                    Price = item.Price,
                })
                .FirstOrDefaultAsync();

            if (fueling == null)
            {
                return new NotFoundResult();
            }

            return fueling;
        }

        /// <summary>
        /// Method returns List of items in Base DTO format
        /// </summary>
        /// <returns>List of items in Base DTO format</returns>
        public override async Task<ActionResult<IEnumerable<BaseFuelingDTO>>> GetDtoList()
        {
            return await _context.Fuelings
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Select(item => new BaseFuelingDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    FuelingDate = item.FuelingDate,
                    Quantity = item.Quantity,
                    Price = item.Price,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of items in Full DTO format
        /// </summary>
        /// <returns>List of items in Full DTO format</returns>
        public async Task<ActionResult<IEnumerable<FuelingDTO>>> GetFullDtoList()
        {
            return await _context.Fuelings
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Select(item => new FuelingDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    FuelingDate = item.FuelingDate,
                    Quantity = item.Quantity,
                    Price = item.Price,
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
        public override async Task<IActionResult> UpdateItem(int id, BaseFuelingDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingFueling = await _context.Fuelings.FindAsync(id);

            if (existingFueling == null)
            {
                return new NotFoundResult();
            }

            existingFueling.VehicleId = modelDto.VehicleId;
            existingFueling.FuelingDate = modelDto.FuelingDate;
            existingFueling.Quantity = modelDto.Quantity;

            existingFueling.Price = (string.IsNullOrEmpty(modelDto.Price + "") || modelDto.Price == -1M) ? null : modelDto.Price;

            existingFueling.EditDateTime = DateTime.Now;

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
        /// Method returns List of Specific Vehicle Fuelings in Full DTO format
        /// </summary>
        /// <param name="vehicleId">Id of vehicle which Fuelings will be found</param>
        /// <returns>List of Specific Vehicle Fuelings in Full DTO format</returns>
        public async Task<ActionResult<IEnumerable<FuelingDTO>>> GetFuelingsOfSpecificVehicle(int vehicleId)
        {
            return await _context.Fuelings
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Where(item => item.VehicleId == vehicleId)
                .Select(item => new FuelingDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    FuelingDate = item.FuelingDate,
                    Quantity = item.Quantity,
                    Price = item.Price,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }
    }
}
