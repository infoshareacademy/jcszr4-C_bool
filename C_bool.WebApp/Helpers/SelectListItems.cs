using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_bool.WebApp.Helpers
{
    public class SelectListItems
    {

        //TODO: Natalka tutaj :-)
        public static List<SelectListItem> gameTaskTypes { get; } = new()
        {
            new SelectListItem { Value = "ENUM", Text = "Zrób zdjęcie" },
        };

        public static List<SelectListItem> PlaceSearchRange { get; } = new()
        {
            new SelectListItem { Value = "1000", Text = "do 1 kilometra" },
            new SelectListItem { Value = "2000", Text = "do 2 kilometrów" },
            new SelectListItem { Value = "5000", Text = "do 5 kilometrów"  },
            new SelectListItem { Value = "10000", Text = "do 10 kilometrów"  },
            new SelectListItem { Value = "15000", Text = "do 15 kilometrów"  },
            new SelectListItem { Value = "30000", Text = "do 30 kilometrów"  },
            new SelectListItem { Value = "50000", Text = "do 50 kilometrów"  },
            new SelectListItem { Value = "100000", Text = "do 100 kilometrów"  },
            new SelectListItem { Value = "1000000", Text = "do 1000 kilometrów"  },
            new SelectListItem { Value = "40000000", Text = "Dawaj wszystkie!" },
        };

        public static List<SelectListItem> SearchForType { get; } = new()
        {
            new SelectListItem { Value = "place", Text = "Miejsca" },
            new SelectListItem { Value = "task", Text = "Zadania" },
        };


        //TODO: filtrowanie miejsc w widoku miejsc po kategoriach
        private static SelectListGroup _forAdults { get; } = new() { Name = "Dla dorosłych" };
        private static SelectListGroup _placesOfCult { get; } = new() { Name = "Obiekt kultu religijnego" };
        private static SelectListGroup _art { get; } = new() { Name = "Kultura i rozrywka" };
        private static SelectListGroup _school { get; } = new() { Name = "Nauka" };
        private static SelectListGroup _travel { get; } = new() { Name = "Podróże i transport" };
        private static SelectListGroup _poi { get; } = new() { Name = "POI" };
        private static SelectListGroup _restaurant { get; } = new() { Name = "Restauracje i kawiarnie" };
        private static SelectListGroup _store { get; } = new() { Name = "Sklepy" };
        private static SelectListGroup _service { get; } = new() { Name = "Usługi" };
        private static SelectListGroup _health { get; } = new() { Name = "Zdrowie i uroda" };

        public static List<SelectListItem> GooglePlaceCategories { get; } = new()
        {
            new SelectListItem { Value = "casino",  Text = "Kasyno", Group = _forAdults},
            new SelectListItem { Value = "night_club",  Text = "Klub Nocny", Group = _forAdults},
            new SelectListItem { Value = "church",  Text = "Kościół", Group = _placesOfCult},
            new SelectListItem { Value = "mosque",  Text = "Meczet", Group = _placesOfCult},
            new SelectListItem { Value = "synagogue",  Text = "Synagoga", Group = _placesOfCult},
            new SelectListItem { Value = "hindu_temple",  Text = "Świątynia Indyjska", Group = _placesOfCult},
            new SelectListItem { Value = "tourist_attraction",  Text = "Atrakcja Turystyczna", Group = _art},
            new SelectListItem { Value = "library",  Text = "Biblioteka", Group = _art},
            new SelectListItem { Value = "art_gallery",  Text = "Galeria Sztuki", Group = _art},
            new SelectListItem { Value = "movie_theater",  Text = "Kino", Group = _art},
            new SelectListItem { Value = "bowling_alley",  Text = "Kręgielnia", Group = _art},
            new SelectListItem { Value = "book_store",  Text = "Księgarnia", Group = _art},
            new SelectListItem { Value = "museum",  Text = "Muzeum", Group = _art},
            new SelectListItem { Value = "aquarium",  Text = "Oceanarium", Group = _art},
            new SelectListItem { Value = "zoo",  Text = "Ogród Zoologiczny", Group = _art},
            new SelectListItem { Value = "park",  Text = "Park", Group = _art},
            new SelectListItem { Value = "amusement_park",  Text = "Park Rozrywki", Group = _art},
            new SelectListItem { Value = "stadium",  Text = "Stadion", Group = _art},
            new SelectListItem { Value = "movie_rental",  Text = "Wypożyczalnia Filmów", Group = _art},
            new SelectListItem { Value = "school",  Text = "Szkoła", Group = _school},
            new SelectListItem { Value = "primary_school",  Text = "Szkoła Podstawowa", Group = _school},
            new SelectListItem { Value = "secondary_school",  Text = "Szkoła Średnia", Group = _school},
            new SelectListItem { Value = "university",  Text = "Uczelnia wyższa", Group = _school},
            new SelectListItem { Value = "travel_agency",  Text = "Biuro Podróży", Group = _travel},
            new SelectListItem { Value = "bus_station",  Text = "Dworzec Autobusowy", Group = _travel},
            new SelectListItem { Value = "campground",  Text = "Kemping", Group = _travel},
            new SelectListItem { Value = "lodging",  Text = "Kwatera", Group = _travel},
            new SelectListItem { Value = "airport",  Text = "Lotnisko", Group = _travel},
            new SelectListItem { Value = "parking",  Text = "Parking", Group = _travel},
            new SelectListItem { Value = "taxi_stand",  Text = "Postój Taksówek", Group = _travel},
            new SelectListItem { Value = "light_rail_station",  Text = "Stacja Kolei Miejskiej", Group = _travel},
            new SelectListItem { Value = "train_station",  Text = "Stacja Kolejowa", Group = _travel},
            new SelectListItem { Value = "subway_station",  Text = "Stacja Metra", Group = _travel},
            new SelectListItem { Value = "transit_station",  Text = "Stacja Przejściowa", Group = _travel},
            new SelectListItem { Value = "car_rental",  Text = "Wypożyczalnia Samochodów", Group = _travel},
            new SelectListItem { Value = "embassy",  Text = "Ambasada", Group = _poi},
            new SelectListItem { Value = "bank",  Text = "Bank", Group = _poi},
            new SelectListItem { Value = "atm",  Text = "Bankomat", Group = _poi},
            new SelectListItem { Value = "cemetery",  Text = "Cmentarz", Group = _poi},
            new SelectListItem { Value = "car_dealer",  Text = "Dealer Samochodowy", Group = _poi},
            new SelectListItem { Value = "funeral_home",  Text = "Dom Pogrzebowy", Group = _poi},
            new SelectListItem { Value = "post_office",  Text = "Poczta", Group = _poi},
            new SelectListItem { Value = "police",  Text = "Policja", Group = _poi},
            new SelectListItem { Value = "city_hall",  Text = "Ratusz", Group = _poi},
            new SelectListItem { Value = "courthouse",  Text = "Sąd", Group = _poi},
            new SelectListItem { Value = "gas_station",  Text = "Stacja Paliw", Group = _poi},
            new SelectListItem { Value = "fire_station",  Text = "Straż Pożarna", Group = _poi},
            new SelectListItem { Value = "local_government_office",  Text = "Urząd Miejski", Group = _poi},
            new SelectListItem { Value = "restaurant",  Text = "Restauracja", Group = _restaurant},
            new SelectListItem { Value = "meal_takeaway",  Text = " Fastfood", Group = _restaurant},
            new SelectListItem { Value = "meal_delivery",  Text = " Z Zdostawą", Group = _restaurant},
            new SelectListItem { Value = "bar",  Text = "Bar", Group = _restaurant},
            new SelectListItem { Value = "cafe",  Text = "Kawiarnia", Group = _restaurant},
            new SelectListItem { Value = "car_repair",  Text = "Serwis Samochodowy", Group = _service},
            new SelectListItem { Value = "store",  Text = "Sklep", Group = _store},
            new SelectListItem { Value = "home_goods_store",  Text = "Artykuły Gospodarstwa Domowego", Group = _store},
            new SelectListItem { Value = "shopping_mall",  Text = "Centrum Handlowe", Group = _store},
            new SelectListItem { Value = "department_store",  Text = "Dom Handlowy", Group = _store},
            new SelectListItem { Value = "electronics_store",  Text = "Elektronika AGD/RTV", Group = _store},
            new SelectListItem { Value = "furniture_store",  Text = "Meblowy", Group = _store},
            new SelectListItem { Value = "liquor_store",  Text = "Monopolowy", Group = _store},
            new SelectListItem { Value = "hardware_store",  Text = "Narzędziowy", Group = _store},
            new SelectListItem { Value = "shoe_store",  Text = "Obuwniczy", Group = _store},
            new SelectListItem { Value = "clothing_store",  Text = "Odzieżowy", Group = _store},
            new SelectListItem { Value = "bakery",  Text = "Piekarnia", Group = _store},
            new SelectListItem { Value = "bicycle_store",  Text = "Rowerowy", Group = _store},
            new SelectListItem { Value = "convenience_store",  Text = "Spożywczy", Group = _store},
            new SelectListItem { Value = "supermarket",  Text = "Supermarket", Group = _store},
            new SelectListItem { Value = "pet_store",  Text = "Zoologiczny", Group = _store},
            new SelectListItem { Value = "real_estate_agency",  Text = "Agencja Nieruchomości", Group = _service},
            new SelectListItem { Value = "insurance_agency",  Text = "Agencja Ubezpieczeniowa", Group = _service},
            new SelectListItem { Value = "electrician",  Text = "Elektryk", Group = _service},
            new SelectListItem { Value = "moving_company",  Text = "Firma Przeprowadzkowa", Group = _service},
            new SelectListItem { Value = "plumber",  Text = "Hydraulik", Group = _service},
            new SelectListItem { Value = "jewelry_store",  Text = "Jubiler", Group = _service},
            new SelectListItem { Value = "accounting",  Text = "Księgowość", Group = _service},
            new SelectListItem { Value = "florist",  Text = "Kwiaciarnia", Group = _service},
            new SelectListItem { Value = "storage",  Text = "Magazyn", Group = _service},
            new SelectListItem { Value = "painter",  Text = "Malarz", Group = _service},
            new SelectListItem { Value = "car_wash",  Text = "Myjnia Samochodowa", Group = _service},
            new SelectListItem { Value = "laundry",  Text = "Pralnia", Group = _service},
            new SelectListItem { Value = "lawyer",  Text = "Prawnik", Group = _service},
            new SelectListItem { Value = "locksmith",  Text = "Ślusarz", Group = _service},
            new SelectListItem { Value = "drugstore",  Text = "Apteka", Group = _health},
            new SelectListItem { Value = "pharmacy",  Text = "Apteka", Group = _health},
            new SelectListItem { Value = "dentist",  Text = "Dentysta", Group = _health},
            new SelectListItem { Value = "physiotherapist",  Text = "Fizjoterapeuta", Group = _health},
            new SelectListItem { Value = "doctor",  Text = "Lekarz", Group = _health},
            new SelectListItem { Value = "veterinary_care",  Text = "Opieka Weterynaryjna", Group = _health},
            new SelectListItem { Value = "hair_care",  Text = "Pielęgnacja Włosów", Group = _health},
            new SelectListItem { Value = "beauty_salon",  Text = "Salon Piękności", Group = _health},
            new SelectListItem { Value = "gym",  Text = "Siłownia", Group = _health},
            new SelectListItem { Value = "spa",  Text = "SPA", Group = _health},
            new SelectListItem { Value = "hospital",  Text = "Szpital", Group = _health},
        };
    }
}
