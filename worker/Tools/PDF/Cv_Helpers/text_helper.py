from datetime import datetime
from typing import List, Optional


class TextHelper:
    @staticmethod
    def safe_strip(value: Optional[str]) -> str:
        if value is None:
            return ""
        return str(value).strip()

    @staticmethod
    def format_month_year(date_str: Optional[str]) -> str:
        if not date_str:
            return ""

        try:
            dt = datetime.strptime(date_str, "%Y-%m-%d")
            return dt.strftime("%m.%Y")
        except ValueError:
            return date_str

    @staticmethod
    def build_date_range(start_date: Optional[str], end_date: Optional[str], is_current: bool) -> str:
        start = TextHelper.format_month_year(start_date)
        end = "obecnie" if is_current else TextHelper.format_month_year(end_date)

        if start and end:
            return f"{start} - {end}"
        if start:
            return start
        if end:
            return end
        return ""

    @staticmethod
    def split_lines(text: Optional[str]) -> List[str]:
        if not text:
            return []

        lines = [line.strip() for line in text.splitlines()]
        return [line for line in lines if line]

    @staticmethod
    def normalize_whitespace(text: Optional[str]) -> str:
        if not text:
            return ""
        return " ".join(str(text).split())

    @staticmethod
    def unique_non_empty(values: List[str]) -> List[str]:
        result = []
        seen = set()

        for value in values:
            cleaned = TextHelper.safe_strip(value)
            if cleaned and cleaned not in seen:
                result.append(cleaned)
                seen.add(cleaned)

        return result

    @staticmethod
    def take_first_non_empty(values: List[str], default: str = "") -> str:
        for value in values:
            cleaned = TextHelper.safe_strip(value)
            if cleaned:
                return cleaned
        return default

    @staticmethod
    def extract_bullet_lines(text: Optional[str], limit: int = 8) -> List[str]:
        if not text:
            return []

        lines = TextHelper.split_lines(text)
        bullets: List[str] = []

        for line in lines:
            normalized = line.strip()

            if normalized.startswith("-"):
                normalized = normalized[1:].strip()

            if not normalized:
                continue

            bullets.append(normalized)

        return bullets[:limit]

    @staticmethod
    def safe_url(url: Optional[str]) -> str:
        cleaned = TextHelper.safe_strip(url)
        if not cleaned:
            return ""

        if cleaned.startswith("http://") or cleaned.startswith("https://"):
            return cleaned

        return f"https://{cleaned}"

    @staticmethod
    def build_full_name(first_name: Optional[str], last_name: Optional[str]) -> str:
        full_name = f"{TextHelper.safe_strip(first_name)} {TextHelper.safe_strip(last_name)}".strip()
        return full_name