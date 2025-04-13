namespace CarRentalManagementAPI.Models.DTOs.Rental
{
    public class RentalDTO : BaseRentalDTO
    {
        public string CustomerName { get; set; } = default!;
        public string VehicleName { get; set; } = default!;
    }
}
