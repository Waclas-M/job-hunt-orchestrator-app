from typing import Any, List

from reportlab.platypus import Paragraph

from Tools.PDF.models.cv_view_models import CvHeader
from Tools.PDF.models.template_style_models import CVStyles


class HeaderSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, header: CvHeader) -> List[Any]:
        flow: List[Any] = []

        if header.full_name:
            flow.append(Paragraph(header.full_name, self.styles.h_name))

        if header.title:
            flow.append(Paragraph(header.title, self.styles.h_title))

        summary = self._normalize_summary(header.summary)
        if summary:
            flow.append(Paragraph(summary, self.styles.summary))

        return flow

    def _normalize_summary(self, summary: str, max_len: int = 650) -> str:
        if not summary:
            return ""

        summary = summary.strip()
        if len(summary) <= max_len:
            return summary

        return summary[:max_len - 3].rstrip() + "..."