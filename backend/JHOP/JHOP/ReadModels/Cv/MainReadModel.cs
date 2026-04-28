using JHOP.Models;
using JHOP.Models.Dto.Education;
using JHOP.Models.Dto.Experience;
using JHOP.Models.Dto.Interest;
using JHOP.Models.Dto.Strenght;

namespace JHOP.ReadModels.Cv
{
    public class MainReadModel
    {
        public List<UserEducationDto> Educations { get; set; }
        public List<UserExperienceDto> JobExperiences { get; set; }

        public List<UserStrengsDto> Strengs { get; set; }
        public List<UserIntrestsDto> Intrests { get; set; }
        
    }
}
