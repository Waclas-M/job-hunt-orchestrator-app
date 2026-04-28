from typing import Any, List
from reportlab.lib import colors
from reportlab.platypus import Image
import os
from reportlab.platypus import Paragraph, Spacer, Table, TableStyle

from Tools.PDF.Cv_Helpers.custom_flowables import DotsLevel
from Tools.PDF.Cv_Helpers.image_helper import ImageHelper
from Tools.PDF.models.cv_view_models import CvSidebar
from Tools.PDF.models.template_style_models import CVStyles


class SidebarSectionBuilder:
    def __init__(self, styles: CVStyles):
        self.styles = styles

    def build(self, sidebar: CvSidebar, sidebar_width: float) -> List[Any]:
        flow: List[Any] = []

        flow.extend(self._build_photo(sidebar, sidebar_width))
        flow.extend(self._build_contact_block(sidebar, sidebar_width))
        flow.extend(self._build_skills_languages_block(sidebar, sidebar_width))

        return flow

    def _build_photo(self, sidebar: CvSidebar, sidebar_width: float):
        print("\n\nJestem w build photo \n\n")
        if sidebar.photo_path and os.path.exists(sidebar.photo_path):
            print("PHOTO FOUND!")
        if sidebar.photo_path and os.path.exists(sidebar.photo_path):
            try:
                img = Image(sidebar.photo_path)

                # dopasowanie do sidebaru
                img.drawWidth = sidebar_width
                img.drawHeight = sidebar_width * 0.8

                return [img]
            except Exception as e:
                print("Image error:", e)

        return [ImageHelper.placeholder_box(sidebar_width, sidebar_width * 0.8)]

    def _build_contact_block(self, sidebar: CvSidebar, sidebar_width: float):
        rows = []

        if sidebar.contact_lines:
            rows.append([Paragraph("KONTAKT", self.styles.sidebar_label)])
            for line in sidebar.contact_lines:
                rows.append([Paragraph(line, self.styles.sidebar_text)])
            rows.append([Spacer(1, 3)])  # 👈 mniejsze

        table = Table(rows, colWidths=[sidebar_width])
        table.setStyle(TableStyle([
            ("BACKGROUND", (0, 0), (-1, -1), colors.HexColor("#1f7a1f")),
            ("LEFTPADDING", (0, 0), (-1, -1), 10),
            ("RIGHTPADDING", (0, 0), (-1, -1), 10),
            ("TOPPADDING", (0, 0), (-1, -1), 8),
            ("BOTTOMPADDING", (0, 0), (-1, -1), 8),
        ]))

        return [table]

    def _build_skills_languages_block(self, sidebar: CvSidebar, sidebar_width: float):
        rows = []

        rows.append([Paragraph("UMIEJĘTNOŚCI", self.styles.sidebar_label)])
        rows.append([Spacer(1, 3)])

        for skill in sidebar.skills:
            rows.append([Paragraph(f"• {skill}", self.styles.sidebar_text)])

        if sidebar.languages:
            rows.append([Spacer(1, 6)])
            rows.append([Paragraph("JĘZYKI", self.styles.sidebar_label)])
            rows.append([Spacer(1, 3)])

            for lang in sidebar.languages:
                rows.append([
                    Paragraph(lang.name, self.styles.sidebar_text)
                ])

        table = Table(rows, colWidths=[sidebar_width])
        table.setStyle(TableStyle([
            ("BACKGROUND", (0, 0), (-1, -1), colors.HexColor("#2f2f2f")),
            ("LEFTPADDING", (0, 0), (-1, -1), 10),
            ("RIGHTPADDING", (0, 0), (-1, -1), 10),
            ("TOPPADDING", (0, 0), (-1, -1), 8),
            ("BOTTOMPADDING", (0, 0), (-1, -1), 8),
        ]))

        return [table]