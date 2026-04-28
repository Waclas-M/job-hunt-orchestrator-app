from typing import List
from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.cv_view_models import CvProjectsItem
from Tools.PDF.models.raw_cv_models import RawProjects

class ProjectsMapper:
    def map(self, projects: List[RawProjects]) -> List[CvProjectsItem]:
        results: List[CvProjectsItem] = []
        sorted_items = sorted(
            projects,
            key=lambda item: (item.IsCurrent, item.StartDate or ""),
            reverse=True
        )
        for item in sorted_items:
            results.append(
                CvProjectsItem(
                    date_range=TextHelper.build_date_range(start_date=item.StartDate, end_date=item.EndDate, is_current=False),
                    name=TextHelper.safe_strip(item.Name),
                    description=TextHelper.safe_strip(item.Description),
                    link=item.Link,
                    technologies= item.Technologies
                )
            )

        return results