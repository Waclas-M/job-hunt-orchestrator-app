using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JHOP.Models
{
    public class UserProfilePersonalData
    {

        [Key]
        public int ProfileId { get; set; }
        [JsonIgnore]
        public Profile Profile { get; set; } = default!;

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? PersonalProfile { get; set; } 
        public string? GitHubURL { get; set; } 
        public string? LinkedInURL { get; set; }
    }
}
