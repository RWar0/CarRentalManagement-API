namespace CarRentalManagementAPI.Models.DTOs.Deposit
{
    public class DepositDTO : BaseDepositDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = default!;

        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
