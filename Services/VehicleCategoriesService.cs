using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.VehicleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class VehicleCategoriesService : BaseService<VehicleCategy, VehicleCategoryDTO>
    {
        public VehicleCategoriesService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<VehicleCategoryDTO>> CreateItem(VehicleCategoryDTO modelDto)
        {
            VehicleCategy vehicleCategy = new()
            {
                Title = modelDto.Title,
                IsActive = true,
                CreationDateTime = DateTime.UtcNow
            };

            _context.VehicleCategies.Add(vehicleCategy);

            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetVehicleCategy", "VehicleCategies", new { id = vehicleCategy.Id }, vehicleCategy);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<VehicleCategoryDTO>> GetDtoItemById(int id)
        {
            var vehicleCategy = await _context.VehicleCategies
                .Where(item => item.Id == id)
                .Select(item => new VehicleCategoryDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                })
                .FirstOrDefaultAsync();

            if (vehicleCategy == null)
            {
                return new NotFoundResult();
            }

            return vehicleCategy;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<VehicleCategoryDTO>>> GetDtoList()
        {
            return await _context.VehicleCategies
                .Where(item => item.IsActive)
                .Select(item => new VehicleCategoryDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
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
        public override async Task<IActionResult> UpdateItem(int id, VehicleCategoryDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            VehicleCategy? existingCategory = await _context.VehicleCategies.FindAsync(id);
            if (existingCategory == null) return new NotFoundResult();

            existingCategory.Title = modelDto.Title;
            existingCategory.EditDateTime = DateTime.Now;

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
