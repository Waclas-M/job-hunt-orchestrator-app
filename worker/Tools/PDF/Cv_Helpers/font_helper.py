import os
from typing import List, Optional, Tuple

from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont


class FontHelper:
    @staticmethod
    def find_first_existing_path(paths: List[str]) -> Optional[str]:
        for path in paths:
            if path and os.path.exists(path):
                return path
        return None

    @staticmethod
    def register_fonts() -> Tuple[str, str]:
        """
        Rejestruje font regular i bold z obsługą polskich znaków.
        Preferencja:
        1. fonty lokalne projektu
        2. Linux DejaVu
        3. Windows Arial
        4. fallback Helvetica
        """
        possible_regular_paths = [
            # lokalne fonty w projekcie
            "./assets/fonts/DejaVuSans.ttf",
            "./fonts/DejaVuSans.ttf",

            # typowe Linux
            "/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf",
            "/usr/share/fonts/dejavu/DejaVuSans.ttf",

            # typowe Windows
            os.path.join(os.environ.get("WINDIR", r"C:\Windows"), "Fonts", "arial.ttf"),
            os.path.join(os.environ.get("WINDIR", r"C:\Windows"), "Fonts", "Arial.ttf"),
        ]

        possible_bold_paths = [
            # lokalne fonty w projekcie
            "./assets/fonts/DejaVuSans-Bold.ttf",
            "./fonts/DejaVuSans-Bold.ttf",

            # typowe Linux
            "/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf",
            "/usr/share/fonts/dejavu/DejaVuSans-Bold.ttf",

            # typowe Windows
            os.path.join(os.environ.get("WINDIR", r"C:\Windows"), "Fonts", "arialbd.ttf"),
            os.path.join(os.environ.get("WINDIR", r"C:\Windows"), "Fonts", "Arialbd.ttf"),
            os.path.join(os.environ.get("WINDIR", r"C:\Windows"), "Fonts", "ARIALBD.TTF"),
        ]

        regular_path = FontHelper.find_first_existing_path(possible_regular_paths)
        bold_path = FontHelper.find_first_existing_path(possible_bold_paths)

        if not regular_path or not bold_path:
            print(" [!] No Unicode font found. Falling back to Helvetica.")
            return "Helvetica", "Helvetica-Bold"

        regular_name = "CVFont"
        bold_name = "CVFont-Bold"

        pdfmetrics.registerFont(TTFont(regular_name, regular_path))
        pdfmetrics.registerFont(TTFont(bold_name, bold_path))

        print(f" [x] Registered regular font: {regular_path}")
        print(f" [x] Registered bold font: {bold_path}")

        return regular_name, bold_name


