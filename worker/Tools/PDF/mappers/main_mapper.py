from Tools.PDF.models.cv_view_models import CvMain
from Tools.PDF.models.raw_cv_models import RawCvInput
from Tools.PDF.mappers.education_mapper import EducationMapper
from Tools.PDF.mappers.experience_mapper import ExperienceMapper
from Tools.PDF.mappers.interest_mapper import InterestMapper
from Tools.PDF.mappers.strengths_mapper import StrengthsMapper
from  Tools.PDF.mappers.projects_mapper import ProjectsMapper


class MainMapper:
    DEFAULT_RODO = (
        "Wyrażam zgodę na przetwarzanie moich danych osobowych dla potrzeb "
        "niezbędnych do realizacji procesu rekrutacji zgodnie z obowiązującymi przepisami prawa."
    )

    def __init__(self):
        self.education_mapper = EducationMapper()
        self.experience_mapper = ExperienceMapper()
        self.strengths_mapper = StrengthsMapper()
        self.interest_mapper = InterestMapper()
        self.project_mapper = ProjectsMapper()

    def map(self, raw_input: RawCvInput) -> CvMain:
        return CvMain(
            education=self.education_mapper.map(raw_input.Education),
            experience=self.experience_mapper.map(raw_input.Experience),
            strengths_tags=self.strengths_mapper.map(raw_input.Strengs),
            interests=self.interest_mapper.map(raw_input.Interests),
            projects= self.project_mapper.map(raw_input.Projects),
            rodo_line=self.DEFAULT_RODO,
        )