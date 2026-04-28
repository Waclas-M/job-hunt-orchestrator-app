namespace JHOP.Models.Dto
{
    public class ProfilePersonalDataDto
    {
        public int ProfileId { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

    }
}
