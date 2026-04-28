using JHOP.Validators;

namespace JHOP.Models.Dto
{
    public class GenerateCvModel
    {
        public string OfferURL { get; set; }
        public int CvForm { get; set; }
        public int ProfileId { get; set; }

        public string? CompanyName { get; set; }

        public bool UserEducationsProcessAuto { get; set; } // 0 - Auto, 1 - Manual, 2 - None
        public bool UserExperiencesProcessAuto { get; set; } // 0 - Auto, 1 - Manual, 2 - None
        public bool UserStrengsProcessAuto { get; set; } // 0 - Auto, 1 - Manual, 2 - NoneNone
        public bool UserSkillsProcessAuto { get; set; } // 0 - Auto, 1 - Manual, 2 - NoneNone

        [RequiredIfTrueAttribute("UserEducationsProcessAuto",
        ErrorMessage = "UserEducationsId is required when process type is not Auto.")]
        public List<int>? UserEducationsIds { get; set; }

        [RequiredIfTrueAttribute("UserExperiencesProcessAuto",
        ErrorMessage = "UserExperiencesId is required when process type is not Auto.")]
        public List<int>? UserExperiencesIds { get; set; }

        [RequiredIfTrueAttribute("UserStrengsProcessAuto",
        ErrorMessage = "UserStrengsId is required when process type is not Auto.")]
        public List<int>? UserStrengsIds { get; set; }


        [RequiredIfTrueAttribute("UserSkillsProcessAuto",
        ErrorMessage = "UserSkillsId is required when process type is not Auto.")]
        public List<int>? UserSkillsIds { get; set; }
       



    }
}
