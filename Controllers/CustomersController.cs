using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Customer;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _service;
        public CustomersController(DatabaseContext context)
        {
            _service = new CustomersService(context);
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers() => await _service.GetDtoList();

        // GET: api/Customers/SelectorList
        [HttpGet("SelectorList")]
        public async Task<ActionResult<IEnumerable<CustomerSelectorDTO>>> GetCustomersSelectorList() => await _service.GetCustomersSelectorList();

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id) => await _service.GetDtoItemById(id);

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO) => await _service.UpdateItem(id, customerDTO);

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO) => await _service.CreateItem(customerDTO);
            

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id) => await _service.DeleteItem(id);
    }
}
