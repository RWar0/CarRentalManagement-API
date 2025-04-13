namespace CarRentalManagementAPI.Models.DTOs.Payment
{
    public class PaymentDTO : BasePaymentDTO
    {
        public string InvoiceTitle { get; set; } = default!;
    }
}