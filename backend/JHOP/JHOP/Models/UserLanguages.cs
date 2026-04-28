using JHOP.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserLanguages
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public int Language { get; set; } = default!; // np. "English"
        public string Level { get; set; } = default!;    // np. "
    }
}
