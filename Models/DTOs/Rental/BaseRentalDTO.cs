namespace CarRentalManagementAPI.Models.DTOs.Rental
{
    public class BaseRentalDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
