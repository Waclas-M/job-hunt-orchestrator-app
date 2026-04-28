from typing import Any, Dict, List

from reportlab.lib import colors
from reportlab.lib.enums import TA_CENTER
from reportlab.lib.pagesizes import A4
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet
from reportlab.platypus import (
    Frame,
    FrameBreak,
    KeepInFrame,
    Paragraph,
    Spacer,
    Table,
    TableStyle,
)

from Tools.PDF.Cv_Helpers.font_helper import FontHelper
from Tools.PDF.sections.education_section_builder import EducationSectionBuilder
from Tools.PDF.sections.experience_section_builder import ExperienceSectionBuilder
from Tools.PDF.sections.footer_section_builder import FooterSectionBuilder
from Tools.PDF.sections.interests_section_builder import InterestsSectionBuilder
from Tools.PDF.templates.i_cv_template import ICvTemplate
from Tools.PDF.models.cv_view_models import CvDocument

class ProfessionalNavyCvTemplate(ICvTemplate):
    def __init__(self,cv_document):
        self.font, self.font_bold = FontHelper.register_fonts()
        self.palette = self._build_palette()
        self.styles = self._build_styles()
        self.cv_documnet = cv_document
        self.education_builder = EducationSectionBuilder(self.styles)
        self.experience_builder = ExperienceSectionBuilder(self.styles)
        self.interests_builder = InterestsSectionBuilder(self.styles)
        self.footer_builder = FooterSectionBuilder(self.styles)

    def get_document_config(self) -> Dict[str, Any]:
        return {
            "pagesize": A4,
            "leftMargin": 12,
            "rightMargin": 12,
            "topMargin": 12,
            "bottomMargin": 12,
            "title": "CV",
            "author": "CV Generator",
        }

    def get_frames(self, page_width: float, page_height: float) -> List[Any]:
        left_margin = 12
        right_margin = 12
        top_margin = 12
        bottom_margin = 12

        usable_width = page_width - left_margin - right_margin
        usable_height = page_height - top_margin - bottom_margin

        sidebar_width = usable_width * 0.38
        main_width = usable_width - sidebar_width

        sidebar_frame = Frame(
            x1=left_margin,
            y1=bottom_margin,
            width=sidebar_width,
            height=usable_height,
            leftPadding=12,
            rightPadding=12,
            topPadding=14,
            bottomPadding=14,
            id="sidebar",
            showBoundary=0,
        )

        main_frame = Frame(
            x1=left_margin + sidebar_width,
            y1=bottom_margin,
            width=main_width,
            height=usable_height,
            leftPadding=16,
            rightPadding=8,
            topPadding=14,
            bottomPadding=14,
            id="main",
            showBoundary=0,
        )

        return [sidebar_frame, main_frame]

    def build_story(self, cv_document: CvDocument, page_width: float, page_height: float) -> List[Any]:
        story: List[Any] = []

        sidebar_flow = self._build_sidebar(cv_document)
        story.append(
            KeepInFrame(
                maxWidth=220,
                maxHeight=760,
                content=sidebar_flow,
                mode="shrink",
                hAlign="LEFT",
                vAlign="TOP",
            )
        )

        story.append(FrameBreak())
        story.extend(self._build_main(cv_document))

        return story

    def _build_sidebar(self, cv_document: CvDocument) -> List[Any]:
        flow: List[Any] = []

        flow.extend(self._build_sidebar_photo(cv_document))
        flow.extend(self._build_sidebar_identity(cv_document))
        flow.extend(self._build_sidebar_contact(cv_document))
        flow.extend(self._build_sidebar_skills(cv_document))
        flow.extend(self._build_sidebar_languages(cv_document))
        flow.extend(self._build_sidebar_strengths(cv_document))

        return flow

    def _build_sidebar_photo(self, cv_document: CvDocument) -> List[Any]:
        sidebar = cv_document.sidebar

        if sidebar.photo_path:
            try:
                from reportlab.platypus import Image

                image = Image(sidebar.photo_path, width=160, height=160)
                return [image, Spacer(1, 10)]
            except Exception:
                pass

        placeholder = Table([[""]], colWidths=[160], rowHeights=[160])
        placeholder.setStyle(TableStyle([
            ("BACKGROUND", (0, 0), (-1, -1), colors.HexColor("#E8EEF3")),
        ]))

        return [placeholder, Spacer(1, 10)]

    def _build_sidebar_identity(self, cv_document: CvDocument) -> List[Any]:
        flow: List[Any] = []

        if cv_document.header.full_name:
            flow.append(Paragraph(cv_document.header.full_name, self.styles["Name"]))

        if cv_document.header.title:
            flow.append(Paragraph(cv_document.header.title, self.styles["Role"]))

        flow.append(Spacer(1, 14))
        return flow

    def _build_sidebar_contact(self, cv_document: CvDocument) -> List[Any]:
        elements: List[Any] = []

        for line in cv_document.sidebar.contact_lines:
            elements.append(Paragraph(self._safe_sidebar_text(line), self.styles["SideText"]))

        return self._side_section("Kontakt", elements)

    def _build_sidebar_skills(self, cv_document: CvDocument) -> List[Any]:
        elements: List[Any] = []

        for skill in cv_document.sidebar.skills:
            elements.append(Paragraph(f"• {self._safe_sidebar_text(skill)}", self.styles["SideText"]))

        return self._side_section("Umiejętności", elements)

    def _build_sidebar_languages(self, cv_document: CvDocument) -> List[Any]:
        elements: List[Any] = []

        for language in cv_document.sidebar.languages:
            elements.append(
                Paragraph(
                    f"• {self._safe_sidebar_text(language.name)} ({language.level_dots}/10)",
                    self.styles["SideText"]
                )
            )

        return self._side_section("Języki", elements)

    def _build_sidebar_strengths(self, cv_document: CvDocument) -> List[Any]:
        elements: List[Any] = []

        for tag in cv_document.main.strengths_tags:
            elements.append(Paragraph(f"• {self._safe_sidebar_text(tag)}", self.styles["SideText"]))

        return self._side_section("Atuty", elements)

    def _side_section(self, title: str, elements: List[Any]) -> List[Any]:
        if not elements:
            return []

        flow: List[Any] = [
            Paragraph(title.upper(), self.styles["SideSection"]),
            Spacer(1, 5),
        ]

        flow.extend(elements)
        flow.append(Spacer(1, 10))

        return flow

    def _build_main(self, cv_document: CvDocument) -> List[Any]:
        flow: List[Any] = []

        if cv_document.header.summary:
            flow.append(Paragraph("Profil zawodowy", self.styles["MainSection"]))
            flow.append(Paragraph(cv_document.header.summary, self.styles["Body"]))
            flow.append(Spacer(1, 12))

        experience_flow = self.experience_builder.build(cv_document.main.experience)
        if experience_flow:
            flow.extend(experience_flow)

        education_flow = self.education_builder.build(cv_document.main.education)
        if education_flow:
            flow.extend(education_flow)

        interests_flow = self.interests_builder.build(cv_document.main.interests, 320)
        if interests_flow:
            flow.extend(interests_flow)

        footer_flow = self.footer_builder.build(cv_document.main.rodo_line)
        if footer_flow:
            flow.extend(footer_flow)

        return flow

    def _build_palette(self) -> Dict[str, Any]:
        return {
            "sidebar_bg": colors.HexColor("#1F3A4D"),
            "accent": colors.HexColor("#2F6B8A"),
            "soft": colors.HexColor("#EEF4F7"),
            "body": colors.HexColor("#1F2933"),
            "muted": colors.HexColor("#5B6871"),
            "white": colors.white,
            "side_text": colors.HexColor("#DDE7EE"),
        }

    def _build_styles(self):
        styles = getSampleStyleSheet()

        styles.add(ParagraphStyle(
            name="Name",
            fontName=self.font_bold,
            fontSize=22,
            leading=26,
            textColor=self.palette["white"],
            alignment=TA_CENTER,
            spaceAfter=2,
        ))

        styles.add(ParagraphStyle(
            name="Role",
            fontName=self.font,
            fontSize=10.5,
            leading=14,
            textColor=self.palette["side_text"],
            alignment=TA_CENTER,
            spaceAfter=0,
        ))

        styles.add(ParagraphStyle(
            name="SideSection",
            fontName=self.font_bold,
            fontSize=10.5,
            leading=12,
            textColor=self.palette["white"],
            spaceAfter=2,
        ))

        styles.add(ParagraphStyle(
            name="SideText",
            fontName=self.font,
            fontSize=8.7,
            leading=11.2,
            textColor=self.palette["white"],
            spaceAfter=1,
        ))

        styles.add(ParagraphStyle(
            name="MainSection",
            fontName=self.font_bold,
            fontSize=12,
            leading=14,
            textColor=self.palette["accent"],
            spaceAfter=4,
        ))

        styles.add(ParagraphStyle(
            name="Body",
            fontName=self.font,
            fontSize=9.6,
            leading=14,
            textColor=self.palette["body"],
        ))

        styles.add(ParagraphStyle(
            name="BodyMuted",
            fontName=self.font,
            fontSize=9.2,
            leading=13,
            textColor=self.palette["muted"],
        ))

        styles.add(ParagraphStyle(
            name="BodyBold",
            fontName=self.font_bold,
            fontSize=10.2,
            leading=13,
            textColor=self.palette["body"],
        ))

        styles.add(ParagraphStyle(
            name="SmallCaps",
            fontName=self.font_bold,
            fontSize=8.8,
            leading=11,
            textColor=self.palette["muted"],
        ))

        return styles

    def _safe_sidebar_text(self, value: str, max_len: int = 42) -> str:
        if not value:
            return ""

        value = value.strip()
        if len(value) <= max_len:
            return value

        return value[:max_len - 3] + "..."