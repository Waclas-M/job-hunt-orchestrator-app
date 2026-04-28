from dataclasses import dataclass
from reportlab.lib.styles import ParagraphStyle


@dataclass
class CVStyles:
    font: str
    font_bold: str
    base: ParagraphStyle
    small: ParagraphStyle
    sidebar_label: ParagraphStyle
    sidebar_text: ParagraphStyle
    sidebar_list: ParagraphStyle
    h_name: ParagraphStyle
    h_title: ParagraphStyle
    summary: ParagraphStyle
    section_title: ParagraphStyle
    section_text: ParagraphStyle
    item_title_green: ParagraphStyle
    item_role: ParagraphStyle
    bullet: ParagraphStyle
    rodo: ParagraphStyle
    tag: ParagraphStyle