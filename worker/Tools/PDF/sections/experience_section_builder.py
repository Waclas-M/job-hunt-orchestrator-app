from typing import Any, List

from reportlab.platypus import Paragraph, Spacer

from Tools.PDF.Cv_Helpers.section_helper import SectionHelper
from Tools.PDF.models.cv_view_models import CvExperienceItem
from Tools.PDF.models.template_style_models import CVStyles


class ExperienceSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, experience_items: List[CvExperienceItem]) -> List[Any]:
        if not experience_items:
            return []

        flow: List[Any] = []
        flow.extend(SectionHelper.build_section_header("DOŚWIADCZENIE", self.styles.section_title))

        for index, item in enumerate(experience_items):
            if item.date_range:
                flow.append(Paragraph(item.date_range, self.styles.small))

            if item.company:
                flow.append(Paragraph(item.company, self.styles.item_title_green))

            if item.role:
                flow.append(Paragraph(item.role, self.styles.item_role))

            for bullet in item.bullets:
                flow.append(Paragraph(bullet, self.styles.bullet, bulletText="–"))

            if index < len(experience_items) - 1:
                flow.append(Spacer(1, 6))

        flow.append(Spacer(1, 4))
        return flow