namespace CarRentalManagementAPI.Models.DTOs.Rental
{
    public class FollowingRentalDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string CustomerFullName { get; set; } = default!;
        public string VehicleName { get; set; } = default!;
        public DateOnly BeginDate { get; set; }
    }
}
