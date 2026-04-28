namespace JHOP.Models.Dto.Experience
{
    public class EditUserExperienceDto
    {
        public int ProfileId { get; set; }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string JobTitle { get; set; }
        public bool IsCurrent { get; set; }
    }
}
