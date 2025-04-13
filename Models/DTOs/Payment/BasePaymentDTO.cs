namespace CarRentalManagementAPI.Models.DTOs.Payment
{
    public class BasePaymentDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal PaymentTotal { get; set; }
        public bool IsFinalized { get; set; }
        public DateOnly? FinalizationDate { get; set; }
    }
}
