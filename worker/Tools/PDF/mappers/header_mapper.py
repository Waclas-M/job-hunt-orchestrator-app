from Tools.PDF.Cv_Helpers.text_helper import TextHelper
from Tools.PDF.models.cv_view_models import CvHeader
from Tools.PDF.models.raw_cv_models import RawCvInput


class HeaderMapper:
    def map(self, raw_input: RawCvInput) -> CvHeader:
        personal_data = raw_input.PersonalData

        full_name = "Imię Nazwisko"
        summary = ""
        photo_raw = ''

        if personal_data:
            full_name = TextHelper.build_full_name(
                personal_data.FirstName,
                personal_data.LastName,
            ) or "Imię Nazwisko"

            summary = TextHelper.safe_strip(personal_data.PersonalProfile)

        if not summary:
            summary = self._build_summary(raw_input)

        if  raw_input.Photo != '':
            photo_raw = raw_input.Photo

        return CvHeader(
            full_name=full_name,
            title=self._resolve_title(raw_input),
            summary=summary,
            photo=photo_raw
        )

    def _resolve_title(self, raw_input: RawCvInput) -> str:
        if raw_input.Experience:
            first_title = TextHelper.safe_strip(raw_input.Experience[0].JobTitle)
            if first_title:
                return first_title

        return "Specjalista"

    def _build_summary(self, raw_input: RawCvInput) -> str:
        experience_count = len(raw_input.Experience)
        education_count = len(raw_input.Education)
        skills_count = len(raw_input.Skills)

        return (
            f"Kandydat z doświadczeniem zawodowym ({experience_count}), "
            f"wykształceniem ({education_count}) i umiejętnościami ({skills_count}) "
            f"przygotowanymi do dalszego dopasowania pod ofertę pracy."
        )