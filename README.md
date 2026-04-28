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
```

Przykładowy przepływ działania: 

```txt
Użytkownik wybiera ofertę i konfigurację CV
        ↓
Frontend wysyła żądanie do backendu
        ↓
Backend zapisuje dane i publikuje zadanie w RabbitMQ
        ↓
Worker Python odbiera zadanie
        ↓
Worker pobiera dane użytkownika i oferty
        ↓
System wybiera odpowiednie sekcje CV
        ↓
Worker generuje plik PDF
        ↓
Backend udostępnia gotowe CV do pobrania
```
Technologie
Backend
ASP.NET Core 8
Minimal APIs
Entity Framework Core
ASP.NET Identity
JWT Authentication
SQL Server
RabbitMQ Client
Docker
Frontend
Angular
TypeScript
Bootstrap
Reactive Forms
Angular Signals
HTTP Client
Worker
Python 3.12
RabbitMQ / pika
ReportLab
Docker
Integracja z modelem LLM
Infrastruktura
Docker
Docker Compose
SQL Server 2022
RabbitMQ Management

Uruchomienie projektu

1. Klonowanie repozytorium
git clone https://github.com/Waclas-M/job-hunt-orchestrator-app.git
cd job-hunt-orchestrator-app

2. Uruchomienie przez Docker Compose
docker compose up --build

Po uruchomieniu dostępne będą usługi:

Frontend:   http://localhost:4200
Backend:    http://localhost:5000
RabbitMQ:   http://localhost:15672
SQL Server: localhost:1433

API backendu

Backend udostępnia REST API odpowiedzialne za:

obsługę użytkowników,
logowanie i generowanie tokenów JWT,
zarządzanie profilem użytkownika,
pobieranie ofert pracy,
uruchamianie procesu generowania CV,
pobieranie wygenerowanych plików PDF.

Przykładowe endpointy:

POST   /login
POST   /register
GET    /api/UserCvDetailsData
POST   /UploadCV
GET    /DownloadCV/{id}
GET    /LaborMarketOffers
POST   /GenerateCV

Autoryzowane endpointy wymagają nagłówka:

Authorization: Bearer <token>
Worker Python

Worker odpowiada za obsługę zadań związanych z generowaniem CV.

Jego główne zadania:

nasłuchiwanie kolejki RabbitMQ,
odbieranie wiadomości z backendu,
pobieranie danych użytkownika,
analiza oferty pracy,
wybór odpowiednich sekcji CV,
wygenerowanie dokumentu PDF,
przekazanie wyniku do backendu lub zapisanie go w bazie.

Przykładowa wiadomość w kolejce:

{
  "userId": "user-id",
  "offerUrl": "https://example.com/job-offer",
  "profileId": 1,
  "userEducationsProcessAuto": true,
  "userExperiencesProcessAuto": true,
  "userSkillsProcessAuto": true,
  "userStrengsProcessAuto": true,
  "userEducationsIds": [],
  "userExperiencesIds": [],
  "userSkillsIds": [],
  "userStrengsIds": []
}
Generowanie CV

CV generowane jest po stronie workera Python z użyciem biblioteki ReportLab.

System obsługuje między innymi:

dane osobowe,
zdjęcie profilowe,
profil zawodowy,
doświadczenie,
wykształcenie,
projekty,
umiejętności,
języki,
mocne strony,
zainteresowania,
dane kontaktowe.

Dokument PDF jest tworzony na podstawie szablonu CV. Projekt zakłada możliwość dodawania wielu szablonów, np.:

ClassicGreenCvTemplate - istnieje
ModernCvTemplate
MinimalCvTemplate

Sekcja projektów może zawierać:

- nazwa projektu,
- opis,
- zakres dat,
- technologie,
- link.
Baza danych

Projekt korzysta z SQL Server oraz Entity Framework Core.

Przykładowe główne encje:

AppUser
Profile
UserProfilePersonalData
Education
JobExperience
Skill
Strength
Language
Interest
ProfilePhoto
LaborMarketOffer
GeneratedCvFile
Project

Relacje danych obejmują między innymi:

użytkownik posiada wiele profili,
profil posiada dane osobowe,
profil posiada wiele doświadczeń,
profil posiada wiele wpisów edukacji,
profil posiada wiele umiejętności,
profil posiada wiele projektów,
profil może posiadać zdjęcie profilowe,
użytkownik może posiadać wiele wygenerowanych CV.
Frontend

Frontend został zbudowany w Angularze i odpowiada za interakcję użytkownika z systemem.

Główne widoki aplikacji:

logowanie,
rejestracja,
dashboard,
ustawienia profilu,
zarządzanie sekcjami CV,
lista ofert pracy,
konfiguracja generowania CV,
modal konfiguracji CV,
lista wygenerowanych dokumentów.

Frontend wykorzystuje:

komponenty standalone,
formularze reaktywne,
walidację danych,
Bootstrap,
komunikację z API przez HTTP Client,
dynamiczne modale do konfiguracji CV.

Plany rozwoju

Planowane lub możliwe rozszerzenia projektu:

dodanie większej liczby szablonów CV,
edytor CV przed wygenerowaniem pliku PDF,
podgląd CV w przeglądarce,
integracja z portalami z ofertami pracy,
automatyczne scrapowanie ofert pracy,
analiza zgodności CV z ofertą,
scoring dopasowania użytkownika do oferty,
obsługa listów motywacyjnych,
eksport danych użytkownika,
historia wygenerowanych dokumentów,
panel administratora,
lepsze zarządzanie kategoriami ofert,
obsługa wielu języków CV,
wdrożenie aplikacji na serwer produkcyjny.
Status projektu

Projekt jest w fazie rozwoju. Aktualnie rozwijane są funkcje związane z:

generowaniem CV,
wyborem danych do dokumentu,
obsługą profili użytkownika,
komunikacją backendu z workerem,
poprawą wyglądu CV,
integracją z ofertami pracy.
Autor

Projekt tworzony przez:

Maciej Węcłaś

Projekt realizowany jako aplikacja wspierająca proces poszukiwania pracy oraz jako praktyczne wykorzystanie technologii:

ASP.NET Core,
Angular,
Python,
RabbitMQ,
SQL Server,
Docker,
AI/LLM,
generowanie dokumentów PDF.
Licencja

Projekt prywatny / edukacyjny.
