namespace CarRentalManagementAPI.Models.DTOs.Customer
{
    public class CustomerSelectorDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = default!;
    }
}
