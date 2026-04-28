from typing import Any, List

from reportlab.platypus import Paragraph, Spacer

from Tools.PDF.models.template_style_models import CVStyles


class FooterSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, rodo_line: str) -> List[Any]:
        if not rodo_line:
            return []

        return [
            Spacer(1, 8),
            Paragraph(rodo_line, self.styles.rodo)
        ]