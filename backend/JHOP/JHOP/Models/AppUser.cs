using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHOP.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(150)")]
        public string SurName { get; set; }

        public ICollection<Profile> Profiles { get; set; } = new List<Profile>();

        [PersonalData]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(550)")]
        public string PersonalProfile { get; set; }

        [Column(TypeName = "nvarchar(550)")]
        public string LinkedInURL { get; set; }
        [Column(TypeName = "nvarchar(550)")]
        public string GitHubURL { get; set; }


    }
}
