using System;
using System.Collections.Generic;
using System.IO;
using C_bool.BLL.Repositories;
using C_bool.ConsoleApp.ConsoleHelpers;

namespace C_bool.ConsoleApp
{
    public class Menu
    {
        private static readonly char _indicator = '►';
        private bool _isWorking = true;
        private bool _isDataSourceSelected;

        private readonly List<string> _dataSourceList = new()
        {
            { _indicator + " Plik" },
            { "  API" },
            { "  Czysta aplikacja" },
            { "  Zamknij aplikację" }
        };

        private readonly List<string> _menuList = new()
        {
            { _indicator + " Wyświetl klasyfikację użytkowników" },
            { "  Dodaj nowego użytkownika" },
            { "  Edytuj wybranego użytkownika" },
            { "  Wyświetl wszystkie miejsca w okolicy" },
            { "  Wyświetl miejsca w określonym zasięgu" },
            { "  Wyświetl miejsca określonego typu" },
            { "  Edytuj wybrane miejsce" },
            { "  Wybierz ponownie źródło danych" },
            { "  Zamknij aplikację" }
        };

        private PlacesRepository _placesRepository = new();
        private UsersRepository _usersRepository = new();

        public void StartProgram()
        {
            while (_isWorking)
            {
                Console.Clear();

                if (!_isDataSourceSelected)
                {
                    Console.WriteLine("+++ WITAJ W APLIKACJI C_bool +++\n");
                    SelectDataSource();
                    _isDataSourceSelected = true;
                }
                else
                {
                    SelectActionFromMenu();
                }
            }
        }

        private void SelectActionFromMenu()
        {
            Console.WriteLine("MENU:\n");

            switch (SelectPositionFromMenu(_menuList))
            {
                case 0: // Wyswietl klasyfikacje uzytkowników
                    Console.Clear();
                    Console.WriteLine(_menuList[0] + "\n");

                    PrintInfo.UserClassification(_usersRepository);

                    BackToMenu();
                    break;
                case 1: // Dodaj nowego uzytkownika
                    Console.Clear();
                    Console.WriteLine(_menuList[1] + "\n");

                    BackToMenu();
                    break;
                case 2: // Edytuj wybranego uzytkownika
                    Console.Clear();
                    Console.WriteLine(_menuList[2] + "\n");

                    BackToMenu();
                    break;
                case 3: // Wyswietl wszystkie miejsca w okolicy
                    Console.Clear();
                    Console.WriteLine(_menuList[3] + "\n");

                    PrintInfo.PlacesNearby(_placesRepository);

                    BackToMenu();
                    break;
                case 4: // Wyswietl miejsca w okreslonym zasiegu
                    Console.Clear();
                    Console.WriteLine(_menuList[4] + "\n");

                    PrintInfo.PlacesByRadius(_placesRepository);

                    BackToMenu();
                    break;
                case 5: // Wyswietl miejsca okreslonego typu
                    Console.Clear();
                    Console.WriteLine(_menuList[5] + "\n");

                    PrintInfo.PlacesByCategory(_placesRepository);

                    BackToMenu();
                    break;
                case 6: // Edytuj wybrane miejsce
                    Console.Clear();
                    Console.WriteLine(_menuList[6] + "\n");

                    BackToMenu();
                    break;
                case 7: // Wybierz ponownie zródlo danych
                    Console.Clear();
                    _isDataSourceSelected = false;
                    break;
                case 8: // Zamknij aplikacje
                    Console.Clear();
                    _isWorking = false;
                    break;
            }
        }

        private void SelectDataSource()
        {
            Console.WriteLine("Wybierz źródło danych dla aplikacji:\n");

            var index = SelectPositionFromMenu(_dataSourceList);

            switch (index)
            {
                case 0: // Plik
                    Console.Clear();
                    try
                    {
                        _usersRepository.AddFileDataToRepository();
                        _placesRepository.AddFileDataToRepository();
                    }
                    catch (FileNotFoundException)
                    {
                        var errorMessage = "[ERROR] Nie znaleziono pliku! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }
                    catch (IOException)
                    {
                        var errorMessage = "[ERROR] Błąd odczytu pliku! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }
                    catch (ArgumentNullException)
                    {
                        var errorMessage = "[ERROR] Plik jest pusty! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }

                    break;
                case 1: //API
                    Console.Clear();
                    try
                    {
                        _usersRepository.AddFileDataToRepository();
                    }
                    catch (FileNotFoundException)
                    {
                        var errorMessage = "[ERROR] Nie znaleziono pliku! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }
                    catch (IOException)
                    {
                        var errorMessage = "[ERROR] Błąd odczytu pliku! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }
                    catch (ArgumentNullException)
                    {
                        var errorMessage = "[ERROR] Plik jest pusty! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }

                    try
                    {
                        _placesRepository.AddApiDataToRepository(
                            ReadDataFromConsole.GetLatitudeFromConsole(),
                            ReadDataFromConsole.GetLongitudeFromConsole(),
                            ReadDataFromConsole.GetRadiusFromConsole(),
                            ReadDataFromConsole.GetApiKeyFromConsole()
                        );
                    }
                    catch (Exception)
                    {
                        var errorMessage = "[ERROR] Błąd komunikacji z API! Czy chcesz wybrać inne źródło danych?";
                        HandleDataSourceError(index, errorMessage);
                    }

                    break;
                case 2: // Czysta aplikacja
                    _usersRepository = new UsersRepository();
                    _placesRepository = new PlacesRepository();
                    break;
                case 3: // Zamknij aplikacje
                    _isWorking = false;
                    break;
            }
        }

        private void HandleDataSourceError(int index, string errorMessage)
        {
            if (ReadDataFromConsole.ConfirmPrompt(errorMessage))
            {
                {
                    if (!_dataSourceList[index].Contains("[ERROR]"))
                        _dataSourceList[index] += " [ERROR]";
                }
                StartProgram();
            }
            else
            {
                _isWorking = false;
            }
        }

        private int SelectPositionFromMenu(List<string> menu)
        {
            var index = 0;

            while (true)
            {
                Console.SetCursorPosition(0, 4);

                PrintMenuPositions(menu);
                PrintMoveLegend();

                var move = Console.ReadKey(true).Key;

                if (move == ConsoleKey.DownArrow && index < menu.Count - 1)
                {
                    menu[index] = menu[index].Replace(_indicator, ' ');
                    index++;
                    menu[index] = _indicator + menu[index].Substring(1);
                }
                else if (move == ConsoleKey.UpArrow && index > 0)
                {
                    menu[index] = menu[index].Replace(_indicator, ' ');
                    index--;
                    menu[index] = _indicator + menu[index].Substring(1);
                }
                else if (move == ConsoleKey.Enter)
                {
                    menu[index] = menu[index].Replace(_indicator, ' ');
                    menu[0] = _indicator + menu[0].Substring(1);
                    return index;
                }
            }
        }

        private void PrintMenuPositions(List<string> menu)
        {
            foreach (var position in menu)
            {
                if (position.Contains("Zamknij aplikację") ||
                    position.Contains("Wyświetl wszystkie miejsca w okolicy") ||
                    position.Contains("Wybierz ponownie źródło danych"))
                {
                    Console.WriteLine();
                }

                if (position.Contains(_indicator))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\r{position}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"\r{position}");
                }
            }
        }

        private void BackToMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n\n '{ConsoleKey.Enter.ToString().ToUpper()}' - powrót do MENU");
            Console.ResetColor();
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }

        private void PrintMoveLegend()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                $"\n\n '{ConsoleKey.UpArrow}' - w górę || '{ConsoleKey.DownArrow}' - w dół || '{ConsoleKey.Enter.ToString().ToUpper()}' - wybierz ");
            Console.ResetColor();
        }
    }
}