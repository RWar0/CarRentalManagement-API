using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Insurance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class InsurancesService : BaseService<Insurance, InsuranceDTO>
    {

        public InsurancesService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<InsuranceDTO>> CreateItem(InsuranceDTO modelDto)
        {
            var insurance = new Insurance()
            {
                VehicleId = modelDto.VehicleId,
                BeginDate = modelDto.BeginDate,
                EndDate = modelDto.EndDate,
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Insurances.Add(insurance);

            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetInsurances", "Insurances", new { id = insurance.Id }, insurance);
        }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the Base DTO format to be created in databaase</param>
        /// <returns></returns>
        public async Task<ActionResult<InsuranceDTO>> CreateItem(BaseInsuranceDTO modelDto)
        {
            var insurance = new Insurance()
            {
                VehicleId = modelDto.VehicleId,
                BeginDate = modelDto.BeginDate,
                EndDate = modelDto.EndDate,
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Insurances.Add(insurance);

            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetInsurances", "Insurances", new { id = insurance.Id }, insurance);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<InsuranceDTO>> GetDtoItemById(int id)
        {
            var insurance = await _context.Insurances
                .Where(item => item.Id == id)
                .Select(item => new InsuranceDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate
                })
                .FirstOrDefaultAsync();

            if (insurance == null)
            {
                return new NotFoundResult();
            }

            return insurance;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<InsuranceDTO>>> GetDtoList()
        {
            return await _context.Insurances
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Select(item => new InsuranceDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate
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
        public override async Task<IActionResult> UpdateItem(int id, InsuranceDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingInsurance = await _context.Insurances.FindAsync(id);
            if (existingInsurance == null)
            {
                return new NotFoundResult();
            }

            existingInsurance.VehicleId = modelDto.VehicleId;
            existingInsurance.BeginDate = modelDto.BeginDate;
            existingInsurance.EndDate = modelDto.EndDate;
            existingInsurance.EditDateTime = DateTime.Now;

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
        /// Method performs updating fiels of the Base DTO model.
        /// It also assign EditDateTime to now
        /// </summary>
        /// <param name="id">Id of the model in the database to be updated</param>
        /// <param name="modelDto">Updated model in the Base DTO format</param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateItem(int id, BaseInsuranceDTO modelDto)
        {
            if (id != modelDto.Id)
            {
                return new BadRequestResult();
            }

            var existingInsurance = await _context.Insurances.FindAsync(id);
            if (existingInsurance == null)
            {
                return new NotFoundResult();
            }

            existingInsurance.VehicleId = modelDto.VehicleId;
            existingInsurance.BeginDate = modelDto.BeginDate;
            existingInsurance.EndDate = modelDto.EndDate;
            existingInsurance.EditDateTime = DateTime.Now;

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
        /// Method returns List of Specific Vehicle Insurances in DTO format
        /// </summary>
        /// <param name="vehicleId">Id of Vehicle which Insurances will be found</param>
        /// <returns>List of Specific Vehicle Insurances in DTO format</returns>
        public async Task<ActionResult<IEnumerable<InsuranceDTO>>> GetInsurancesOfSpecificVehicle(int vehicleId)
        {
            return await _context.Insurances
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Where(item => item.VehicleId == vehicleId)
                .Select(item => new InsuranceDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }
    }
}
