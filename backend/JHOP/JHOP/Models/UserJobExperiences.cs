using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserJobExperience
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public string CompanyName { get; set; } = default!;
        public string JobTitle { get; set; } = default!;
        public string JobDescription { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
