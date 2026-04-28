import os
from typing import Any, List, Optional

from reportlab.lib import colors
from reportlab.platypus import HRFlowable, Image, Paragraph, Spacer, Table, TableStyle


class SectionHelper:
    @staticmethod
    def build_section_header(title: str, title_style: Any, icon_path: Optional[str] = None) -> List[Any]:
        row = []

        if icon_path and os.path.exists(icon_path):
            try:
                row.append(Image(icon_path, width=10, height=10))
            except Exception:
                row.append(Spacer(10, 10))
        else:
            row.append(Spacer(10, 10))

        row.append(Paragraph(title, title_style))

        header_table = Table([row], colWidths=[12, None], hAlign="LEFT")
        header_table.setStyle(TableStyle([
            ("VALIGN", (0, 0), (-1, -1), "MIDDLE"),
            ("LEFTPADDING", (0, 0), (-1, -1), 0),
            ("RIGHTPADDING", (0, 0), (-1, -1), 0),
            ("TOPPADDING", (0, 0), (-1, -1), 0),
            ("BOTTOMPADDING", (0, 0), (-1, -1), 0),
        ]))

        line = HRFlowable(
            width="100%",
            thickness=0.8,
            color=colors.HexColor("#222222"),
            spaceBefore=0,
            spaceAfter=8,
        )

        return [header_table, line]