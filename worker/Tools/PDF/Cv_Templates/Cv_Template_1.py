import os
from dataclasses import dataclass
from typing import Any, Dict, List, Optional, Tuple

from reportlab.lib.pagesizes import A4
from reportlab.lib import colors
from reportlab.platypus import (
    Image,
    Frame,
    FrameBreak,
    KeepInFrame,
    Flowable,
)
from reportlab.lib.styles import ParagraphStyle
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.pdfgen.canvas import Canvas

from Tools.PDF.Cv_Templates.ICvTemplate import ICvTemplate


def safe_icon(path: str, w: float, h: float):
    if not path:
        return None

    ext = os.path.splitext(path.lower())[1]
    if ext not in (".png", ".jpg", ".jpeg", ".gif"):
        return None

    if not os.path.exists(path):
        return None

    try:
        return Image(path, width=w, height=h)
    except Exception:
        return None


def _find_windows_font(paths: List[str]) -> Optional[str]:
    for p in paths:
        if p and os.path.exists(p):
            return p
    return None


def register_windows_fonts() -> Tuple[str, str]:
    win_fonts_dir = os.path.join(os.environ.get("WINDIR", r"C:\Windows"), "Fonts")

    arial = _find_windows_font([
        os.path.join(win_fonts_dir, "arial.ttf"),
        os.path.join(win_fonts_dir, "Arial.ttf"),
    ])
    arial_bold = _find_windows_font([
        os.path.join(win_fonts_dir, "arialbd.ttf"),
        os.path.join(win_fonts_dir, "Arialbd.ttf"),
        os.path.join(win_fonts_dir, "ARIALBD.TTF"),
    ])

    if not arial or not arial_bold:
        return ("Helvetica", "Helvetica-Bold")

    reg_name = "CVFont"
    bold_name = "CVFont-Bold"
    pdfmetrics.registerFont(TTFont(reg_name, arial))
    pdfmetrics.registerFont(TTFont(bold_name, arial_bold))
    return reg_name, bold_name

class DotsLevel(Flowable):
    def __init__(
        self,
        level: int,
        max_level: int,
        dot_radius: float = 2.0,
        gap: float = 3.5,
        fill_on: colors.Color = colors.white,
        fill_off: colors.Color = colors.Color(1, 1, 1, alpha=0.25),
        stroke: Optional[colors.Color] = None,
        height: float = 8,
    ):
        super().__init__()
        self.level = max(0, int(level))
        self.max_level = max(1, int(max_level))
        self.dot_radius = dot_radius
        self.gap = gap
        self.fill_on = fill_on
        self.fill_off = fill_off
        self.stroke = stroke
        self._height = height
        self._width = (2 * dot_radius) * self.max_level + gap * (self.max_level - 1)

    def wrap(self, availWidth, availHeight):
        return min(self._width, availWidth), self._height

    def draw(self):
        c: Canvas = self.canv
        x = 0
        y = self._height / 2.0

        for i in range(1, self.max_level + 1):
            fill = self.fill_on if i <= self.level else self.fill_off
            c.setFillColor(fill)
            c.setStrokeColor(self.stroke or fill)
            c.circle(x + self.dot_radius, y, self.dot_radius, stroke=1, fill=1)
            x += 2 * self.dot_radius + self.gap

    @dataclass
    class CVStyles:
        font: str
        font_bold: str
        base: ParagraphStyle
        small: ParagraphStyle
        sidebar_label: ParagraphStyle
        sidebar_text: ParagraphStyle
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

    class ClassicGreenCvTemplate(ICvTemplate):
        def __init__(self):
            self.font, self.font_bold = register_windows_fonts()
            self.styles = self._make_styles(self.font, self.font_bold)

        def get_document_config(self) -> Dict[str, Any]:
            return {
                "pagesize": A4,
                "leftMargin": 0,
                "rightMargin": 0,
                "topMargin": 0,
                "bottomMargin": 0,
                "title": "CV",
                "author": "CV generator",
            }

        def get_frames(self, page_width: float, page_height: float) -> List[Any]:
            left_w = page_width * 0.60
            right_w = page_width * 0.40

            left_frame = Frame(
                0, 0, left_w, page_height,
                leftPadding=0, rightPadding=0, topPadding=0, bottomPadding=0,
                id="left",
                showBoundary=0
            )

            right_frame = Frame(
                left_w, 0, right_w, page_height,
                leftPadding=14, rightPadding=14, topPadding=16, bottomPadding=10,
                id="right",
                showBoundary=0
            )

            return [left_frame, right_frame]

        def build_story(self, cv_data: Dict[str, Any], page_width: float, page_height: float) -> List[Any]:
            left_w = page_width * 0.60
            right_w = page_width * 0.40

            cv = cv_data.get("cv", {})
            story: List[Any] = []

            sidebar_flow = self._build_sidebar(cv, left_w)
            story.append(
                KeepInFrame(
                    maxWidth=left_w,
                    maxHeight=page_height,
                    content=sidebar_flow,
                    mode="shrink",
                    hAlign="LEFT",
                    vAlign="TOP",
                )
            )

            story.append(FrameBreak())
            story.extend(self._build_main(cv, right_w - 28))

            return story

