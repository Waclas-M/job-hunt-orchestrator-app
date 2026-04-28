from typing import Any, List

from reportlab.platypus import Paragraph, Spacer

from Tools.PDF.Cv_Helpers.section_helper import SectionHelper
from Tools.PDF.models.cv_view_models import CvEducationItem
from Tools.PDF.models.template_style_models import CVStyles


class EducationSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, education_items: List[CvEducationItem]) -> List[Any]:
        if not education_items:
            return []

        flow: List[Any] = []
        flow.extend(SectionHelper.build_section_header("WYKSZTAŁCENIE", self.styles.section_title))

        for index, item in enumerate(education_items):
            if item.date_range:
                flow.append(Paragraph(item.date_range, self.styles.small))

            if item.school:
                flow.append(Paragraph(item.school, self.styles.item_title_green))

            if item.details:
                flow.append(Paragraph(item.details, self.styles.section_text))

            if index < len(education_items) - 1:
                flow.append(Spacer(1, 5))

        flow.append(Spacer(1, 4))
        return flow