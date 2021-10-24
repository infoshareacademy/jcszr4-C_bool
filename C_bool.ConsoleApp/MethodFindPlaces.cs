using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_bool.ConsoleApp
{
    class MethodFindPlaces
    {
        public static void TransportOption()
        {
            Console.Clear();
            int tansportOptionsCount;
            int selectOptionsTransport;
            List<string> transportOptions = new List<string>();
            transportOptions.Add("CATEGORY 1. airport");
            transportOptions.Add("CATEGORY 2. bus_station");
            transportOptions.Add("CATEGORY 3. car_rental");
            transportOptions.Add("CATEGORY 4. light_rail_station");
            transportOptions.Add("CATEGORY 5. subway_station");
            transportOptions.Add("CATEGORY 6. taxi_stand");
            transportOptions.Add("CATEGORY 7. train_station");
            tansportOptionsCount = transportOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in transportOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionTransport(out selectOptionsTransport, tansportOptionsCount);
            Console.WriteLine($"Your chose is: \t{transportOptions[selectOptionsTransport - 1]}");
        }
        public static int SelectOptionTransport(out int selectOptionsTransport, int tansportOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsTransport))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionTransport(out selectOptionsTransport, tansportOptionsCount);
            }
            else if (selectOptionsTransport > tansportOptionsCount || selectOptionsTransport <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionTransport(out selectOptionsTransport, tansportOptionsCount);
            }

            Console.Clear();
            return (selectOptionsTransport);
        }
        public static void FoodOptions()
        {
            Console.Clear();
            int foodOptionsCount;
            int selectOptionsFood;
            List<string> foodOptions = new List<string>();
            foodOptions.Add("CATEGORY 1. bakery");
            foodOptions.Add("CATEGORY 2. cafe");
            foodOptions.Add("CATEGORY 3. convenience_store");
            foodOptions.Add("CATEGORY 4. meal_delivery");
            foodOptions.Add("CATEGORY 5. meal_takeaway");
            foodOptions.Add("CATEGORY 6. restaurant");
            foodOptionsCount = foodOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in foodOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionTransport(out selectOptionsFood, foodOptionsCount);
            Console.WriteLine($"Your chose is: \t{foodOptions[selectOptionsFood - 1]}");
        }
        public static int SelectOptionFood(out int selectOptionsFood, int foodOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsFood))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionFood(out selectOptionsFood, foodOptionsCount);
            }
            else if (selectOptionsFood > foodOptionsCount || selectOptionsFood <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionFood(out selectOptionsFood, foodOptionsCount);
            }

            Console.Clear();
            return (selectOptionsFood);
        }
        public static void CultureOptions()
        {
            Console.Clear();
            int cultureOptionsCount;
            int selectOptionsCulture;
            List<string> cultureOptions = new List<string>();
            cultureOptions.Add("CATEGORY 1. art_gallery");
            cultureOptions.Add("CATEGORY 2. movie_rental");
            cultureOptions.Add("CATEGORY 3. movie_theater");
            cultureOptions.Add("CATEGORY 4. museum");

            cultureOptionsCount = cultureOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in cultureOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionCulture(out selectOptionsCulture, cultureOptionsCount);
            Console.WriteLine($"Your chose is: \t{cultureOptions[selectOptionsCulture - 1]}");
        }
        public static int SelectOptionCulture(out int selectOptionsCulture, int cultureOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsCulture))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionCulture(out selectOptionsCulture, cultureOptionsCount);
            }
            else if (selectOptionsCulture > cultureOptionsCount || selectOptionsCulture <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionCulture(out selectOptionsCulture, cultureOptionsCount);
            }

            Console.Clear();
            return (selectOptionsCulture);
        }
        public static void ServicesOptions()
        {
            Console.Clear();
            int servicesOptionsCount;
            int selectOptionsServices;
            List<string> servicesOptions = new List<string>();
            servicesOptions.Add("CATEGORY 1. atm");
            servicesOptions.Add("CATEGORY 2. beauty_salon");
            servicesOptions.Add("CATEGORY 3. book_store");
            servicesOptions.Add("CATEGORY 4. car_repair");
            servicesOptions.Add("CATEGORY 5. car_wash");
            servicesOptions.Add("CATEGORY 6. electrician");
            servicesOptions.Add("CATEGORY 7. florist");
            servicesOptions.Add("CATEGORY 8. funeral_home");
            servicesOptions.Add("CATEGORY 9. hair_care");
            servicesOptions.Add("CATEGORY 10. insurance_agency");
            servicesOptions.Add("CATEGORY 11. laundry");
            servicesOptions.Add("CATEGORY 12. lawyer");
            servicesOptions.Add("CATEGORY 13. locksmith");
            servicesOptions.Add("CATEGORY 14. moving_company");
            servicesOptions.Add("CATEGORY 15. painter");
            servicesOptions.Add("CATEGORY 16. parking");
            servicesOptions.Add("CATEGORY 17. plumber");


            servicesOptionsCount = servicesOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in servicesOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionServices(out selectOptionsServices, servicesOptionsCount);
            Console.WriteLine($"Your chose is: \t{servicesOptions[selectOptionsServices - 1]}");
        }
        public static int SelectOptionServices(out int selectOptionsServices, int servicesOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsServices))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionServices(out selectOptionsServices, servicesOptionsCount);
            }
            else if (selectOptionsServices > servicesOptionsCount || selectOptionsServices <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionServices(out selectOptionsServices, servicesOptionsCount);
            }

            Console.Clear();
            return (selectOptionsServices);
        }
        public static void EnterainmentOptions()
        {
            Console.Clear();
            int entertainmentOptionsCount;
            int selectOptionsEntertainment;
            List<string> entertainmentOptions = new List<string>();
            entertainmentOptions.Add("CATEGORY 1. amusement_park");
            entertainmentOptions.Add("CATEGORY 2. aquarium");
            entertainmentOptions.Add("CATEGORY 3. bowling_alley");
            entertainmentOptions.Add("CATEGORY 4. casino");
            entertainmentOptions.Add("CATEGORY 5. library");
            entertainmentOptions.Add("CATEGORY 6. night_club");
            entertainmentOptions.Add("CATEGORY 7. spa");
            entertainmentOptions.Add("CATEGORY 8. zoo");

            entertainmentOptionsCount = entertainmentOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in entertainmentOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionEntertainment(out selectOptionsEntertainment, entertainmentOptionsCount);
            Console.WriteLine($"Your chose is: \t{entertainmentOptions[selectOptionsEntertainment - 1]}");
        }
        public static int SelectOptionEntertainment(out int selectOptionsEntertainment, int entertainmentOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsEntertainment))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionEntertainment(out selectOptionsEntertainment, entertainmentOptionsCount);
            }
            else if (selectOptionsEntertainment > entertainmentOptionsCount || selectOptionsEntertainment <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionEntertainment(out selectOptionsEntertainment, entertainmentOptionsCount);
            }

            Console.Clear();
            return (selectOptionsEntertainment);
        }
        public static void FinancePlacesOptions()
        {
            Console.Clear();
            int financePlacesOptionsCount;
            int selectOptionsFinancePlaces;
            List<string> financePlacesOptions = new List<string>();
            financePlacesOptions.Add("CATEGORY 1. accounting");
            financePlacesOptions.Add("CATEGORY 2. bank");

            financePlacesOptionsCount = financePlacesOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in financePlacesOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionFinancePlaces(out selectOptionsFinancePlaces, financePlacesOptionsCount);
            Console.WriteLine($"Your chose is: \t{financePlacesOptions[selectOptionsFinancePlaces - 1]}");
        }
        public static int SelectOptionFinancePlaces(out int selectOptionsFinancePlaces, int financePlacesOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsFinancePlaces))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionFinancePlaces(out selectOptionsFinancePlaces, financePlacesOptionsCount);
            }
            else if (selectOptionsFinancePlaces > financePlacesOptionsCount || selectOptionsFinancePlaces <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionFinancePlaces(out selectOptionsFinancePlaces, financePlacesOptionsCount);
            }

            Console.Clear();
            return (selectOptionsFinancePlaces);
        }
        public static void StateOfficesOptions()
        {
            Console.Clear();
            int stateOfficesOptionsCount;
            int selectOptionsStateOffices;
            List<string> stateOfficesOptions = new List<string>();
            stateOfficesOptions.Add("CATEGORY 1. city_hall");
            stateOfficesOptions.Add("CATEGORY 2. courthouse");
            stateOfficesOptions.Add("CATEGORY 3. embassy");
            stateOfficesOptions.Add("CATEGORY 4. fire_station");
            stateOfficesOptions.Add("CATEGORY 5. local_government_office");
            stateOfficesOptions.Add("CATEGORY 6. police");
            stateOfficesOptions.Add("CATEGORY 7. post_office");


            stateOfficesOptionsCount = stateOfficesOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in stateOfficesOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionStateOffices(out selectOptionsStateOffices, stateOfficesOptionsCount);
            Console.WriteLine($"Your chose is: \t{stateOfficesOptions[selectOptionsStateOffices - 1]}");
        }
        public static int SelectOptionStateOffices(out int selectOptionsStateOffices, int stateOfficesOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsStateOffices))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionStateOffices(out selectOptionsStateOffices, stateOfficesOptionsCount);
            }
            else if (selectOptionsStateOffices > stateOfficesOptionsCount || selectOptionsStateOffices <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionStateOffices(out selectOptionsStateOffices, stateOfficesOptionsCount);
            }

            Console.Clear();
            return (selectOptionsStateOffices);
        }
        public static void TourismAndRecreationOptions()
        {
            Console.Clear();
            int tourismAndRecreationOptionsCount;
            int selectOptionsTourismAndRecreation;
            List<string> tourismAndRecreationOptions = new List<string>();
            tourismAndRecreationOptions.Add("CATEGORY 1. campground");
            tourismAndRecreationOptions.Add("CATEGORY 2. lodging");
            tourismAndRecreationOptions.Add("CATEGORY 3. park");
            tourismAndRecreationOptions.Add("CATEGORY 4. rv_park");
            tourismAndRecreationOptions.Add("CATEGORY 5. tourist_attraction");
            tourismAndRecreationOptions.Add("CATEGORY 6. transit_station");
            tourismAndRecreationOptions.Add("CATEGORY 7. travel_agency");



            tourismAndRecreationOptionsCount = tourismAndRecreationOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in tourismAndRecreationOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionTourismAndRecreation(out selectOptionsTourismAndRecreation, tourismAndRecreationOptionsCount);
            Console.WriteLine($"Your chose is: \t{tourismAndRecreationOptions[selectOptionsTourismAndRecreation - 1]}");
        }
        public static int SelectOptionTourismAndRecreation(out int selectOptionsTourismAndRecreation, int tourismAndRecreationOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsTourismAndRecreation))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionTourismAndRecreation(out selectOptionsTourismAndRecreation, tourismAndRecreationOptionsCount);
            }
            else if (selectOptionsTourismAndRecreation > tourismAndRecreationOptionsCount || selectOptionsTourismAndRecreation <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionTourismAndRecreation(out selectOptionsTourismAndRecreation, tourismAndRecreationOptionsCount);
            }

            Console.Clear();
            return (selectOptionsTourismAndRecreation);
        }
        public static void PlacesOfCultOptions()
        {
            Console.Clear();
            int placesOfCultOptionsCount;
            int selectOptionsPlacesOfCult;
            List<string> placesOfCultOptions = new List<string>();
            placesOfCultOptions.Add("CATEGORY 1. cemetery");
            placesOfCultOptions.Add("CATEGORY 2. church");
            placesOfCultOptions.Add("CATEGORY 3. mosque");
            placesOfCultOptions.Add("CATEGORY 4. synagogue");
            
            placesOfCultOptionsCount = placesOfCultOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in placesOfCultOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionPlacesOfCult(out selectOptionsPlacesOfCult, placesOfCultOptionsCount);
            Console.WriteLine($"Your chose is: \t{placesOfCultOptions[selectOptionsPlacesOfCult - 1]}");
        }
        public static int SelectOptionPlacesOfCult(out int selectOptionsPlacesOfCult, int placesOfCultOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsPlacesOfCult))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionPlacesOfCult(out selectOptionsPlacesOfCult, placesOfCultOptionsCount);
            }
            else if (selectOptionsPlacesOfCult > placesOfCultOptionsCount || selectOptionsPlacesOfCult <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionPlacesOfCult(out selectOptionsPlacesOfCult, placesOfCultOptionsCount);
            }

            Console.Clear();
            return (selectOptionsPlacesOfCult);
        }
        public static void MedicalServicesOptions()
        {
            Console.Clear();
            int medicalServicesOptionsCount;
            int selectOptionsMedicalServices;
            List<string> medicalServicesOptions = new List<string>();
            medicalServicesOptions.Add("CATEGORY 1. dentist");
            medicalServicesOptions.Add("CATEGORY 2. doctor");
            medicalServicesOptions.Add("CATEGORY 3. hospital");
            medicalServicesOptions.Add("CATEGORY 4. physiotherapist");
            medicalServicesOptions.Add("CATEGORY 5. veterinary_care");


            medicalServicesOptionsCount = medicalServicesOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in medicalServicesOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionMedicalServices(out selectOptionsMedicalServices, medicalServicesOptionsCount);
            Console.WriteLine($"Your chose is: \t{medicalServicesOptions[selectOptionsMedicalServices - 1]}");
        }
        public static int SelectOptionMedicalServices(out int selectOptionsMedicalServices, int medicalServicesOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsMedicalServices))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionMedicalServices(out selectOptionsMedicalServices, medicalServicesOptionsCount);
            }
            else if (selectOptionsMedicalServices > medicalServicesOptionsCount || selectOptionsMedicalServices <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionMedicalServices(out selectOptionsMedicalServices, medicalServicesOptionsCount);
            }

            Console.Clear();
            return (selectOptionsMedicalServices);
        }
        public static void ShopsOptions()
        {
            Console.Clear();
            int shopsOptionsCount;
            int selectOptionsShops;
            List<string> shopsOptions = new List<string>();
            shopsOptions.Add("CATEGORY 1. bicycle_store");
            shopsOptions.Add("CATEGORY 2. car_dealer");
            shopsOptions.Add("CATEGORY 3. clothing_store");
            shopsOptions.Add("CATEGORY 4. department_store");
            shopsOptions.Add("CATEGORY 5. electronics_store");
            shopsOptions.Add("CATEGORY 6. furniture_store");
            shopsOptions.Add("CATEGORY 7. hardware_store");
            shopsOptions.Add("CATEGORY 8. jewelry_store");
            shopsOptions.Add("CATEGORY 9. pet_store");
            shopsOptions.Add("CATEGORY 10. real_estate_agency");
            shopsOptions.Add("CATEGORY 11. shoe_store");
            shopsOptions.Add("CATEGORY 12. shopping_mall");
            shopsOptions.Add("CATEGORY 13. store");
            shopsOptions.Add("CATEGORY 14. supermarket");
            
            shopsOptionsCount = shopsOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in shopsOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionShops(out selectOptionsShops, shopsOptionsCount);
            Console.WriteLine($"Your chose is: \t{shopsOptions[selectOptionsShops - 1]}");
        }
        public static int SelectOptionShops(out int selectOptionsShops, int shopsOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsShops))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionShops(out selectOptionsShops, shopsOptionsCount);
            }
            else if (selectOptionsShops > shopsOptionsCount || selectOptionsShops <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionShops(out selectOptionsShops, shopsOptionsCount);
            }

            Console.Clear();
            return (selectOptionsShops);
        }
        public static void EducationOptions()
        {
            Console.Clear();
            int educationOptionsCount;
            int selectOptionsEducation;
            List<string> educationOptions = new List<string>();
            educationOptions.Add("CATEGORY 1. primary_school");
            educationOptions.Add("CATEGORY 2. school");
            educationOptions.Add("CATEGORY 3. secondary_school");
            educationOptions.Add("CATEGORY 4. university");

            educationOptionsCount = educationOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in educationOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionEducation(out selectOptionsEducation, educationOptionsCount);
            Console.WriteLine($"Your chose is: \t{educationOptions[selectOptionsEducation - 1]}");
        }
        public static int SelectOptionEducation(out int selectOptionsEducation, int educationOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsEducation))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionEducation(out selectOptionsEducation, educationOptionsCount);
            }
            else if (selectOptionsEducation > educationOptionsCount || selectOptionsEducation <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionEducation(out selectOptionsEducation, educationOptionsCount);
            }

            Console.Clear();
            return (selectOptionsEducation);
        }
        public static void PlacesWithAlcoholOptions()
        {
            Console.Clear();
            int placesWithAlcoholOptionsCount;
            int selectOptionsPlacesWithAlcohol;
            List<string> placesWithAlcoholOptions = new List<string>();
            placesWithAlcoholOptions.Add("CATEGORY 1. bar");
            placesWithAlcoholOptions.Add("CATEGORY 2. liquor_store");

            placesWithAlcoholOptionsCount = placesWithAlcoholOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in placesWithAlcoholOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionPlacesWithAlcohol(out selectOptionsPlacesWithAlcohol, placesWithAlcoholOptionsCount);
            Console.WriteLine($"Your chose is: \t{placesWithAlcoholOptions[selectOptionsPlacesWithAlcohol - 1]}");
        }
        public static int SelectOptionPlacesWithAlcohol(out int selectOptionsPlacesWithAlcohol, int placesWithAlcoholOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsPlacesWithAlcohol))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionPlacesWithAlcohol(out selectOptionsPlacesWithAlcohol, placesWithAlcoholOptionsCount);
            }
            else if (selectOptionsPlacesWithAlcohol > placesWithAlcoholOptionsCount || selectOptionsPlacesWithAlcohol <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionPlacesWithAlcohol(out selectOptionsPlacesWithAlcohol, placesWithAlcoholOptionsCount);
            }

            Console.Clear();
            return (selectOptionsPlacesWithAlcohol);
        }

        public static void PharmacyOptions()
        {
            Console.Clear();
            int pharmacyOptionsCount;
            int selectOptionsPharmacy;
            List<string> pharmacyOptions = new List<string>();
            pharmacyOptions.Add("CATEGORY 1. drugstore");
            pharmacyOptions.Add("CATEGORY 2. pharmacy");

            pharmacyOptionsCount = pharmacyOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in pharmacyOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionPharmacy(out selectOptionsPharmacy, pharmacyOptionsCount);
            Console.WriteLine($"Your chose is: \t{pharmacyOptions[selectOptionsPharmacy - 1]}");
        }
        public static int SelectOptionPharmacy(out int selectOptionsPharmacy, int pharmacyOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsPharmacy))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionPharmacy(out selectOptionsPharmacy, pharmacyOptionsCount);
            }
            else if (selectOptionsPharmacy > pharmacyOptionsCount || selectOptionsPharmacy <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionPharmacy(out selectOptionsPharmacy, pharmacyOptionsCount);
            }

            Console.Clear();
            return (selectOptionsPharmacy);
        }
        public static void OtherOptions()
        {
            Console.Clear();
            int otherOptionsCount;
            int selectOptionsOther;
            List<string> otherOptions = new List<string>();
            otherOptions.Add("CATEGORY 1. gas_station");
            otherOptions.Add("CATEGORY 2. gym");
            otherOptions.Add("CATEGORY 3. hindu_temple");
            otherOptions.Add("CATEGORY 4. home_goods_store");
            otherOptions.Add("CATEGORY 5. roofing_contractor");
            otherOptions.Add("CATEGORY 6. stadium");
            otherOptions.Add("CATEGORY 7. storage");

            otherOptionsCount = otherOptions.Count;

            Console.WriteLine();
            Console.WriteLine("choose from the available options ");
            foreach (var option in otherOptions)
            {
                Console.WriteLine($"{option}");
            }

            SelectOptionOther(out selectOptionsOther, otherOptionsCount);
            Console.WriteLine($"Your chose is: \t{otherOptions[selectOptionsOther - 1]}");
        }
        public static int SelectOptionOther(out int selectOptionsOther, int otherOptionsCount)
        {
            if (!int.TryParse(Console.ReadLine(), out selectOptionsOther))
            {
                Console.WriteLine("Something wrong... Try again!");
                SelectOptionOther(out selectOptionsOther, otherOptionsCount);
            }
            else if (selectOptionsOther > otherOptionsCount || selectOptionsOther <= 0)
            {
                Console.WriteLine("Wrong number of options. Try again!");
                SelectOptionOther(out selectOptionsOther, otherOptionsCount);
            }

            Console.Clear();
            return (selectOptionsOther);
        }
    }
}
