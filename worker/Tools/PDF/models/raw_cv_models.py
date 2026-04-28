from dataclasses import dataclass, field
from typing import List, Optional


@dataclass
class RawPersonalData:
    ProfileId: int
    FirstName: Optional[str]
    LastName: Optional[str]
    Email: Optional[str]
    PhoneNumber: Optional[str]
    PersonalProfile: Optional[str]
    GitHubURL: Optional[str]
    LinkedInURL: Optional[str]


@dataclass
class RawEducation:
    ProfileId: int
    SchoolName: str
    StudyProfile: str
    StartDate: Optional[str]
    EndDate: Optional[str]
    IsCurrent: bool


@dataclass
class RawExperience:
    ProfileId: int
    CompanyName: str
    JobDescription: str
    StartDate: Optional[str]
    EndDate: Optional[str]
    JobTitle: str
    IsCurrent: bool


@dataclass
class RawStrength:
    ProfileId: int
    Strength: str


@dataclass
class RawSkill:
    ProfileId: int
    Skill: str


@dataclass
class RawLanguage:
    ProfileId: int
    Language: str
    Level: Optional[str]


@dataclass
class RawInterest:
    ProfileId: int
    Interest: str
    Description: Optional[str]

@dataclass
class RawProjects:
    ProfileId: int
    Name: str
    Description: str
    Link: str
    StartDate: str
    EndDate: str
    Technologies: List[str]
    IsCurrent: bool = False


@dataclass
class RawCvInput:
    UserId: str
    OfferURL: str
    CvForm: int = 0
    PersonalData: Optional[RawPersonalData] = None
    UserEducationsProcessAuto: bool = False
    UserExperiencesProcessAuto: bool = False
    UserStrengsProcessAuto: bool = False
    UserSkillsProcessAuto: bool = False
    Education: List[RawEducation] = field(default_factory=list)
    Experience: List[RawExperience] = field(default_factory=list)
    Strengs: List[RawStrength] = field(default_factory=list)
    Skills: List[RawSkill] = field(default_factory=list)
    Languages: List[RawLanguage] = field(default_factory=list)
    Interests: List[RawInterest] = field(default_factory=list)
    Projects: List[RawProjects] = field(default_factory=list)
    Photo : bytes = ''