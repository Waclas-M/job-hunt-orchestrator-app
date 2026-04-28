from abc import ABC, abstractmethod
from typing import Any, Dict, List

from Tools.PDF.models.cv_view_models import CvDocument


class ICvTemplate(ABC):
    @abstractmethod
    def get_document_config(self) -> Dict[str, Any]:
        """
        Zwraca konfigurację dokumentu:
        - pagesize
        - margins
        - title
        - author
        """
        pass

    @abstractmethod
    def get_frames(self, page_width: float, page_height: float) -> List[Any]:
        """
        Zwraca listę frame'ów ReportLab dla dokumentu.
        """
        pass

    @abstractmethod
    def build_story(self, cv_document: CvDocument, page_width: float, page_height: float) -> List[Any]:
        """
        Buduje pełną listę flowables dla dokumentu.
        """
        pass