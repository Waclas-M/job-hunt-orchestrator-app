from typing import List

from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.cv_view_models import CvLanguage
from Tools.PDF.models.raw_cv_models import RawLanguage


class LanguageMapper:
    TEXT_LEVEL_MAPPING = {
        "basic": 3,
        "intermediate": 6,
        "advanced": 8,
        "native": 10,
    }

    NUMERIC_LEVEL_MAPPING = {
        "0": 3,
        "1": 6,
        "2": 8,
        "3": 10,
    }

    def map(self, languages: List[RawLanguage]) -> List[CvLanguage]:
        results: List[CvLanguage] = []

        for item in languages:
            name = TextHelper.safe_strip(item.Language)
            if not name:
                continue

            results.append(
                CvLanguage(
                    name=name,
                    level_dots=self._map_level_to_dots(item.Level),
                    max_dots=10,
                )
            )

        return results

    def _map_level_to_dots(self, level: str | None) -> int:
        cleaned = TextHelper.safe_strip(level).lower()

        if not cleaned:
            return 0

        if cleaned in self.TEXT_LEVEL_MAPPING:
            return self.TEXT_LEVEL_MAPPING[cleaned]

        if cleaned in self.NUMERIC_LEVEL_MAPPING:
            return self.NUMERIC_LEVEL_MAPPING[cleaned]

        if cleaned.isdigit():
            numeric_value = int(cleaned)
            if 0 <= numeric_value <= 10:
                return numeric_value

        return 0