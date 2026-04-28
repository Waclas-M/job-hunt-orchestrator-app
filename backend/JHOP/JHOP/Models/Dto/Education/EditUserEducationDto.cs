namespace JHOP.Models.Dto.Education
{
    public class EditUserEducationDto
    {
        public int ProfileId { get; set; }
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string StudyProfile { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
