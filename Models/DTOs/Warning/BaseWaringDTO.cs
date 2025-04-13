namespace CarRentalManagementAPI.Models.DTOs.Warning
{
    public class BaseWaringDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; } = default!;
        public DateOnly WarningDate { get; set; }
    }
}
