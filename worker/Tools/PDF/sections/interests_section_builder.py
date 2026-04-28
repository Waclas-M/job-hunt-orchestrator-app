from typing import Any, List

from reportlab.platypus import Paragraph, Spacer

from Tools.PDF.Cv_Helpers.section_helper import SectionHelper
from Tools.PDF.models.cv_view_models import CvInterestItem
from Tools.PDF.models.template_style_models import CVStyles


class InterestsSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, interests: List[CvInterestItem], avail_width: float) -> List[Any]:
        if not interests:
            return []

        interest_labels = self._extract_interest_labels(interests)
        if not interest_labels:
            return []

        flow: List[Any] = []

        flow.extend(
            SectionHelper.build_section_header(
                "ZAINTERESOWANIA",
                self.styles.section_title
            )
        )

        interests_line = " • ".join(interest_labels)
        flow.append(Paragraph(interests_line, self.styles.section_text))
        flow.append(Spacer(1, 6))

        return flow

    def _extract_interest_labels(self, interests: List[CvInterestItem]) -> List[str]:
        labels: List[str] = []
        seen = set()

        for item in interests:
            label = (item.label or "").strip()
            if not label:
                continue

            if label not in seen:
                labels.append(label)
                seen.add(label)

        return labels