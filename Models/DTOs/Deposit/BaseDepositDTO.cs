namespace CarRentalManagementAPI.Models.DTOs.Deposit
{
    public class BaseDepositDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = default!;
    }
}
