from typing import Optional

from reportlab.lib import colors
from reportlab.pdfgen.canvas import Canvas
from reportlab.platypus import Flowable


class DotsLevel(Flowable):
    """
    Rysuje poziom w postaci kropek, np. 7/10.
    """

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
        canvas: Canvas = self.canv
        x = 0
        y = self._height / 2.0

        for i in range(1, self.max_level + 1):
            fill = self.fill_on if i <= self.level else self.fill_off

            canvas.setFillColor(fill)
            canvas.setStrokeColor(self.stroke or fill)
            canvas.circle(x + self.dot_radius, y, self.dot_radius, stroke=1, fill=1)

            x += 2 * self.dot_radius + self.gap