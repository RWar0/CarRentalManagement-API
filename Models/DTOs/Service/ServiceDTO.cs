namespace CarRentalManagementAPI.Models.DTOs.Service
{
    public class ServiceDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
    }
}
