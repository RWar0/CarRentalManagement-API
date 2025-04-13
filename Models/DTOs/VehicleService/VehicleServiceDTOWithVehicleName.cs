namespace CarRentalManagementAPI.Models.DTOs.VehicleService
{
    public class VehicleServiceDTOWithVehicleName : VehicleServiceDTO
    {
        public string VehicleName { get; set; } = default!;
    }
}
