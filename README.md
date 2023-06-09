# MovieRegister

## Funktioner

- **Sökning**: Användare kan söka efter filmer baserat på titel och genre.  Användaren kan även söka efter producenter.
- **Filminformation**: För en icke inloggad användare syns de inlagda filmernas bild, titel, genre och beskrivning. Medans en inloggad användare kan den ta del av information som vilken producent har producerat filmen.
- **Producentinformation**:  För en icke inloggad användare syns de inlagda producenternas namn, bild och ålder. Medans för en inloggad användare kan den ta del av vilka filmer den valda producenten har producerat.
- **Tillägg av filmer och producenter**: Enbart admin kan lägga till nya filmer och producenter i registret genom att fylla i relevant information i ett formulär.
- **Redigering och borttagning**: Användare med administratörsbehörighet kan redigera och ta bort befintliga filmer och producenter.
- **Användarhantering**: Registrerade användare kan logga in och då ta del av ytterliggare information och kopplingen mellan filmer och dess producent.

## Teknologier

- ASP.NET C#: Projektspråket och ramverket för att utveckla webbapplikationen.
- HTML/CSS: Används för att strukturera och styla användargränssnittet.
- Entity Framework: Ett objektrelationellt kartläggningsverktyg som används för att kommunicera med databasen.
- SQL Server: Databasen som lagrar filmregistrets information.


## Installation

1. Klona eller ladda ner projektet från GitHub-repositoriet.
2. Öppna projektet i Visual Studio. 
3. Konfigurera anslutningssträngen till SQL Server-databasen i appsettings.json.
4. Skriv "update-database" i Package-Manager-Console. Det för att fylla databasen med den redan skapade datan.
5. Starta applikationen och ta del av registret.

 
