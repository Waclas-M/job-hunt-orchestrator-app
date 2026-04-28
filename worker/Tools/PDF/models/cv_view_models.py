from contextlib import nullcontext
from dataclasses import dataclass, field
from typing import List, Optional


@dataclass
class CvHeader:
    full_name: str = ""
    title: str = ""
    summary: str = ""
    photo: bytes = None


@dataclass
class CvLanguage:
    name: str
    level_dots: int = 0
    max_dots: int = 10


@dataclass
class CvEducationItem:
    date_range: str
    school: str
    details: str


@dataclass
class CvExperienceItem:
    date_range: str
    company: str
    role: str
    bullets: List[str] = field(default_factory=list)


@dataclass
class CvInterestItem:
    label: str
    description: str = ""

@dataclass
class CvProjectsItem:
    name: str
    description: str
    date_range: str
    technologies: List[str]
    link: str




@dataclass
class CvSidebar:
    photo_path: str = ""
    address_label: str = "ADRES"
    contact_label: str = "KONTAKT"
    address_lines: List[str] = field(default_factory=list)
    contact_lines: List[str] = field(default_factory=list)
    skills: List[str] = field(default_factory=list)
    languages: List[CvLanguage] = field(default_factory=list)
    birthdate: Optional[str] = None


@dataclass
class CvMain:
    education: List[CvEducationItem] = field(default_factory=list)
    experience: List[CvExperienceItem] = field(default_factory=list)
    strengths_tags: List[str] = field(default_factory=list)
    interests: List[CvInterestItem] = field(default_factory=list)
    projects: List[CvProjectsItem] = field(default_factory=list)
    rodo_line: str = ""


@dataclass
class CvDocument:
    header: CvHeader = field(default_factory=CvHeader)
    sidebar: CvSidebar = field(default_factory=CvSidebar)
    main: CvMain = field(default_factory=CvMain)