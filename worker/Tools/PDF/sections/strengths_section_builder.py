from typing import Any, List

from reportlab.lib import colors
from reportlab.platypus import Paragraph, Spacer, Table, TableStyle, KeepInFrame

from Tools.PDF.Cv_Helpers.section_helper import SectionHelper
from Tools.PDF.models.template_style_models import CVStyles


class StrengthsSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, tags: List[str], avail_width: float) -> List[Any]:
        if not tags:
            return []

        flow: List[Any] = []
        flow.extend(SectionHelper.build_section_header("ATUTY", self.styles.section_title))

        tag_flow = []

        for tag in tags:
            cell = Table([[Paragraph(tag, self.styles.tag)]], colWidths=[avail_width])
            cell.setStyle(TableStyle([
                ("BACKGROUND", (0, 0), (-1, -1), colors.HexColor("#2f2f2f")),
                ("LEFTPADDING", (0, 0), (-1, -1), 6),
                ("RIGHTPADDING", (0, 0), (-1, -1), 6),
                ("TOPPADDING", (0, 0), (-1, -1), 4),
                ("BOTTOMPADDING", (0, 0), (-1, -1), 4),
            ]))

            tag_flow.append(cell)
            tag_flow.append(Spacer(1, 4))

        flow.append(
            KeepInFrame(
                maxWidth=avail_width,
                maxHeight=160,
                content=tag_flow,
                mode="shrink",
            )
        )

        return flow