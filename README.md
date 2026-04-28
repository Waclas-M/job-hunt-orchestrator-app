# JHOP – Job Hunt Orchestrator Project

JHOP to aplikacja wspierająca proces poszukiwania pracy poprzez automatyzację generowania CV dopasowanego do konkretnej oferty pracy. System pozwala użytkownikowi zarządzać profilem zawodowym, pobierać dane o ofertach pracy, wybierać informacje do CV oraz generować gotowy plik PDF.

Projekt został zbudowany w architekturze wielousługowej z wykorzystaniem backendu ASP.NET Core, frontendu Angular, workera Python oraz komunikacji asynchronicznej przez RabbitMQ.

---

## Spis treści

- [Opis projektu](#opis-projektu)
- [Główne funkcjonalności](#główne-funkcjonalności)
- [Architektura systemu](#architektura-systemu)
- [Technologie](#technologie)
- [Struktura projektu](#struktura-projektu)
- [Uruchomienie projektu](#uruchomienie-projektu)
- [Konfiguracja środowiska](#konfiguracja-środowiska)
- [API backendu](#api-backendu)
- [Worker Python](#worker-python)
- [Generowanie CV](#generowanie-cv)
- [Baza danych](#baza-danych)
- [Frontend](#frontend)
- [Docker](#docker)
- [Plany rozwoju](#plany-rozwoju)
- [Autor](#autor)

---

## Opis projektu

**JHOP** to aplikacja typu Job Hunt Orchestrator, której celem jest uproszczenie procesu aplikowania na oferty pracy. Użytkownik może utworzyć swój profil zawodowy zawierający dane osobowe, doświadczenie, wykształcenie, umiejętności, języki, mocne strony oraz projekty.

Na podstawie wybranej oferty pracy system może automatycznie lub ręcznie dobrać najbardziej pasujące elementy profilu użytkownika, a następnie wygenerować profesjonalne CV w formacie PDF.

Projekt łączy klasyczną aplikację webową z elementami automatyzacji, mikroserwisów i sztucznej inteligencji.

---

## Główne funkcjonalności

- Rejestracja i logowanie użytkownika.
- Obsługa autoryzacji JWT.
- Zarządzanie profilem zawodowym użytkownika.
- Dodawanie:
  - danych osobowych,
  - wykształcenia,
  - doświadczenia zawodowego,
  - umiejętności,
  - mocnych stron,
  - języków,
  - zainteresowań,
  - projektów.
- Pobieranie i przechowywanie ofert pracy.
- Wybór oferty pracy do wygenerowania CV.
- Ręczna konfiguracja sekcji CV.
- Automatyczny dobór danych do CV na podstawie oferty pracy.
- Komunikacja backendu z workerem przez RabbitMQ.
- Generowanie CV w formacie PDF.
- Pobieranie wygenerowanych plików CV.
- Obsługa zdjęcia profilowego.
- Praca w środowisku Docker Compose.

---

## Architektura systemu

Projekt składa się z kilku głównych komponentów:

```txt
JHOP
│
├── Frontend Angular
│   └── Interfejs użytkownika
│
├── Backend ASP.NET Core
│   ├── API REST
│   ├── Autoryzacja JWT
│   ├── Obsługa użytkowników
│   ├── Komunikacja z bazą danych
│   └── Wysyłanie zadań do RabbitMQ
│
├── Worker Python
│   ├── Odbieranie zadań z RabbitMQ
│   ├── Przetwarzanie danych użytkownika i oferty
│   ├── Opcjonalna integracja z LLM
│   └── Generowanie CV PDF
│
├── RabbitMQ
│   └── Kolejka zadań
│
└── SQL Server
    └── Baza danych aplikacji
