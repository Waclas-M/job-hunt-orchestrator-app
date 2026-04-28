from typing import List

from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.raw_cv_models import RawStrength


class StrengthsMapper:
    def map(self, strengths: List[RawStrength]) -> List[str]:
        values = [item.Strength for item in strengths]
        return TextHelper.unique_non_empty(values)