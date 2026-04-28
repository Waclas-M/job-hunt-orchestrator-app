using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserSkills
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public string Skill { get; set; } = default!;

    }
}
