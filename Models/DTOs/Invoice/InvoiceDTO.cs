namespace CarRentalManagementAPI.Models.DTOs.Invoice
{
    public class InvoiceDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public int RentalId { get; set; }
        public decimal Price { get; set; }
        public DateOnly InvoiceDate { get; set; }
    }
}
