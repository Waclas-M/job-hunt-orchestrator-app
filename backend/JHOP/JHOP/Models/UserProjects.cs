using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserProjects
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public string ProjectName { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string? ProjectURL { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public List<string> Technologies { get; set; } = new List<string>();

    }
}
