using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using ArgumentNullException = System.ArgumentNullException;

namespace C_bool.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int selectOption;
            int menuOptionsCount;

            do
            {


                Console.WriteLine("Hello World!");
                List<string> menuOptions = new List<string>();
                if (menuOptions == null) throw new ArgumentNullException(nameof(menuOptions));
                menuOptions.Add("GROUP 1. TRANSPORT");
                menuOptions.Add("GROUP 2. FOOD");
                menuOptions.Add("GROUP 3. CULTURE");
                menuOptions.Add("GROUP 4. SERVICES");
                menuOptions.Add("GROUP 5. ENTERTAINMENT");
                menuOptions.Add("GROUP 6. FINANCE PLACES");
                menuOptions.Add("GROUP 7. STATE OFFICES");
                menuOptions.Add("GROUP 8. TOURISM AND RECREATION");
                menuOptions.Add("GROUP 9. PLACES OF CULT");
                menuOptions.Add("GROUP 10. MEDICAL SERVICES");
                menuOptions.Add("GROUP 11. SHOPS");
                menuOptions.Add("GROUP 12. EDUCATION");
                menuOptions.Add("GROUP 13. PLACES WITH ALCOHOL");
                menuOptions.Add("GROUP 14. PHARMACY");
                menuOptions.Add("GROUP 15. OTHER");

                menuOptionsCount = menuOptions.Count;

                foreach (var option in menuOptions)
                {
                    Console.WriteLine($"{option}");
                }

                Console.WriteLine();
                Console.WriteLine("choose from the available options ");

                SelectOption(out selectOption, menuOptionsCount);
                Console.Clear();
                Console.WriteLine($"Your chose is: \t{menuOptions[selectOption - 1]}");

                switch (selectOption)
                {
                    case 1:
                        MethodFindPlaces.TransportOption();
                        break;
                    case 2:
                        MethodFindPlaces.FoodOptions();
                        break;
                    case 3:
                        MethodFindPlaces.CultureOptions();
                        break;
                    case 4:
                        MethodFindPlaces.ServicesOptions();
                        break;
                    case 5:
                        MethodFindPlaces.EnterainmentOptions();
                        break;
                    case 6:
                        MethodFindPlaces.FinancePlacesOptions();
                        break;
                    case 7:
                        MethodFindPlaces.StateOfficesOptions();
                        break;
                    case 8:
                        MethodFindPlaces.TourismAndRecreationOptions();
                        break;
                    case 9:
                        MethodFindPlaces.PlacesOfCultOptions();
                        break;
                    case 10:
                        MethodFindPlaces.MedicalServicesOptions();
                        break;
                    case 11:
                        MethodFindPlaces.ShopsOptions();
                        break;
                    case 12:
                        MethodFindPlaces.EducationOptions();
                        break;
                    case 13:
                        MethodFindPlaces.PlacesWithAlcoholOptions();
                        break;
                    case 14:
                        MethodFindPlaces.PharmacyOptions();
                        break;
                    case 15:
                        MethodFindPlaces.OtherOptions();
                        break;
                }


            } while (selectOption < menuOptionsCount);
        }
        private static int SelectOption(out int selectOptions, int menuOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptions))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOption(out selectOptions, menuOptionsCount);
            }
            else if (selectOptions > menuOptionsCount || selectOptions <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOption(out selectOptions, menuOptionsCount);
            }

            Console.Clear();
            return (selectOptions);
        }
    }
}
