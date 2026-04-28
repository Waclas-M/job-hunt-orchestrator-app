from typing import Any, Dict

from Tools.PDF.mappers.header_mapper import HeaderMapper
from Tools.PDF.mappers.main_mapper import MainMapper
from Tools.PDF.mappers.sidebar_mapper import SidebarMapper
from Tools.PDF.models.cv_view_models import CvDocument
from Tools.PDF.models.raw_cv_models import (
    RawCvInput,
    RawEducation,
    RawExperience,
    RawInterest,
    RawLanguage,
    RawPersonalData,
    RawSkill,
    RawStrength,
    RawProjects
)


class CvDataMapper:
    def __init__(self):
        self.header_mapper = HeaderMapper()
        self.sidebar_mapper = SidebarMapper()
        self.main_mapper = MainMapper()

    def map_from_dict(self, raw_data: Dict[str, Any]) -> CvDocument:
        raw_input = self._build_raw_input(raw_data)
        return self.map(raw_input)

    def map(self, raw_input: RawCvInput) -> CvDocument:
        return CvDocument(
            header=self.header_mapper.map(raw_input),
            sidebar=self.sidebar_mapper.map(raw_input),
            main=self.main_mapper.map(raw_input),
        )

    def _build_raw_input(self, raw_data: Dict[str, Any]) -> RawCvInput:
        personal_data_dict = raw_data.get("PersonalData")
        personal_data = RawPersonalData(**personal_data_dict) if personal_data_dict else None

        educations = [
            RawEducation(**item)
            for item in raw_data.get("Education", [])
        ]

        experiences = [
            RawExperience(**item)
            for item in raw_data.get("Experience", [])
        ]

        strengths = [
            RawStrength(**item)
            for item in raw_data.get("Strengs", [])
        ]

        skills = [
            RawSkill(**item)
            for item in raw_data.get("Skills", [])
        ]

        languages = [
            RawLanguage(**item)
            for item in raw_data.get("Languages", [])
        ]

        interests = [
            RawInterest(**item)
            for item in raw_data.get("Interests", [])
        ]

        projects = [
            RawProjects(**item)
            for item in raw_data.get("Projects",[])
        ]

        return RawCvInput(
            UserId=raw_data.get("UserId", ""),
            OfferURL=raw_data.get("OfferURL", ""),
            CvForm=raw_data.get("CvForm", 0),
            PersonalData=personal_data,
            UserEducationsProcessAuto=raw_data.get("UserEducationsProcessAuto", False),
            UserExperiencesProcessAuto=raw_data.get("UserExperiencesProcessAuto", False),
            UserStrengsProcessAuto=raw_data.get("UserStrengsProcessAuto", False),
            UserSkillsProcessAuto=raw_data.get("UserSkillsProcessAuto", False),
            Education=educations,
            Experience=experiences,
            Strengs=strengths,
            Skills=skills,
            Languages=languages,
            Interests=interests,
            Projects=projects,
            Photo=raw_data.get('Photo')
        )