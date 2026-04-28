import os
from typing import Optional

from reportlab.lib import colors
from reportlab.platypus import Image, Table, TableStyle


class ImageHelper:
    SUPPORTED_EXTENSIONS = (".png", ".jpg", ".jpeg", ".gif")

    @staticmethod
    def is_supported_image(path: str) -> bool:
        if not path:
            return False

        ext = os.path.splitext(path.lower())[1]
        return ext in ImageHelper.SUPPORTED_EXTENSIONS

    @staticmethod
    def exists_and_supported(path: str) -> bool:
        return bool(path) and os.path.exists(path) and ImageHelper.is_supported_image(path)

    @staticmethod
    def safe_image(path: str, width: float, height: float) -> Optional[Image]:
        """
        Zwraca Image jeśli plik istnieje i jest wspierany.
        W przeciwnym razie zwraca None.
        """
        if not ImageHelper.exists_and_supported(path):
            return None

        try:
            return Image(path, width=width, height=height)
        except Exception:
            return None

    @staticmethod
    def placeholder_box(width: float, height: float, color_hex: str = "#E6E6E6") -> Table:
        """
        Placeholder np. zamiast zdjęcia lub ikony.
        """
        table = Table([[""]], colWidths=[width], rowHeights=[height])
        table.setStyle(TableStyle([
            ("BACKGROUND", (0, 0), (-1, -1), colors.HexColor(color_hex)),
        ]))
        return table