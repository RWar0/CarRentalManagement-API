namespace CarRentalManagementAPI.Models.DTOs.VehicleService
{
    public class VehicleServiceDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int VehicleId { get; set; }
        public DateOnly ServiceDate { get; set; }
    }
}
