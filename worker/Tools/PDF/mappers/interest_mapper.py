from typing import List

from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.cv_view_models import CvInterestItem
from Tools.PDF.models.raw_cv_models import RawInterest


class InterestMapper:
    def map(self, interests: List[RawInterest]) -> List[CvInterestItem]:
        results: List[CvInterestItem] = []

        for item in interests:
            label = TextHelper.safe_strip(item.Interest)
            if not label:
                continue

            results.append(
                CvInterestItem(
                    label=label,
                    description=TextHelper.safe_strip(item.Description),
                )
            )

        return results