namespace CarRentalManagementAPI.Models.DTOs.Service
{
    public class ServiceOfVehicleDTO : ServiceDTO
    {
        public int ServiceId { get; set; }
        public DateOnly ServiceDate { get; set; }
    }
}
