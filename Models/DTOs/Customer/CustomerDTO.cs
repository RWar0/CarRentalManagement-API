namespace CarRentalManagementAPI.Models.DTOs.Customer
{
    public class CustomerDTO : IBaseDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Pesel { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; } = default!;
    }
}
