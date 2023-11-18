# NoticeBoard

Instrukcja instalacji:

1. Pobieramy projekt klikając CODE -> Download Zip
2. Wypakowujemy
3. Uruchamiamy za pomocą Visual Studio plik NoticeBoard.sln z folderu NoticeBoard-master
4. Znajdujemy w okienku solution explorer (po prawej stronie) plik appsettings.json i otwieramy go
5. Jeśli mamy bazę danych SQL Server na lokalnym komputerze, to edytujemy linikę DefaultConnection, aby wyglądała tak:
"DefaultConnection": "Server=.\\\SQLEXPRESS;Database=NoticeBoard;User Id=(twoja nazwa użytkownika);Password=(twoje hasło);"
Kropka i ukośnik ".\\" to domyślny adres serwera na komputerze lokalnym, a SQLEXPRESS to domyślna nazwa servera.
7. Wpisujemy następującą komendę w Package Manager Console (na dole):
update-database
8. Następnie uruchamiamy aplikację
9. Rejestrujemy użytkownika, poprzez opcję w prawym górnym rogu ekranu Zarejestruj się
10. Mozemy teraz korzystać z naszej aplikacji!
