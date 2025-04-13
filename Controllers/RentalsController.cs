using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagementAPI.Models;
using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs.Rental;
using CarRentalManagementAPI.Services;

namespace CarRentalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly RentalsService _service;

        public RentalsController(DatabaseContext context)
        {
            _service = new RentalsService(context);
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentals() => await _service.GetDtoList();

        // GET: api/Rentals/ActiveRentalIds
        [HttpGet("ActiveRentalIds")]
        public async Task<ActionResult<IEnumerable<object>>> GetActiveRentalIds() => await _service.GetDescendingActiveRentalIds();

        // GET: api/Rentals/CustomerRentals/5
        [HttpGet("CustomerRentals/{customerId}")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentalsOfCustomer(int customerId) => await _service.GetRentalsOfCustomer(customerId);

        // GET: api/Rentals/VehicleRentals/5
        [HttpGet("VehicleRentals/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<RentalDTO>>> GetRentalsOfVehicle(int vehicleId) => await _service.GetRentalsOfVehicle(vehicleId);

        // GET: api/Rentals/GetMaxId
        [HttpGet("GetMaxId")]
        public async Task<ActionResult<int>> GetMaxId() => await _service.GetMaximumIdOfInvoice();

        // GET: api/Rentals/DTO/5
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<BaseRentalDTO>> GetRentalDTO(int id) => await _service.GetBaseDtoItemById(id);

        // GET: api/Rentals/GetFullDTO/5
        [HttpGet("GetFullDTO/{id}")]
        public async Task<ActionResult<RentalDTO>> GetFullRentalDTO(int id) => await _service.GetDtoItemById(id);

        // GET: api/Rentals/Following/5
        [HttpGet("Following/{quantity}")]
        public async Task<ActionResult<IEnumerable<FollowingRentalDTO>>> GetFollowingRentals(int quantity) => await _service.GetFollowingRentals(quantity);

        // GET: api/Rentals/TopVehicle
        [HttpGet("TopVehicle")]
        public async Task<ActionResult<object>> GetTopVehicle() => await _service.GetTopVehicle();

        // GET: api/Rentals/TopCustomer
        [HttpGet("TopCustomer")]
        public async Task<ActionResult<object>> GetTopCustomer() => await _service.GetTopCustomer();

        // GET: api/Rentals/RentalChartData
        [HttpGet("RentalChartData")]
        public async Task<ActionResult<IEnumerable<object>>> GetRentalChartData() => await _service.GetRentalChartData();

        // GET: api/Rentals/FutureRentalsOfTheVehicle/5
        [HttpGet("FutureRentalsOfTheVehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetFutureRentalsDatesOfVehicle(int vehicleId) => await _service.GetFutureRentalsDatesOfVehicle(vehicleId);
        
        // GET: api/Rentals/FutureRentalsOfTheVehicle/2/5
        [HttpGet("GetFutureRentalsDatesOfVehicleWithoutSpecificOne/{rentalId}/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetFutureRentalsDatesOfVehicleWithoutSpecificOne(int rentalId, int vehicleId) => await _service.GetFutureRentalsDatesOfVehicleWithoutSpecificOne(rentalId, vehicleId);

        // PUT: api/Rentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutRental(int id, BaseRentalDTO rentalDTO) => await _service.UpdateItem(id, rentalDTO);

        // POST: api/Rentals/DTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("DTO")]
        public async Task<ActionResult<BaseRentalDTO>> PostRental(BaseRentalDTO rentalDTO) => await _service.CreateItem(rentalDTO);

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id) => await _service.DeleteItem(id);

    }
}
