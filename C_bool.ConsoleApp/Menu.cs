using System;
using System.Collections.Generic;
using C_bool.BLL.Repositories;
using C_bool.ConsoleApp.Logic;

namespace C_bool.ConsoleApp
{
    public class Menu
    {
        private static readonly char _indicator = '►';
        private bool _isWorking = true;
        private bool _isDataSourceSelected;
        private readonly List<string> _dataSourceList = new()
        {
            {_indicator + " Plik"},
            {"  API"},
            {"  Czysta aplikacja"},
            {"  Zamknij aplikację"}
        };
        private readonly List<string> _menuList = new()
        {
            {_indicator + " Wyświetl klasyfikację użytkowników"},
            {"  Dodaj nowego użytkownika"},
            {"  Edytuj wybranego użytkownika"},
            {"  Wyświetl wszystkie miejsca w okolicy"},
            {"  Wyświetl miejsca w określonym zasięgu"},
            {"  Wyświetl miejsca określonego typu"},
            {"  Edytuj wybrane miejsce"},
            {"  Wybierz ponownie źródło danych"},
            {"  Zamknij aplikację"}
        };

        private PlacesRepository placesRepository = new PlacesRepository();
        private UsersRepository usersRepository = new UsersRepository();

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
                case 0:
                    Console.Clear();
                    Console.WriteLine(_menuList[0]);
                    BackToMenu();
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine(_menuList[1]);
                    BackToMenu();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine(_menuList[2]);
                    BackToMenu();
                    break;
                case 3:
                    Console.Clear();
                    GetInfo.PlaceInformation(placesRepository.Repository, "");
                    BackToMenu();
                    break;
                case 4:
                    Console.Clear();
                    ReadDataFromConsole.SetRadiusFromConsole();
                    GetInfo.PlaceInformation(placesRepository.GetNearbyPlacesFromRadius(ReadDataFromConsole.Radius), "");
                    BackToMenu();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine(_menuList[5]);
                    BackToMenu();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine(_menuList[6]);
                    BackToMenu();
                    break;
                case 7:
                    Console.Clear();
                    _isDataSourceSelected = false;
                    break;
                case 8:
                    Console.Clear();
                    _isWorking = false;
                    break;
            }
        }

        private void SelectDataSource()
        {
            Console.WriteLine("Wybierz źródło danych dla aplikacji:\n");

            switch (SelectPositionFromMenu(_dataSourceList))
            {
                case 0:
                    Console.Clear();
                    usersRepository.AddFileDataToRepository();
                    placesRepository.AddFileDataToRepository();
                    break;
                case 1:
                    Console.Clear();
                    usersRepository.AddFileDataToRepository();
                    placesRepository.AddApiDataToRepository(ReadDataFromConsole.Latitude, ReadDataFromConsole.Longitude, ReadDataFromConsole.Radius, ReadDataFromConsole.ApiKey);
                    break;
                case 2:
                    usersRepository = new UsersRepository();
                    placesRepository = new PlacesRepository();
                    break;
                case 3:
                    _isWorking = false;
                    break;
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