using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    public class CustomersService : BaseService<Customer, CustomerDTO>
    {
        public CustomersService(DatabaseContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Method perform adding new item to the database
        /// </summary>
        /// <param name="modelDto">Model in the DTO format to be created in databaase</param>
        /// <returns></returns>
        public override async Task<ActionResult<CustomerDTO>> CreateItem(CustomerDTO modelDto)
        {
            Customer customer = new()
            {
                Firstname = modelDto.Firstname,
                Lastname = modelDto.Lastname,
                DateOfBirth = modelDto.DateOfBirth,
                PlaceOfBirth = modelDto.PlaceOfBirth,
                Pesel = modelDto.Pesel,

                IsActive = true,
                CreationDateTime = DateTime.Now,
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetCustomers", "Customers", new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of the item to be searched and returned</param>
        /// <returns>Item in the DTO format</returns>
        public override async Task<ActionResult<CustomerDTO>> GetDtoItemById(int id)
        {
            var item = await _context.Customers
                .Where(item => item.Id == id)
                .Select(item => new CustomerDTO()
                {
                    Id = item.Id,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Pesel = item.Pesel,
                    DateOfBirth = item.DateOfBirth,
                    PlaceOfBirth = item.PlaceOfBirth,
                })
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return new NotFoundResult();
            }

            return item;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public override async Task<ActionResult<IEnumerable<CustomerDTO>>> GetDtoList()
        {
            return await _context.Customers
                .Where(item => item.IsActive)
                .Select(item => new CustomerDTO()
                {
                    Id = item.Id,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Pesel = item.Pesel,
                    DateOfBirth = item.DateOfBirth,
                    PlaceOfBirth = item.PlaceOfBirth,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method returns List of items in specific format for selectors
        /// </summary>
        /// <returns>List of items in specific format for selectors</returns>
        public async Task<ActionResult<IEnumerable<CustomerSelectorDTO>>> GetCustomersSelectorList()
        {
            return await _context.Customers
                .Where(item => item.IsActive)
                .Select(item => new CustomerSelectorDTO()
                {
                    Id = item.Id,
                    CustomerName = $"{item.Firstname} {item.Lastname}",
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
        public override async Task<IActionResult> UpdateItem(int id, CustomerDTO modelDto)
        {
            if(!IsCorrectRequest(id, modelDto)) return new BadRequestResult();


            Customer? existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null) return new NotFoundResult();

            existingCustomer.Firstname = modelDto.Firstname;
            existingCustomer.Lastname = modelDto.Lastname;
            existingCustomer.DateOfBirth = modelDto.DateOfBirth;
            existingCustomer.PlaceOfBirth = modelDto.PlaceOfBirth;
            existingCustomer.Pesel = modelDto.Pesel;
            existingCustomer.EditDateTime = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (existingCustomer == null)
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
