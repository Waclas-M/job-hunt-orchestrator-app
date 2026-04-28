using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHOP.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; } = default!;

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = default!;

        public byte ProfileIndex { get; set; }
        public string Name { get; set; } = default!;
        public UserProfilePersonalData? PersonalData { get; set; }
        public ICollection<UserEducation> Educations { get; set; } = new List<UserEducation>();
        public ICollection<UserJobExperience> JobExperiences { get; set; } = new List<UserJobExperience>();
        public ICollection<UserLanguages> Languages { get; set; } = new List<UserLanguages>();
        public ICollection<UserInterests> Interests { get; set; } = new List<UserInterests>();
        public ICollection<UserSkills> Skills { get; set; } = new List<UserSkills>();
        public ICollection<UserStrengths> Strengths { get; set; } = new List<UserStrengths>();

        public ICollection<UserProjects> Projects { get; set; } = new List<UserProjects>();

        public ProfilePhoto? ProfilePhoto { get; set; }
   

    }
}
