namespace CarRentalManagementAPI.Models.DTOs.Fueling
{
    public class BaseFuelingDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateOnly FuelingDate { get; set; }

        public decimal Quantity { get; set; }
        public decimal? Price { get; set; }

    }
}
