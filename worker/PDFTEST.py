import os
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Table, TableStyle
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet
from reportlab.lib.pagesizes import A4
from reportlab.lib.units import cm
from reportlab.lib import colors
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.pdfbase import pdfmetrics

# --------------------------------------------------
# 1️⃣ Rejestracja czcionki Arial (Windows)
# --------------------------------------------------

arial_path = r"C:\Windows\Fonts\arial.ttf"

if not os.path.exists(arial_path):
    raise FileNotFoundError("Nie znaleziono arial.ttf w Windows Fonts.")

pdfmetrics.registerFont(TTFont("Arial", arial_path))

# --------------------------------------------------
# 2️⃣ Tworzenie dokumentu (bez marginesów)
# --------------------------------------------------

doc = SimpleDocTemplate(
    "arial_polskie_znaki.pdf",
    pagesize=A4,
    rightMargin=0,
    leftMargin=0,
    topMargin=0,
    bottomMargin=0
)

elements = []

# --------------------------------------------------
# 3️⃣ Tworzenie własnych stylów z Arial
# --------------------------------------------------

styles = getSampleStyleSheet()

normal_style = ParagraphStyle(
    name="ArialNormal",
    parent=styles["Normal"],
    fontName="Arial",
    fontSize=12
)

heading_style = ParagraphStyle(
    name="ArialHeading",
    parent=styles["Heading1"],
    fontName="Arial",
    fontSize=18
)

# --------------------------------------------------
# 4️⃣ Nagłówek
# --------------------------------------------------

elements.append(Paragraph("Zażółć gęślą jaźń — polskie znaki działają ✔", heading_style))
elements.append(Spacer(1, 0.7 * cm))

# --------------------------------------------------
# 5️⃣ Sekcja w boxie (Table jako kontener)
# --------------------------------------------------

section_content = Paragraph(
    "To jest treść sekcji z czcionką Arial.<br/>"
    "Obsługiwane znaki: ąćęłńóśźż ĄĆĘŁŃÓŚŹŻ",
    normal_style
)

table = Table([[section_content]], colWidths=[(A4[0])])

table.setStyle(TableStyle([
    ("BOX", (0,0), (-1,-1), 1, colors.black),
    ("INNERPADDING", (0,0), (-1,-1), 15),
]))

elements.append(table)

# --------------------------------------------------
# 6️⃣ Budowanie dokumentu
# --------------------------------------------------

doc.build(elements)

print("PDF wygenerowany poprawnie ✔")