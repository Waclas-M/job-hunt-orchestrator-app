using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserEducation
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public string SchoolName { get; set; } = default!;
        public string StudyProfile { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
