using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class RentalsService : BaseService<Rental, RentalDTO>
    {
        public RentalsService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<RentalDTO>> CreateItem(RentalDTO modelDto)
        {
            Rental rental = new()
            {
                VehicleId = modelDto.VehicleId,
                CustomerId = modelDto.CustomerId,
                BeginDate = modelDto.BeginDate,
                EndDate = modelDto.EndDate,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetRentals", "Rentals", new { id = rental.Id }, rental);
        }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public async Task<ActionResult<BaseRentalDTO>> CreateItem(BaseRentalDTO modelDto)
        {
            Rental rental = new()
            {
                VehicleId = modelDto.VehicleId,
                CustomerId = modelDto.CustomerId,
                BeginDate = modelDto.BeginDate,
                EndDate = modelDto.EndDate,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetRentals", "Rentals", new { id = rental.Id }, rental);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<RentalDTO>> GetDtoItemById(int id)
        {
            RentalDTO? rental = await _context.Rentals
                .Include(item => item.Customer)
                .Include(item => item.Vehicle)
                .Where(item => item.Id == id)
                .Select(item => new RentalDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    CustomerName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                })
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                return new NotFoundResult();
            }

            return rental;
        }

        /// <summary>
        /// Method returns Item with specific Id in Base DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the Base DTO format</returns>
        public async Task<ActionResult<BaseRentalDTO>> GetBaseDtoItemById(int id)
        {
            BaseRentalDTO? rental = await _context.Rentals
                .Where(item => item.Id == id)
                .Select(item => new BaseRentalDTO()
                {
                    Id = item.Id,
                    VehicleId = item.VehicleId,
                    CustomerId = item.CustomerId,
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                })
                .FirstOrDefaultAsync();

            if (rental == null)
            {
                return new NotFoundResult();
            }

            return rental;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<RentalDTO>>> GetDtoList()
        {
            return await _context.Rentals
                .Include(item => item.Customer)
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Select(item => new RentalDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    CustomerName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }
        /// <summary>
        /// Method return List of active rentals Ids in descending order
        /// </summary>
        /// <returns>List of active rentals Ids in descending order</returns>
        public async Task<ActionResult<IEnumerable<object>>> GetDescendingActiveRentalIds()
        {
            return await _context.Rentals
                .Where(item => item.IsActive)
                .Select(item => new {
                    item.Id
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of Specific Customer rentals
        /// </summary>
        /// <param name="customerId">Id of Customer which Rentals will be found</param>
        /// <returns>List of Specific Customer rentals</returns>
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentalsOfCustomer(int customerId)
        {
            return await _context.Rentals
                .Include(item => item.Customer)
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Where(item => item.CustomerId == customerId)
                .Select(item => new RentalDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    CustomerName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
                })
                .OrderByDescending(item => item.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of Specific Vehicle rentals
        /// </summary>
        /// <param name="vehicleId">Id of Vehicle which Rentals will be found</param>
        /// <returns>List of Specific Vehicle rentals</returns>
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentalsOfVehicle(int vehicleId)
        {
            return await _context.Rentals
                .Include(item => item.Customer)
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Where(item => item.VehicleId == vehicleId)
                .Select(item => new RentalDTO()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    CustomerName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    VehicleId = item.VehicleId,
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate,
                    EndDate = item.EndDate,
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
        public override async Task<IActionResult> UpdateItem(int id, RentalDTO modelDto)
        {
            if (!IsCorrectRequest(id, modelDto)) return new BadRequestResult();

            var existingRental = await _context.Rentals.FindAsync(id);
            if (existingRental == null) return new NotFoundResult();

            existingRental.VehicleId = modelDto.VehicleId;
            existingRental.CustomerId = modelDto.CustomerId;
            existingRental.BeginDate = modelDto.BeginDate;
            existingRental.EndDate = modelDto.EndDate;

            existingRental.EditDateTime = DateTime.Now;

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
        /// <param name="modelDto">Updated model in the Base DTO format</param>
        public async Task<IActionResult> UpdateItem(int id, BaseRentalDTO modelDto)
        {
            if (id != modelDto.Id) return new BadRequestResult();

            var existingRental = await _context.Rentals.FindAsync(id);
            if (existingRental == null) return new NotFoundResult();

            existingRental.VehicleId = modelDto.VehicleId;
            existingRental.CustomerId = modelDto.CustomerId;
            existingRental.BeginDate = modelDto.BeginDate;
            existingRental.EndDate = modelDto.EndDate;

            existingRental.EditDateTime = DateTime.Now;

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
        /// Method search and get the Id of last created invoice (maximum assigned Id)
        /// </summary>
        /// <returns>Id of last created invoice (maximum assigned Id)</returns>
        public async Task<ActionResult<int>> GetMaximumIdOfInvoice()
        {
            return await _context.Rentals.MaxAsync(r => r.Id);
        }

        /// <summary>
        /// Method search and returns specific quantity of following rentals in FollowingRentalDTO format
        /// </summary>
        /// <param name="quantity">Quantity of the records to be found and returned</param>
        /// <returns>Specific quantity of following rentals in FollowingRentalDTO format</returns>
        public async Task<ActionResult<IEnumerable<FollowingRentalDTO>>> GetFollowingRentals(int quantity)
        {
            DateOnly currentDay = DateOnly.FromDateTime(DateTime.Now);

            return await _context.Rentals
                .Include(item => item.Vehicle)
                .Include(item => item.Customer)
                .Where(item => item.IsActive)
                .Where(item => item.BeginDate > currentDay)
                .OrderBy(item => item.BeginDate)
                .Take(quantity)
                .Select(item => new FollowingRentalDTO()
                {
                    Id = item.Id,
                    CustomerFullName = $"{item.Customer.Firstname} {item.Customer.Lastname}",
                    VehicleName = $"{item.Vehicle.Brand} {item.Vehicle.Model}",
                    BeginDate = item.BeginDate
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method search for the vehicle in database that was the most commmonly rented (highest rental quantity)
        /// </summary>
        /// <returns>Vehicle with the highest rental quantity as OBJECT in format: VehicleName and RentalQuantity</returns>
        public async Task<ActionResult<object>> GetTopVehicle()
        {
            int currentYear = DateTime.Now.Year;
            var topVehicle = await _context.Rentals
                .Include(item => item.Vehicle)
                .Where(item => item.IsActive)
                .Where(item => item.BeginDate.Year == currentYear)
                .GroupBy(item => new { item.Vehicle.Brand, item.Vehicle.Model })
                .Select(item => new
                {
                    VehicleName = $"{item.Key.Brand} {item.Key.Model}",
                    RentalQuantity = item.Count()
                })
                .OrderByDescending(x => x.RentalQuantity)
                .FirstOrDefaultAsync();

            if (topVehicle == null)
            {
                return new NotFoundResult();
            }

            return topVehicle;
        }

        /// <summary>
        /// Method search for the customer in database with the highest quantity of rentals
        /// </summary>
        /// <returns>Customer with the highest quantity of rental as OBJECT in format: CustomerName and RentalQuantity</returns>
        public async Task<ActionResult<object>> GetTopCustomer()
        {
            int currentYear = DateTime.Now.Year;
            var topCustomer = await _context.Rentals
                .Include(item => item.Customer)
                .Where(item => item.IsActive)
                .Where(item => item.BeginDate.Year == currentYear)
                .GroupBy(item => new { item.Customer.Firstname, item.Customer.Lastname })
                .Select(item => new
                {
                    CustomerName = $"{item.Key.Firstname} {item.Key.Lastname}",
                    RentalQuantity = item.Count()
                })
                .OrderByDescending(x => x.RentalQuantity)
                .FirstOrDefaultAsync();

            if (topCustomer == null)
            {
                return new NotFoundResult();
            }

            return topCustomer;
        }

        /// <summary>
        /// Method gets from the database quantity of rentals for each month and returns as object with 12 sets of (Month, Quantity) for all months in current Year.
        /// (BeginDate is equal for specific month, EndDate is not taken into account)
        /// </summary>
        /// <returns>Object with 12 sets of (Month, Quantity) for all months in current year. Each month has assigned quantity of rentals during this period.
        /// (It counts when BeginDate is assigned for specific one)</returns>
        public async Task<ActionResult<IEnumerable<object>>> GetRentalChartData()
        {
            int currentYear = DateTime.Now.Year;

            return await _context.Rentals
                .Where(item => item.IsActive)
                .Where(item => item.BeginDate.Year == currentYear)
                .GroupBy(item => item.BeginDate.Month)
                .Select(item => new
                {
                    Month = item.Key,
                    Quantity = item.Count()
                })
                .OrderBy(data => data.Month)
                .ToListAsync();
        }


        /// <summary>
        /// Method search for reservations od Desired Vehicle in database and returns Begin and End Dates of the reservations in future
        /// </summary>
        /// <param name="vehicleId">Id of the vehicle that will be searched for future reservations</param>
        /// <returns>Begin and End Dates of the reservations of the Desired Vehicle in future</returns>
        public async Task<ActionResult<IEnumerable<object>>> GetFutureRentalsDatesOfVehicle(int vehicleId)
        {
            DateOnly filterDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-14));

            return await _context.Rentals
                .Where(item => item.IsActive)
                .Where(item => item.VehicleId == vehicleId)
                .Where(item => item.BeginDate > filterDate)
                .Select(item => new
                {
                    item.BeginDate,
                    item.EndDate,
                }).
                ToListAsync();
        }

        /// <summary>
        /// This method is dedicated for editing existing rentals.
        /// Method search for reservations od Desired Vehicle in database and returns Begin and End Dates of the reservations in future.
        /// This method will ommit rental with entered ID.
        /// </summary>
        /// <param name="rentalId"></param>
        /// <param name="vehicleId">Id of the vehicle that will be searched for future reservations</param>
        /// <returns>Begin and End Dates of the reservations of the Desired Vehicle in future whitout reservation with entered Rental ID</returns>
        public async Task<ActionResult<IEnumerable<object>>> GetFutureRentalsDatesOfVehicleWithoutSpecificOne(int rentalId, int vehicleId)
        {
            DateOnly filterDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-14));

            return await _context.Rentals
                .Where(item => item.IsActive)
                .Where(item => item.Id != rentalId)
                .Where(item => item.VehicleId == vehicleId)
                .Where(item => item.BeginDate > filterDate)
                .Select(item => new
                {
                    item.BeginDate,
                    item.EndDate,
                }).
                ToListAsync();
        }
    }
}
