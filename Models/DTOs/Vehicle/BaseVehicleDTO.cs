namespace CarRentalManagementAPI.Models.DTOs.Vehicle
{
    public class BaseVehicleDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string Model { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public int Production { get; set; }
        public string? Color { get; set; }
        public int VehicleCategyId { get; set; }
    }
}
