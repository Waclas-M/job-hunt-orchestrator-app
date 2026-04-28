from Tools.PDF.FORM_1_GENERATOR import  build_cv_pdf

sample_data = {
        "cv": {
            "header": {
                "full_name": "MARIUSZ NOWAK",
                "title": "Specjalista ds. Sprzedaży",
                "summary": "Dokładny, pracowity i odpowiedzialny specjalista ds. handlu z dwuletnim doświadczeniem zawodowym w branży farmaceutycznej. Posiada wysoko rozwinięte umiejętności komunikacyjne i zdolności. Chętnie podejmuje wyzwania, które w większości zrealizowane są z sukcesem."
            },
            "sidebar": {
                "photo_path": "photo.jpg",
                "address": {"label": "ADRES", "lines": ["Dworcowa 231/543a", "60-180 Poznań"]},
                "contact": {"label": "KONTAKT", "lines": ["00077775555", "nazwisko@wp.pl"]},
                "languages": [
                    {"name": "Angielski", "level_dots": 7, "max_dots": 10},
                    {"name": "Niemiecki", "level_dots": 5, "max_dots": 10},
                    {"name": "Włoski", "level_dots": 4, "max_dots": 10},
                ],
                "skills": [
                    "Wiedza branżowa",
                    "Profesjonalna obsługa klienta",
                    "Znajomość i umiejętność wykorzystania technik sprzedaży",
                    "Wysoko rozwinięte umiejętności negocjacyjne",
                    "Umiejętność zarządzania czasem",
                ],
                # opcjonalnie:
                # "birthdate": "23.03.1989"
            },
            "main": {
                "education": [
                    {"date_range": "10.2008 - 07.2013", "school": "Wyższa Szkoła Handlu i Usług", "details": "Specjalizacja: Finanse i zarządzanie (licencjat)"}
                ],
                "experience": [
                    {
                        "date_range": "09.2011 - obecnie",
                        "company": "Lidl Polska",
                        "role": "Junior Buyer",
                        "bullets": [
                            "Obniżyłem koszty zakupu kosmetyków Isana o 15%",
                            "Zawierałem kontrakty z zagranicznymi dostawcami",
                            "Tworzyłem strategie sprzedażowe dla marki Isana",
                        ],
                    }
                ],
                "strengths_tags": [
                    "Doświadczenie w handlu",
                    "Wykształcenie kierunkowe",
                    "Samodzielność",
                    "Nastawienie na sukces",
                    "Asertywność",
                ],
                "interests": [
                    {"label": "Rozwój osobisty", "icon_path": "icons/growth.png"},
                    {"label": "Psychologia biznesu", "icon_path": "icons/psychology.png"},
                    {"label": "Sztuki Walki (Boks)", "icon_path": "icons/boxing.png"},
                ],
                "rodo_line": "Wyrażam zgodę na przetwarzanie moich danych osobowych dla potrzeb niezbędnych do realizacji procesu rekrutacji zgodnie z RODO."
            }
        }
    }

build_cv_pdf(sample_data, out_path="cv_output.pdf")
print("Zapisano: cv_output.pdf")


