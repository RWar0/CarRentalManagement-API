using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Customer;
using CarRentalManagementAPI.Models.DTOs.Deposit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class DepositsService : BaseService<Deposit, DepositDTO>
    {

        public enum DepositStatus
        {
            Kept, Returned
        }

        public DepositsService(DatabaseContext dbContext) : base(dbContext)
        {
            
        }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<DepositDTO>> CreateItem(DepositDTO modelDto)
        {
            Deposit deposit = new()
            {
                RentalId = modelDto.RentalId,
                Price = modelDto.Price,
                Status = modelDto.Status,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetDeposits", "Deposits", new { id = deposit.Id }, deposit);
        }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public async Task<ActionResult<BaseDepositDTO>> CreateItem(BaseDepositDTO modelDto)
        {
            Deposit deposit = new()
            {
                RentalId = modelDto.RentalId,
                Price = modelDto.Price,
                Status = modelDto.Status,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetDeposits", "Deposits", new { id = deposit.Id }, deposit);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<DepositDTO>> GetDtoItemById(int id)
        {
            var deposit = await _context.Deposits
                .Include(item => item.Rental)
                    .ThenInclude(item => item.Customer)
                    .Include(item => item.Rental.Vehicle)
                .Where(item => item.Id == id)
                .Select(item => new DepositDTO()
                {
                    Id = item.Id,
                    RentalId = item.RentalId,
                    BeginDate = item.Rental.BeginDate,
                    EndDate = item.Rental.EndDate,
                    CustomerId = item.Rental.CustomerId,
                    CustomerName = $"{item.Rental.Customer.Firstname} {item.Rental.Customer.Lastname}",
                    VehicleId = item.Rental.VehicleId,
                    VehicleName = $"{item.Rental.Vehicle.Brand} {item.Rental.Vehicle.Model}",
                    Price = item.Price,
                    Status = item.Status,
                })
                .FirstOrDefaultAsync();

            if (deposit == null) return new NotFoundResult();

            return deposit;
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public async Task<ActionResult<BaseDepositDTO>> GetBaseDtoItemById(int id)
        {
            var deposit = await _context.Deposits
                .Include(item => item.Rental)
                    .ThenInclude(item => item.Customer)
                    .Include(item => item.Rental.Vehicle)
                .Where(item => item.Id == id)
                .Select(item => new BaseDepositDTO()
                {
                    Id = item.Id,
                    RentalId = item.RentalId,
                    Price = item.Price,
                    Status = item.Status,
                })
                .FirstOrDefaultAsync();

            if (deposit == null) return new NotFoundResult();

            return deposit;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<DepositDTO>>> GetDtoList()
        {
            return await _context.Deposits
                .Include(item => item.Rental)
                    .ThenInclude(item => item.Customer)
                    .Include(item => item.Rental.Vehicle)
                .Where(item => item.IsActive)
                .Select(item => new DepositDTO()
                {
                    Id = item.Id,
                    RentalId = item.RentalId,
                    BeginDate = item.Rental.BeginDate,
                    EndDate = item.Rental.EndDate,
                    CustomerId = item.Rental.CustomerId,
                    CustomerName = $"{item.Rental.Customer.Firstname} {item.Rental.Customer.Lastname}",
                    VehicleId = item.Rental.VehicleId,
                    VehicleName = $"{item.Rental.Vehicle.Brand} {item.Rental.Vehicle.Model}",
                    Price = item.Price,
                    Status = item.Status,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of active items (deposits that was not already finalized) in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public async Task<ActionResult<IEnumerable<DepositDTO>>> GetActiveItemsDtoList()
        {
            return await _context.Deposits
                    .Include(item => item.Rental)
                        .ThenInclude(item => item.Customer)
                        .Include(item => item.Rental.Vehicle)
                    .Where(item => item.IsActive)
                    .Where(item => item.Status.Equals("Active"))
                    .Select(item => new DepositDTO()
                    {
                        Id = item.Id,
                        RentalId = item.RentalId,
                        BeginDate = item.Rental.BeginDate,
                        EndDate = item.Rental.EndDate,
                        CustomerId = item.Rental.CustomerId,
                        CustomerName = $"{item.Rental.Customer.Firstname} {item.Rental.Customer.Lastname}",
                        VehicleId = item.Rental.VehicleId,
                        VehicleName = $"{item.Rental.Vehicle.Brand} {item.Rental.Vehicle.Model}",
                        Price = item.Price,
                        Status = item.Status,
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
        public override async Task<IActionResult> UpdateItem(int id, DepositDTO modelDto)
        {
            if (id != modelDto.Id) return new BadRequestResult();

            var existingDeposit = await _context.Deposits.FindAsync(id);

            if (existingDeposit == null) return new NotFoundResult();

            existingDeposit.Price = modelDto.Price;
            existingDeposit.Status = modelDto.Status;

            existingDeposit.EditDateTime = DateTime.Now;

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
        /// Method performs updating fiels of the model.
        /// It also assign EditDateTime to now
        /// </summary>
        /// <param name="id">Id of the model in the database to be updated</param>
        /// <param name="modelDto">Updated model in the DTO format</param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateItem(int id, BaseDepositDTO modelDto)
        {
            if (id != modelDto.Id) return new BadRequestResult();

            var existingDeposit = await _context.Deposits.FindAsync(id);

            if (existingDeposit == null) return new NotFoundResult();

            existingDeposit.Price = modelDto.Price;
            existingDeposit.Status = modelDto.Status;

            existingDeposit.EditDateTime = DateTime.Now;

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
        /// Method sets deposit status to desired
        /// </summary>
        /// <param name="id">Id of the deposit to change the status</param>
        /// <param name="status">Status to be assigned</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeDepositStatus(int id, DepositStatus status)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return new NotFoundResult();
            }

            switch(status)
            {
                case DepositStatus.Returned:
                    deposit.Status = "Returned";
                    break;
                case DepositStatus.Kept:
                    deposit.Status = "Kept";
                    break;
                default:
                    deposit.Status = "Active";
                    break;
            }

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

        /// <summary>
        /// Method returns List of Specific Rental Desposits in DTO format
        /// </summary>
        /// <param name="rentalId">Id of rental which Deposits will be found</param>
        /// <returns>List of Specific Rental Desposits in DTO format</returns>
        public async Task<ActionResult<IEnumerable<DepositDTO>>> GetDepositsOfSpecificRental(int rentalId)
        {
            return await _context.Deposits
                .Include(item => item.Rental)
                    .ThenInclude(item => item.Customer)
                    .Include(item => item.Rental.Vehicle)
                .Where(item => item.IsActive)
                .Where(item => item.RentalId == rentalId)
                .Select(item => new DepositDTO()
                {
                    Id = item.Id,
                    RentalId = item.RentalId,
                    BeginDate = item.Rental.BeginDate,
                    EndDate = item.Rental.EndDate,
                    CustomerId = item.Rental.CustomerId,
                    CustomerName = $"{item.Rental.Customer.Firstname} {item.Rental.Customer.Lastname}",
                    VehicleId = item.Rental.VehicleId,
                    VehicleName = $"{item.Rental.Vehicle.Brand} {item.Rental.Vehicle.Model}",
                    Price = item.Price,
                    Status = item.Status,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }
    }
}
