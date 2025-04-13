namespace CarRentalManagementAPI.Models.DTOs.VehicleCategory
{
    public class VehicleCategoryDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
    }
}
