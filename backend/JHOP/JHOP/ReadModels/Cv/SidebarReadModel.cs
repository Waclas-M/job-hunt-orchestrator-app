using JHOP.Models.Dto.Language;
using JHOP.Models.Dto.Skill;

namespace JHOP.ReadModels.Cv
{
    public class SidebarReadModel
    {
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string GithubUrl { get; set; }
        public string LinkedInUrl { get; set; }

        public List<UserLanguagesDto> languages { get; set; }
        public List<UserSkillsDto> skills { get; set; }




    }
}
