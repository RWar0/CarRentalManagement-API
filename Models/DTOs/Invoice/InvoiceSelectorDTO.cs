namespace CarRentalManagementAPI.Models.DTOs.Invoice
{
    public class InvoiceSelectorDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
    }
}
