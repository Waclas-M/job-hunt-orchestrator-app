from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.mappers.language_mapper import LanguageMapper
from Tools.PDF.models.cv_view_models import CvSidebar
from Tools.PDF.models.raw_cv_models import RawCvInput


class SidebarMapper:
    def __init__(self):
        self.language_mapper = LanguageMapper()

    def map(self, raw_input: RawCvInput) -> CvSidebar:
        personal_data = raw_input.PersonalData

        skills = TextHelper.unique_non_empty([item.Skill for item in raw_input.Skills])
        languages = self.language_mapper.map(raw_input.Languages)

        contact_lines = []

        if personal_data:
            email = TextHelper.safe_strip(personal_data.Email)
            phone = TextHelper.safe_strip(personal_data.PhoneNumber)
            github = TextHelper.safe_url(personal_data.GitHubURL)
            linkedin = TextHelper.safe_url(personal_data.LinkedInURL)

            contact_lines = TextHelper.unique_non_empty([
                email,
                phone,
                github,
                linkedin,
            ])

        return CvSidebar(
            photo_path="/app/assets/staticImage/MARCIN_WECLAS_20240507.jpg",
            address_lines=[],
            contact_lines=contact_lines,
            skills=skills,
            languages=languages,
            birthdate=None,
        )