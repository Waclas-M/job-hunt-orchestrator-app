from typing import List

from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.cv_view_models import CvExperienceItem
from Tools.PDF.models.raw_cv_models import RawExperience


class ExperienceMapper:
    def map(self, experiences: List[RawExperience]) -> List[CvExperienceItem]:
        results: List[CvExperienceItem] = []

        sorted_items = sorted(
            experiences,
            key=lambda item: (item.IsCurrent, item.StartDate or ""),
            reverse=True
        )

        for item in sorted_items:
            bullets = self._map_bullets(item)

            results.append(
                CvExperienceItem(
                    date_range=TextHelper.build_date_range(
                        start_date=item.StartDate,
                        end_date=item.EndDate,
                        is_current=item.IsCurrent,
                    ),
                    company=TextHelper.safe_strip(item.CompanyName),
                    role=TextHelper.safe_strip(item.JobTitle),
                    bullets=bullets,
                )
            )

        return results

    def _map_bullets(self, item: RawExperience) -> List[str]:
        bullets = TextHelper.extract_bullet_lines(item.JobDescription, limit=8)

        if bullets:
            return bullets

        fallback_parts = [
            TextHelper.safe_strip(item.JobTitle),
            TextHelper.safe_strip(item.CompanyName),
            TextHelper.normalize_whitespace(item.JobDescription),
        ]

        fallback = TextHelper.take_first_non_empty(fallback_parts)
        return [fallback] if fallback else []