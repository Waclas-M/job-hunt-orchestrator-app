from typing import List

from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.cv_view_models import CvEducationItem
from Tools.PDF.models.raw_cv_models import RawEducation


class EducationMapper:
    def map(self, educations: List[RawEducation]) -> List[CvEducationItem]:
        results: List[CvEducationItem] = []

        sorted_items = sorted(
            educations,
            key=lambda item: (item.IsCurrent, item.StartDate or ""),
            reverse=True
        )

        for item in sorted_items:
            results.append(
                CvEducationItem(
                    date_range=TextHelper.build_date_range(
                        start_date=item.StartDate,
                        end_date=item.EndDate,
                        is_current=item.IsCurrent,
                    ),
                    school=TextHelper.safe_strip(item.SchoolName),
                    details=TextHelper.safe_strip(item.StudyProfile),
                )
            )

        return results