from abc import ABC, abstractmethod
from typing import Any, Dict, List


class ICvTemplate(ABC):
    @abstractmethod
    def get_document_config(self) -> Dict[str, Any]:
        """
        Zwraca konfigurację dokumentu np. marginesy, title, author, pagesize.
        """
        pass

    @abstractmethod
    def get_frames(self, page_width: float, page_height: float) -> List[Any]:
        """
        Zwraca listę frame'ów używanych przez PageTemplate.
        """
        pass

    @abstractmethod
    def build_story(self, cv_data: Dict[str, Any], page_width: float, page_height: float) -> List[Any]:
        """
        Buduje pełną listę flowables dla dokumentu.
        """
        pass