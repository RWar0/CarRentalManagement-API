namespace CarRentalManagementAPI.Models.DTOs.Vehicle
{
    public class VehicleSelectorDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string VehicleName { get; set; } = default!;
        public int Production { get; set; }
    }
}
