from pathlib import Path
import base64
from io import BytesIO

from reportlab.lib.pagesizes import A4
from reportlab.lib import colors
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.enums import TA_CENTER
from reportlab.platypus import (
    BaseDocTemplate,
    PageTemplate,
    Frame,
    Paragraph,
    Spacer,
    Image,
    Table,
    FrameBreak,
    KeepTogether,
    NextPageTemplate,
    Flowable,
)
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.graphics.shapes import Drawing, Rect, Circle, String
from reportlab.graphics import renderPM

from Tools.PDF.models.cv_view_models import CvDocument


BASE = Path('/app/WorkFlow/generated_cv/')
PDF_OUT = BASE / 'cv.pdf'
IMG_OUT = BASE / 'cv_avatar_placeholder.png'

FONT_REG = Path('/app/assets/fonts/DejaVuSans.ttf')
FONT_BOLD = Path('/app/assets/fonts/DejaVuSans-Bold.ttf')

print("FONT_REG exists:", FONT_REG.exists(), FONT_REG)
print("FONT_BOLD exists:", FONT_BOLD.exists(), FONT_BOLD)

pdfmetrics.registerFont(TTFont('DejaVuSans', str(FONT_REG)))
pdfmetrics.registerFont(TTFont('DejaVuSans-Bold', str(FONT_BOLD)))


def countFiles(userID: str) -> int:
    base_path = Path('/app/WorkFlow/generated_cv') / str(userID)
    base_path.mkdir(parents=True, exist_ok=True)

    files = list(base_path.glob('*.pdf'))
    return len(files)


def make_avatar(path: Path, cv_document: CvDocument, user_id: str):
    d = Drawing(350, 350)

    d.add(Rect(
        0,
        0,
        350,
        350,
        fillColor=colors.HexColor('#E8EEF3'),
        strokeColor=colors.HexColor('#E8EEF3')
    ))

    d.add(Circle(
        175,
        225,
        90,
        fillColor=colors.HexColor('#C6D2DD'),
        strokeColor=colors.HexColor('#C6D2DD')
    ))

    d.add(Circle(
        175,
        30,
        150,
        fillColor=colors.HexColor('#C6D2DD'),
        strokeColor=colors.HexColor('#C6D2DD')
    ))

    initials = "".join([
        part[0]
        for part in cv_document.header.full_name.split()
        if part
    ])

    d.add(String(
        175,
        35,
        initials,
        fontName='Helvetica-Bold',
        fontSize=56,
        textAnchor='middle',
        fillColor=colors.HexColor('#4B5D6B')
    ))

    renderPM.drawToFile(d, str(path), fmt='PNG')


def LoadPhoto(photo_base64, avatar_size):
    if "," in photo_base64 and photo_base64.startswith("data:image"):
        photo_base64 = photo_base64.split(",", 1)[1]

    image_bytes = base64.b64decode(photo_base64)
    image_buffer = BytesIO(image_bytes)

    return Image(
        image_buffer,
        width=avatar_size,
        height=avatar_size * 1.15
    )


class SectionHeader(Flowable):
    """
    Dynamiczny nagłówek sekcji.
    Dopasowuje się do aktualnej szerokości frame'a:
    - na stronie 1 do głównej kolumny,
    - na stronie 2+ do pełnej szerokości strony.
    """

    def __init__(self, title, paragraph_style, line_color):
        super().__init__()
        self.title = title
        self.paragraph_style = paragraph_style
        self.line_color = line_color
        self.paragraph = Paragraph(title.upper(), paragraph_style)
        self.width = 0
        self.height = 0
        self.paragraph_width = 0
        self.paragraph_height = 0

    def wrap(self, availWidth, availHeight):
        self.width = availWidth
        self.paragraph_width, self.paragraph_height = self.paragraph.wrap(
            availWidth,
            availHeight
        )

        self.height = self.paragraph_height + 11
        return self.width, self.height

    def draw(self):
        self.paragraph.drawOn(self.canv, 0, 6)

        self.canv.saveState()
        self.canv.setStrokeColor(self.line_color)
        self.canv.setLineWidth(1)
        self.canv.line(0, 2, self.width, 2)
        self.canv.restoreState()


def build_pdf(cv_document: CvDocument, user_id: str):
    print(type(cv_document.header.full_name))
    print(cv_document.header.full_name)

    PDF_OUT = BASE / f'{user_id}/cv{countFiles(user_id)}.pdf'
    IMG_OUT = BASE / f'{user_id}/cv_avatar_placeholder.png'

    make_avatar(IMG_OUT, cv_document, user_id)

    doc = BaseDocTemplate(
        str(PDF_OUT),
        pagesize=A4,
        leftMargin=0,
        rightMargin=0,
        topMargin=0,
        bottomMargin=0,
        title=f'{cv_document.header.full_name} - CV',
        author='JHOP'
    )

    page_w, page_h = A4
    usable_w = page_w - doc.leftMargin - doc.rightMargin
    usable_h = page_h - doc.topMargin - doc.bottomMargin

    sidebar_w = usable_w * 0.32
    main_w = usable_w - sidebar_w
    gap = 0

    # Kolory
    sidebar_bg = colors.HexColor('#0F172A')
    sidebar_card = colors.HexColor('#1E293B')
    accent = colors.HexColor('#38BDF8')
    accent_dark = colors.HexColor('#0284C7')
    soft = colors.HexColor('#F1F5F9')
    line = colors.HexColor('#E2E8F0')
    body = colors.HexColor('#0F172A')
    muted = colors.HexColor('#64748B')
    white = colors.white

    # Paddingi strony 1
    SIDEBAR_LEFT_PAD = 12
    SIDEBAR_RIGHT_PAD = 12
    MAIN_LEFT_PAD = 18
    MAIN_RIGHT_PAD = 18
    MAIN_CONTENT_W = main_w - MAIN_LEFT_PAD - MAIN_RIGHT_PAD

    # Paddingi stron 2+
    FULL_LEFT_PAD = 34
    FULL_RIGHT_PAD = 34
    FULL_TOP_PAD = 24
    FULL_BOTTOM_PAD = 24
    FULL_CONTENT_W = usable_w - FULL_LEFT_PAD - FULL_RIGHT_PAD

    def draw_first_page_bg(canvas, _doc):
        canvas.saveState()
        canvas.setFillColor(sidebar_bg)
        canvas.rect(
            doc.leftMargin,
            doc.bottomMargin,
            sidebar_w,
            usable_h,
            fill=1,
            stroke=0
        )
        canvas.restoreState()

    def draw_full_page_bg(canvas, _doc):
        canvas.saveState()

        # Subtelny pasek dekoracyjny na górze kolejnych stron.
        canvas.setFillColor(accent)
        canvas.rect(
            doc.leftMargin,
            page_h - 5,
            usable_w,
            5,
            fill=1,
            stroke=0
        )

        canvas.restoreState()

    sidebar_frame = Frame(
        doc.leftMargin,
        doc.bottomMargin,
        sidebar_w,
        usable_h,
        leftPadding=SIDEBAR_LEFT_PAD,
        rightPadding=SIDEBAR_RIGHT_PAD,
        topPadding=14,
        bottomPadding=14,
        id='sidebar'
    )

    main_frame = Frame(
        doc.leftMargin + sidebar_w + gap,
        doc.bottomMargin,
        main_w,
        usable_h,
        leftPadding=MAIN_LEFT_PAD,
        rightPadding=MAIN_RIGHT_PAD,
        topPadding=18,
        bottomPadding=14,
        id='main'
    )

    full_page_frame = Frame(
        doc.leftMargin,
        doc.bottomMargin,
        usable_w,
        usable_h,
        leftPadding=FULL_LEFT_PAD,
        rightPadding=FULL_RIGHT_PAD,
        topPadding=FULL_TOP_PAD,
        bottomPadding=FULL_BOTTOM_PAD,
        id='full_page'
    )

    doc.addPageTemplates([
        PageTemplate(
            id='first_page',
            frames=[sidebar_frame, main_frame],
            onPage=draw_first_page_bg
        ),
        PageTemplate(
            id='full_page',
            frames=[full_page_frame],
            onPage=draw_full_page_bg
        )
    ])

    styles = getSampleStyleSheet()

    styles.add(ParagraphStyle(
        name='NameMain',
        fontName='DejaVuSans-Bold',
        fontSize=25,
        leading=30,
        textColor=body,
        spaceAfter=3,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='RoleMain',
        fontName='DejaVuSans',
        fontSize=10.5,
        leading=14,
        textColor=muted,
        spaceAfter=12,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='SideSection',
        fontName='DejaVuSans-Bold',
        fontSize=9.5,
        leading=12,
        textColor=accent,
        spaceAfter=6,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='SideText',
        fontName='DejaVuSans',
        fontSize=8.4,
        leading=11.5,
        textColor=colors.HexColor('#E2E8F0'),
        spaceAfter=2,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='MainSection',
        fontName='DejaVuSans-Bold',
        fontSize=10.5,
        leading=13,
        textColor=accent_dark,
        spaceAfter=4,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='Body',
        fontName='DejaVuSans',
        fontSize=9.2,
        leading=13.5,
        textColor=body,
        spaceAfter=2,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='BodyMuted',
        fontName='DejaVuSans',
        fontSize=8.2,
        leading=11.5,
        textColor=muted,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='BodyBold',
        fontName='DejaVuSans-Bold',
        fontSize=10,
        leading=13,
        textColor=body,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='Meta',
        fontName='DejaVuSans',
        fontSize=8.3,
        leading=11,
        textColor=accent_dark,
        leftIndent=0,
        firstLineIndent=0
    ))

    styles.add(ParagraphStyle(
        name='Chip',
        fontName='DejaVuSans',
        fontSize=7.4,
        leading=9,
        textColor=colors.white,
        alignment=TA_CENTER
    ))

    styles.add(ParagraphStyle(
        name='SmallCaps',
        fontName='DejaVuSans-Bold',
        fontSize=8.8,
        leading=11,
        textColor=muted,
        leftIndent=0,
        firstLineIndent=0
    ))

    def section_header(title):
        return SectionHeader(
            title=title,
            paragraph_style=styles['MainSection'],
            line_color=line
        )

    def chips(items, col_width):
        rows = []
        row = []

        for item in items:
            chip = Table(
                [[Paragraph(str(item), styles['Chip'])]],
                style=[
                    ('BACKGROUND', (0, 0), (-1, -1), colors.HexColor('#334155')),
                    ('BOX', (0, 0), (-1, -1), 0.4, colors.HexColor('#475569')),
                    ('LEFTPADDING', (0, 0), (-1, -1), 5),
                    ('RIGHTPADDING', (0, 0), (-1, -1), 5),
                    ('TOPPADDING', (0, 0), (-1, -1), 3),
                    ('BOTTOMPADDING', (0, 0), (-1, -1), 3),
                ]
            )

            row.append(chip)

            if len(row) == 2:
                rows.append(row)
                row = []

        if row:
            while len(row) < 2:
                row.append('')
            rows.append(row)

        return Table(
            rows,
            colWidths=[col_width / 2 - 4, col_width / 2 - 4],
            hAlign='LEFT',
            style=[
                ('VALIGN', (0, 0), (-1, -1), 'TOP'),
                ('BOTTOMPADDING', (0, 0), (-1, -1), 4),
                ('RIGHTPADDING', (0, 0), (-1, -1), 4),
            ]
        )

    def bullets(items, style, spacer=2.5):
        result = []

        for item in items:
            result.append(Paragraph(f'• {item}', style))
            result.append(Spacer(1, spacer))

        return result

    def side_section(title, elements):
        result = [
            Paragraph(title.upper(), styles['SideSection']),
            Spacer(1, 3)
        ]

        result.extend(elements)
        result.append(Spacer(1, 12))

        return result

    def experience_block(exp):
        timeline_w = 12
        text_w = MAIN_CONTENT_W - timeline_w - 8

        content = [
            [
                '',
                [
                    Paragraph(exp.company, styles['BodyBold']),
                    Paragraph(f'{exp.role} | {exp.date_range}', styles['Meta']),
                    Spacer(1, 4),
                    *bullets(exp.bullets, styles['Body'], spacer=1.8)
                ]
            ]
        ]

        table = Table(
            content,
            colWidths=[timeline_w, text_w],
            style=[
                ('LINEBEFORE', (0, 0), (0, 0), 1.2, accent),
                ('VALIGN', (0, 0), (-1, -1), 'TOP'),

                ('LEFTPADDING', (0, 0), (0, 0), 0),
                ('RIGHTPADDING', (0, 0), (0, 0), 6),
                ('LEFTPADDING', (1, 0), (1, 0), 8),
                ('RIGHTPADDING', (1, 0), (1, 0), 0),

                ('TOPPADDING', (0, 0), (-1, -1), 0),
                ('BOTTOMPADDING', (0, 0), (-1, -1), 8),
            ]
        )

        return table

    def project_block(project):
        block = []

        header = [
            Paragraph(project.name, styles['BodyBold']),
            Paragraph(project.date_range, styles['Meta']),
            Spacer(1, 4)
        ]

        # Trzymamy razem tylko nazwę + datę,
        # ale opis może się normalnie podzielić między stronami.
        block.append(KeepTogether(header))

        if project.description:
            block += [
                Paragraph(project.description, styles['Body']),
                Spacer(1, 4)
            ]

        if project.technologies:
            tech_text = ", ".join(project.technologies)
            block += [
                Paragraph(f'<b>Technologie:</b> {tech_text}', styles['Body']),
                Spacer(1, 3)
            ]

        if project.link:
            block += [
                Paragraph(
                    f'<b>Link:</b> <link href="{project.link}">{project.link}</link>',
                    styles['BodyMuted']
                ),
                Spacer(1, 3)
            ]

        block.append(Spacer(1, 10))

        return block

    story = []

    # To jest najważniejsze dla opcji B:
    # pierwsza strona używa first_page, a każda kolejna automatycznie full_page.
    story.append(NextPageTemplate('full_page'))

    # =========================
    # SIDEBAR - tylko strona 1
    # =========================

    avatar_size = sidebar_w - SIDEBAR_LEFT_PAD - SIDEBAR_RIGHT_PAD

    if cv_document.header.photo is not None:
        image = LoadPhoto(cv_document.header.photo, avatar_size)
        story += [image, Spacer(1, 10)]
    else:
        story += [
            Image(str(IMG_OUT), width=avatar_size, height=avatar_size),
            Spacer(1, 10)
        ]

    if cv_document.sidebar.contact_lines:
        story += side_section('Kontakt', [
            Paragraph("<br/>".join(cv_document.sidebar.contact_lines), styles['SideText'])
        ])

    if cv_document.sidebar.skills:
        story += side_section('Stack techniczny', [
            chips(cv_document.sidebar.skills, sidebar_w - SIDEBAR_LEFT_PAD - SIDEBAR_RIGHT_PAD)
        ])

    if cv_document.sidebar.languages:
        story += side_section('Języki', [
            Paragraph(
                "<br/>".join([
                    f"• {language.name}"
                    for language in cv_document.sidebar.languages
                ]),
                styles['SideText']
            )
        ])

    if cv_document.main.strengths_tags:
        story += side_section('Atuty', [
            Paragraph(
                "<br/>".join([
                    f"• {strength}"
                    for strength in cv_document.main.strengths_tags
                ]),
                styles['SideText']
            )
        ])

    # Przejście z sidebara do głównej kolumny na pierwszej stronie.
    story += [FrameBreak()]

    # =========================
    # GŁÓWNA TREŚĆ
    # =========================

    summary_text = cv_document.header.summary

    story += [
        Paragraph(cv_document.header.full_name, styles['NameMain']),
        Spacer(1, 4)
    ]

    if summary_text:
        story += [
            section_header('Profil zawodowy'),
            Paragraph(summary_text, styles['Body']),
            Spacer(1, 12)
        ]

    if cv_document.main.experience:
        story += [section_header('Doświadczenie')]

        for experience in cv_document.main.experience:
            story += [
                KeepTogether([experience_block(experience)]),
                Spacer(1, 6)
            ]

    if cv_document.main.education:
        story += [section_header('Wykształcenie')]

        for education in cv_document.main.education:
            education_block = [
                Paragraph(f'{education.school}', styles['BodyBold']),
                Paragraph(
                    f'{education.details} | {education.date_range}',
                    styles['SmallCaps']
                ),
                Spacer(1, 12)
            ]

            story += [KeepTogether(education_block)]

    if cv_document.main.projects:
        story += [section_header('Projekty')]

        for project in cv_document.main.projects:
            story += project_block(project)

    if cv_document.main.rodo_line:
        story += [
            Spacer(1, 6),
            section_header('Klauzula'),
            Paragraph(cv_document.main.rodo_line, styles['BodyMuted'])
        ]

    doc.build(story)

    print(PDF_OUT)
    return PDF_OUT