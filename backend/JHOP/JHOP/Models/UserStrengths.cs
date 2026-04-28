using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserStrengths
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public string Strength { get; set; } = default!;
    }
}
