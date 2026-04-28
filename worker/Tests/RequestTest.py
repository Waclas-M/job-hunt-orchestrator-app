import requests

from Tests.modelApitest import ApiTest
import json

def Scrap(url: str):
    payload = {
        "url": url,
        "wait_seconds": 5
    }

    response = requests.post(
        "http://host.docker.internal:8000/fetch",
        json=payload,
        timeout=120
    )
    r = response.json()

    Response_format_v1 = {
                "type": "json_schema",
                "json_schema": {
                    "name": "job_offer_analysis",
                    "schema": {
                        "type": "object",
                        "properties": {
                            "company_name": {
                                "type": "string"
                            },
                            "requirements": {
                                "type": "array",
                                "items": {"type": "string"}
                            },
                            "responsibilites": {
                                "type": "array",
                                "items": {"type": "string"}
                            },
                            "technology": {
                                "type": "array",
                                "items": {"type": "string"}
                            }
                        },
                        "required": ["company_name", "requirements", "responsibilites","technology"],
                        "additionalProperties": False
                    }
                }
            }
    output = ApiTest(f"""Z podanego niżej html wyciągnij i wypisz tylko tekst
            Wyciągnij nazwę firmy {r['CompanyName']} \n
            Wyciągnij zawrte technologię (tylko Expected) {r['Technology']} \n
            Wyciągnij zadania na stanowisku {r['responsibilites']} \n
            Wyciągnij wymagania na stanowisko {r['requirements']} \n
            
            """, Response_format_v1)


    return output


Response_format_v2 = {
    "type": "json_schema",
    "json_schema": {
        "name": "job_offer_cv_indexes_selector",
        "schema": {
            "type": "object",
            "properties": {
                "EducationIndexes": {
                    "type": "array",
                    "items": {"type": "integer"}
                },
                "ExperienceIndexes": {
                    "type": "array",
                    "items": {"type": "integer"}
                },
                "StrengthIndexes": {
                    "type": "array",
                    "items": {"type": "integer"}
                },
                "SkillIndexes": {
                    "type": "array",
                    "items": {"type": "integer"}
                }
            },
            "required": [
                "EducationIndexes",
                "ExperienceIndexes",
                "StrengthIndexes",
                "SkillIndexes"
            ],
            "additionalProperties": False
        }
    }
}

def AutoDecision(
    scrapJson,
    UserEducationsProcessAuto,
    UserExperiencesProcessAuto,
    UserStrengsProcessAuto,
    UserSkillsProcessAuto,
    Education,
    Experience,
    Skills,
    Strengs
):
    output = ApiTest(f"""
Jesteś deterministycznym filtrem danych do CV.

Twoim zadaniem jest wybrać WYŁĄCZNIE indeksy elementów,
które powinny zostać użyte w CV dla danej oferty pracy.

ZWRACASZ WYŁĄCZNIE POPRAWNY JSON.
NIE zwracasz opisów, komentarzy, wyjaśnień ani pełnych obiektów.
Zwracasz tylko listy indeksów.

========================
DANE WEJŚCIOWE
========================

Offer:
{json.dumps(scrapJson, ensure_ascii=False)}

Education:
{json.dumps(Education, ensure_ascii=False)}

Experience:
{json.dumps(Experience, ensure_ascii=False)}

Skills:
{json.dumps(Skills, ensure_ascii=False)}

Strengs:
{json.dumps(Strengs, ensure_ascii=False)}

Flags:
{json.dumps({
    "UserEducationsProcessAuto": UserEducationsProcessAuto,
    "UserExperiencesProcessAuto": UserExperiencesProcessAuto,
    "UserStrengsProcessAuto": UserStrengsProcessAuto,
    "UserSkillsProcessAuto": UserSkillsProcessAuto
}, ensure_ascii=False)}

========================
ZNACZENIE INDEKSÓW
========================

Indeks oznacza pozycję elementu w danej tablicy wejściowej:
- pierwszy element ma indeks 0
- drugi element ma indeks 1
- itd.

Przykład:
jeśli chcesz wybrać pierwszy i trzeci element z Education,
to zwracasz:
"EducationIndexes": [0, 2]

========================
ZASADY KRYTYCZNE
========================

1. UŻYWAJ WYŁĄCZNIE danych wejściowych.
   Niczego nie wymyślaj.

2. NIE zwracaj pełnych obiektów.
   Zwracaj tylko indeksy.

3. NIE zmieniaj kolejności indeksów losowo.
   Posortuj indeksy rosnąco.

4. NIE duplikuj indeksów.

5. Indeks musi istnieć w wejściowej tablicy.
   Nie wolno zwracać indeksów spoza zakresu.

6. ZERO tekstu poza JSON.

7. Wybierz maksymalnie 7 najlepszych elementów na sekcję.

8. Wiebierz minimum 2 elementy na sekcję.

9. Zawsze stotsuj się do Logiki FLAG !!!

========================
LOGIKA SELEKCJI
========================

Dla każdej sekcji, jeśli odpowiadająca jej flaga = true:

1. Oceń każdy element pod kątem dopasowania do oferty pracy.

2. Kryteria dopasowania:
   - technologie
   - obowiązki
   - słowa kluczowe
   - zgodność branżowa
   - zgodność ze stanowiskiem

3. Usuń elementy słabo dopasowane.

4. Wybierz maksymalnie 5 najlepszych elementów na sekcję.

5. Jeśli żaden element nie pasuje wystarczająco:
   - zwróć 1 najlepszy indeks zamiast pustej listy

6. Jeśli element nie ma żadnego związku z ofertą:
   - traktuj go jako bardzo słaby kandydat
   - nie wybieraj go, chyba że musisz zwrócić 1 element zgodnie z zasadą powyżej

========================
ZASADY DLA FLAG
========================

Jeśli flaga sekcji = false:
- NIE filtruj
- zwróć indeksy wszystkich elementów z tej sekcji
- czyli:
  - dla tablicy 3-elementowej zwróć [0, 1, 2]
  - dla pustej tablicy zwróć []

========================
SEKCJE
========================

Education:
- wybieraj kierunki związane z IT, stanowiskiem lub ofertą
- ignoruj niepowiązane kierunki, jeśli flaga = true

Experience:
- wybieraj doświadczenie zgodne z technologiami, obowiązkami lub stanowiskiem
- ignoruj niepowiązane doświadczenie, jeśli flaga = true

Strengths:
- wybieraj tylko mocne strony przydatne w tej pracy
  albo bardzo blisko z nią powiązane
  Jeśli flaga = false zwróć wszystkie indexy.

Skills:
- wybieraj tylko technologie i umiejętności użyte w ofercie
  albo bardzo blisko z nią powiązane

========================
WYMAGANY FORMAT ODPOWIEDZI
========================

{{
  "EducationIndexes": [],
  "ExperienceIndexes": [],
  "StrengthIndexes": [],
  "SkillIndexes": []
}}
""", Response_format_v2)

    return output