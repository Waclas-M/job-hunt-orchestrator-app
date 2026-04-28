using JHOP.Models;
using JHOP.Models.Dto.Education;
using JHOP.Models.Dto.Experience;
using JHOP.Models.Dto.Interest;
using JHOP.Models.Dto.Language;
using JHOP.Models.Dto.Skill;
using JHOP.Models.Dto.Strenght;

namespace JHOP.ReadModels.Cv
{
    public class CvReadModelFactory
    {
       
            public CvReadModel Build(
                AppUser user,
                List<UserEducationDto> educations,
                List<UserExperienceDto> jobs,
                List<UserLanguagesDto> userLanguages,
                List<UserStrengsDto> userStrengs,
                List<UserSkillsDto> userSkills,
                List<UserIntrestsDto> userIntrests

                )
            {
                return new CvReadModel
                {
                    Header = new HeaderReadModel
                    {
                        FullName = user.Name + " " + user.SurName,
                        Title = user.Title,
                        Summary = user.PersonalProfile
                    },
                    Main = new MainReadModel
                    {
                        Educations = educations,
                        JobExperiences = jobs,
                        Strengs = userStrengs,
                        Intrests = userIntrests
                    },
                    Sidebar = new SidebarReadModel
                    {
                        GithubUrl = user.LinkedInURL,
                        LinkedInUrl = user.GitHubURL,
                        PhotoPath = "/images/profile.jpg",
                        Email = user.Email,
                        Phone = user.PhoneNumber,
                        languages = userLanguages,
                        skills = userSkills
                    }
                };
            }
        }
}
