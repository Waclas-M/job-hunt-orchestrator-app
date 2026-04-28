namespace JHOP.Models.Dto.Projects
{
    public class UserEditProjectsDto
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string ProjectName { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string? ProjectURL { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();
    }
}
