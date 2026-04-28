from pathlib import Path
from reportlab.lib.pagesizes import A4
from reportlab.lib import colors
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.enums import TA_CENTER
from reportlab.lib.units import mm
from reportlab.platypus import (
    BaseDocTemplate, PageTemplate, Frame, Paragraph, Spacer, Image,
    Table, TableStyle, FrameBreak, KeepTogether
)
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.graphics.shapes import Drawing, Rect, Circle, String
from reportlab.graphics import renderPM

BASE = Path('/mnt/data')
PDF_OUT = BASE / 'cv_marcin_weclas_professional.pdf'
IMG_OUT = BASE / 'cv_avatar_placeholder.png'

FONT_REG = '/usr/share/fonts/truetype/noto/NotoSans-Regular.ttf'
FONT_BOLD = '/usr/share/fonts/truetype/noto/NotoSans-Bold.ttf'
pdfmetrics.registerFont(TTFont('NotoSans', FONT_REG))
pdfmetrics.registerFont(TTFont('NotoSans-Bold', FONT_BOLD))


def make_avatar(path: Path):
    d = Drawing(500, 500)
    d.add(Rect(0, 0, 500, 500, fillColor=colors.HexColor('#E8EEF3'), strokeColor=colors.HexColor('#E8EEF3')))
    d.add(Circle(250, 320, 90, fillColor=colors.HexColor('#C6D2DD'), strokeColor=colors.HexColor('#C6D2DD')))
    d.add(Circle(250, 130, 150, fillColor=colors.HexColor('#C6D2DD'), strokeColor=colors.HexColor('#C6D2DD')))
    d.add(String(250, 35, 'MW', fontName='Helvetica-Bold', fontSize=56, textAnchor='middle', fillColor=colors.HexColor('#4B5D6B')))
    renderPM.drawToFile(d, str(path), fmt='PNG')


def build_pdf():
    make_avatar(IMG_OUT)

    doc = BaseDocTemplate(
        str(PDF_OUT),
        pagesize=A4,
        leftMargin=12*mm,
        rightMargin=12*mm,
        topMargin=12*mm,
        bottomMargin=12*mm,
        title='Marcin Węcłaś - CV',
        author='OpenAI'
    )

    page_w, page_h = A4
    usable_w = page_w - doc.leftMargin - doc.rightMargin
    usable_h = page_h - doc.topMargin - doc.bottomMargin
    sidebar_w = usable_w * 0.38
    main_w = usable_w - sidebar_w
    gap = 0

    sidebar_bg = colors.HexColor('#1F3A4D')
    accent = colors.HexColor('#2F6B8A')
    soft = colors.HexColor('#EEF4F7')
    body = colors.HexColor('#1F2933')
    muted = colors.HexColor('#5B6871')

    def draw_bg(canvas, _doc):
        canvas.saveState()
        canvas.setFillColor(sidebar_bg)
        canvas.rect(doc.leftMargin, doc.bottomMargin, sidebar_w, usable_h, fill=1, stroke=0)
        canvas.restoreState()

    sidebar_frame = Frame(doc.leftMargin, doc.bottomMargin, sidebar_w, usable_h, leftPadding=12, rightPadding=12, topPadding=14, bottomPadding=14, id='sidebar')
    main_frame = Frame(doc.leftMargin + sidebar_w + gap, doc.bottomMargin, main_w, usable_h, leftPadding=16, rightPadding=8, topPadding=14, bottomPadding=14, id='main')
    doc.addPageTemplates([PageTemplate(id='cv', frames=[sidebar_frame, main_frame], onPage=draw_bg)])

    styles = getSampleStyleSheet()
    styles.add(ParagraphStyle(name='Name', fontName='NotoSans-Bold', fontSize=22, leading=26, textColor=colors.white, alignment=TA_CENTER, spaceAfter=2))
    styles.add(ParagraphStyle(name='Role', fontName='NotoSans', fontSize=10.5, leading=14, textColor=colors.HexColor('#DDE7EE'), alignment=TA_CENTER))
    styles.add(ParagraphStyle(name='SideSection', fontName='NotoSans-Bold', fontSize=10.5, leading=12, textColor=colors.white, spaceAfter=4))
    styles.add(ParagraphStyle(name='SideText', fontName='NotoSans', fontSize=8.7, leading=11.2, textColor=colors.white, spaceAfter=1))
    styles.add(ParagraphStyle(name='MainSection', fontName='NotoSans-Bold', fontSize=12, leading=14, textColor=accent, spaceAfter=4))
    styles.add(ParagraphStyle(name='Body', fontName='NotoSans', fontSize=9.6, leading=14, textColor=body))
    styles.add(ParagraphStyle(name='BodyMuted', fontName='NotoSans', fontSize=9.2, leading=13, textColor=muted))
    styles.add(ParagraphStyle(name='BodyBold', fontName='NotoSans-Bold', fontSize=10.2, leading=13, textColor=body))
    styles.add(ParagraphStyle(name='SmallCaps', fontName='NotoSans-Bold', fontSize=8.8, leading=11, textColor=muted))

    def para(text, style):
        return Paragraph(text, style)

    def bullets(items, style, spacer=2.5):
        story = []
        for item in items:
            story.append(Paragraph(f'• {item}', style))
            story.append(Spacer(1, spacer))
        return story

    def side_section(title, elements):
        s = [Paragraph(title.upper(), styles['SideSection']), Spacer(1, 5)]
        s.extend(elements)
        s.append(Spacer(1, 10))
        return s

    story = []

    avatar_size = sidebar_w - 24
    story += [Image(str(IMG_OUT), width=avatar_size, height=avatar_size), Spacer(1, 10)]
    story += [Paragraph('Marcin Węcłaś', styles['Name']), Paragraph('Tester / Student informatyki', styles['Role']), Spacer(1, 14)]

    story += side_section('Kontakt', [
        para('weclasmarcin@gmail.com', styles['SideText']),
        para('github.com/Waclas-M', styles['SideText']),
        para('LinkedIn: marcin-węcłaś-594053322', styles['SideText']),
    ])
    story += side_section('Umiejętności', bullets([
        '.NET', 'Jira', 'Confluence', 'Azure', 'Excel',
        'Systemy wewnętrzne „szyna”', 'Systemy sprzedażowe i ubezpieczeniowe'
    ], styles['SideText']))
    story += side_section('Języki', bullets(['Polski', 'Angielski'], styles['SideText']))
    story += side_section('Atuty', bullets(['Cross-platformowość', 'Samodzielność', 'Szybka nauka', 'Znajomość wielu frameworków'], styles['SideText']))
    story += side_section('Zainteresowania', [para('Programowanie', styles['SideText'])])

    story += [FrameBreak()]

    summary_text = (
        'Student informatyki z doświadczeniem w tworzeniu aplikacji webowych i systemów opartych o AI. '
        'Pracuję z technologiami .NET, Python i Angular, budując projekty wykorzystujące mikroserwisy, '
        'RabbitMQ oraz modele językowe (LLM). Skupiam się na tworzeniu skalowalnych rozwiązań oraz '
        'rozwijaniu kompetencji w obszarze machine learning i systemów backendowych.'
    )
    story += [Paragraph('Profil zawodowy', styles['MainSection']), Paragraph(summary_text, styles['Body']), Spacer(1, 12)]

    story += [Paragraph('Doświadczenie', styles['MainSection'])]
    exp = [
        Paragraph('PKO Leasing', styles['BodyBold']),
        Paragraph('Tester | 05.2024 - obecnie', styles['SmallCaps']),
        Spacer(1, 4),
    ] + bullets([
        'Testowanie funkcjonalne.', 'Testy regresji.', 'Tworzenie scenariuszy testowych w Jira.',
        'Zgłaszanie błędów biznesowych w Jira.', 'Udział w spotkaniach zespołów wytwórczo-projektowych.',
        'Udział w smoke testach.', 'Pomoc w przeprowadzaniu audytów.'
    ], styles['Body'], spacer=2)
    story += [KeepTogether(exp), Spacer(1, 8)]

    story += [Paragraph('Wykształcenie', styles['MainSection'])]
    story += [Paragraph('Wyższa Warszawska Szkoła Informatyki', styles['BodyBold']), Paragraph('Inżynier oprogramowania | 10.2024 - obecnie', styles['SmallCaps']), Spacer(1, 12)]

    story += [Paragraph('Profil techniczny', styles['MainSection'])]
    tech_table = Table([
        [Paragraph('<b>Backend</b>', styles['Body']), Paragraph('.NET, Python, RabbitMQ, systemy backendowe', styles['Body'])],
        [Paragraph('<b>Frontend</b>', styles['Body']), Paragraph('Angular, aplikacje webowe', styles['Body'])],
        [Paragraph('<b>AI / ML</b>', styles['Body']), Paragraph('LLM, rozwiązania oparte o AI, rozwój kompetencji ML', styles['Body'])],
        [Paragraph('<b>Narzędzia</b>', styles['Body']), Paragraph('Jira, Confluence, Azure, Excel', styles['Body'])],
    ], colWidths=[main_w*0.24, main_w*0.70])
    tech_table.setStyle(TableStyle([
        ('BACKGROUND', (0, 0), (-1, -1), soft),
        ('BOX', (0, 0), (-1, -1), 0.6, colors.HexColor('#D6E2E8')),
        ('INNERGRID', (0, 0), (-1, -1), 0.4, colors.HexColor('#D6E2E8')),
        ('VALIGN', (0, 0), (-1, -1), 'MIDDLE'),
        ('LEFTPADDING', (0, 0), (-1, -1), 8), ('RIGHTPADDING', (0, 0), (-1, -1), 8),
        ('TOPPADDING', (0, 0), (-1, -1), 7), ('BOTTOMPADDING', (0, 0), (-1, -1), 7),
    ]))
    story += [tech_table, Spacer(1, 12)]

    story += [Paragraph('Klauzula', styles['MainSection'])]
    clause = 'Wyrażam zgodę na przetwarzanie moich danych osobowych dla potrzeb niezbędnych do realizacji procesu rekrutacji zgodnie z obowiązującymi przepisami prawa.'
    story += [Paragraph(clause, styles['BodyMuted'])]

    doc.build(story)
    print(PDF_OUT)


if __name__ == '__main__':
    build_pdf()
