from reportlab.lib import colors
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet

from Tools.PDF.models.template_style_models import CVStyles


class StyleFactory:
    @staticmethod
    def create_classic_green_styles(font: str, font_bold: str) -> CVStyles:
        base_styles = getSampleStyleSheet()

        base = ParagraphStyle(
            "Base",
            parent=base_styles["Normal"],
            fontName=font,
            fontSize=10.5,
            leading=13,
            textColor=colors.HexColor("#1a1a1a"),
        )

        small = ParagraphStyle(
            "Small",
            parent=base,
            fontSize=9,
            leading=11,
            textColor=colors.HexColor("#333333"),
        )

        sidebar_label = ParagraphStyle(
            "SidebarLabel",
            parent=base,
            fontName=font_bold,
            fontSize=9.5,
            leading=12,
            textColor=colors.white,
            spaceAfter=3,
        )

        sidebar_text = ParagraphStyle(
            "SidebarText",
            parent=small,
            textColor=colors.white,
            fontSize=9.2,
            leading=11.2,
        )

        sidebar_list = ParagraphStyle(
            "SidebarList",
            parent=sidebar_text,
            leftIndent=10,
            bulletIndent=0,
            bulletFontName=font_bold,
            bulletFontSize=10,
            bulletAnchor="start",
            spaceBefore=2,
            spaceAfter=2,
        )

        h_name = ParagraphStyle(
            "Name",
            parent=base,
            fontName=font_bold,
            fontSize=22,
            leading=25,
            textColor=colors.HexColor("#111111"),
            spaceAfter=2,
        )

        h_title = ParagraphStyle(
            "Title",
            parent=base,
            fontSize=11.5,
            leading=14,
            textColor=colors.HexColor("#444444"),
            spaceAfter=10,
        )

        summary = ParagraphStyle(
            "Summary",
            parent=small,
            fontSize=9.6,
            leading=12.5,
            textColor=colors.HexColor("#2a2a2a"),
            spaceAfter=12,
        )

        section_title = ParagraphStyle(
            "SectionTitle",
            parent=base,
            fontName=font_bold,
            fontSize=11,
            leading=14,
            textColor=colors.HexColor("#111111"),
            spaceBefore=6,
            spaceAfter=6,
        )

        section_text = ParagraphStyle(
            "SectionText",
            parent=small,
            spaceAfter=2,
        )

        item_title_green = ParagraphStyle(
            "ItemTitleGreen",
            parent=base,
            fontName=font_bold,
            fontSize=10.2,
            leading=12.5,
            textColor=colors.HexColor("#1f7a1f"),
            spaceAfter=1,
        )

        item_role = ParagraphStyle(
            "ItemRole",
            parent=small,
            fontName=font_bold,
            textColor=colors.HexColor("#111111"),
            spaceAfter=4,
        )

        bullet = ParagraphStyle(
            "Bullet",
            parent=small,
            leftIndent=12,
            bulletIndent=0,
            spaceBefore=1.5,
            spaceAfter=1.5,
        )

        rodo = ParagraphStyle(
            "Rodo",
            parent=small,
            fontSize=7.5,
            leading=9,
            textColor=colors.HexColor("#666666"),
            alignment=1,
        )

        tag = ParagraphStyle(
            "Tag",
            parent=small,
            fontName=font_bold,
            fontSize=8.6,
            leading=10.5,
            textColor=colors.white,
            alignment=1,
        )

        return CVStyles(
            font=font,
            font_bold=font_bold,
            base=base,
            small=small,
            sidebar_label=sidebar_label,
            sidebar_text=sidebar_text,
            sidebar_list=sidebar_list,
            h_name=h_name,
            h_title=h_title,
            summary=summary,
            section_title=section_title,
            section_text=section_text,
            item_title_green=item_title_green,
            item_role=item_role,
            bullet=bullet,
            rodo=rodo,
            tag=tag,
        )