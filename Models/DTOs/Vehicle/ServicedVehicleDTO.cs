namespace CarRentalManagementAPI.Models.DTOs.Vehicle
{
    public class ServicedVehicleDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = default!;
        public int VehicleProduction { get; set; }
        public DateOnly ServiceDate { get; set; }
    }
}
