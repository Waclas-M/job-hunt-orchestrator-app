from typing import Optional

from reportlab.platypus import BaseDocTemplate, PageTemplate

from Tools.PDF.models.cv_view_models import CvDocument
from Tools.PDF.templates.i_cv_template import ICvTemplate


class CvGenerator:
    def __init__(self, template: ICvTemplate):
        self.template = template

    def generate(self, cv_document: CvDocument, output_path: str) -> str:
        """
        Generuje PDF na podstawie przekazanego template i CvDocument.
        Zwraca ścieżkę do wygenerowanego pliku.
        """
        config = self.template.get_document_config()

        pagesize = config.get("pagesize")
        page_width, page_height = pagesize

        document = BaseDocTemplate(
            filename=output_path,
            pagesize=pagesize,
            leftMargin=config.get("leftMargin", 0),
            rightMargin=config.get("rightMargin", 0),
            topMargin=config.get("topMargin", 0),
            bottomMargin=config.get("bottomMargin", 0),
            title=config.get("title", "CV"),
            author=config.get("author", "CV Generator"),
        )

        frames = self.template.get_frames(page_width, page_height)
        page_template = PageTemplate(
            id="cv_page_template",
            frames=frames,
        )

        document.addPageTemplates([page_template])

        story = self.template.build_story(
            cv_document=cv_document,
            page_width=page_width,
            page_height=page_height,
        )

        document.build(story)

        return output_path