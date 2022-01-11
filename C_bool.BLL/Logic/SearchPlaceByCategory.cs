using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Logic
{
    public class SearchPlaceByCategory
    {
        public static Dictionary<string, HashSet<string>> PlaceCategories { get; set; } = new()
        {
            {
                "transport",
                new HashSet<string>
                {
                    "airport",
                    "bus_station",
                    "car_rental",
                    "light_rail_station",
                    "subway_station",
                    "taxi_stand",
                    "train_station"
                }
            },
            {
                "food",
                new HashSet<string>
                {
                    "bakery",
                    "cafe",
                    "convenience_store",
                    "meal_delivery",
                    "meal_takeaway",
                    "restaurant",
                    "bar"
                }
            },
            {
                "culture",
                new HashSet<string>
                {
                    "art_gallery",
                    "movie_rental",
                    "movie_theater",
                    "museum"
                }
            },
            {
                "services",
                new HashSet<string>
                {
                    "atm",
                    "beauty_salon",
                    "book_store",
                    "car_repair",
                    "car_wash",
                    "electrician",
                    "florist",
                    "funeral_home",
                    "hair_care",
                    "insurance_agency",
                    "laundry",
                    "lawyer",
                    "locksmith",
                    "moving_company",
                    "painter",
                    "parking",
                    "plumber"
                }
            },
            {
                "entertainment",
                new HashSet<string>
                {
                    "amusement_park",
                    "aquarium",
                    "bowling_alley",
                    "casino",
                    "library",
                    "night_club",
                    "spa",
                    "zoo"
                }
            },
            {
                "finance_places",
                new HashSet<string>
                {
                    "accounting",
                    "bank",
                    "atm"
                }
            },
            {
                "state_offices",
                new HashSet<string>
                {
                    "city_hall",
                    "courthouse",
                    "embassy",
                    "fire_station",
                    "local_government_office",
                    "police",
                    "post_office"
                }
            },
            {
                "tourism_and_recreation",
                new HashSet<string>
                {
                    "campground",
                    "lodging",
                    "park",
                    "rv_park",
                    "tourist_attraction",
                    "transit_station",
                    "travel_agency"
                }
            },
            {
                "places_of_cult",
                new HashSet<string>
                {
                    "cemetery",
                    "church",
                    "mosque",
                    "synagogue",
                    "hindu_temple",
                }
            },
            {
                "medical_services",
                new HashSet<string>
                {
                    "dentist",
                    "doctor",
                    "hospital",
                    "physiotherapist",
                    "veterinary_care"
                }
            },
            {
                "shops",
                new HashSet<string>
                {
                    "bicycle_store",
                    "car_dealer",
                    "clothing_store",
                    "department_store",
                    "electronics_store",
                    "furniture_store",
                    "hardware_store",
                    "jewelry_store",
                    "pet_store",
                    "real_estate_agency",
                    "shoe_store",
                    "shopping_mall",
                    "store",
                    "supermarket",
                    "liquor_store"
                }
            },
            {
                "education",
                new HashSet<string>
                {
                    "primary_school",
                    "school",
                    "secondary_school",
                    "university"
                }
            },
            {
                "places_with_alcohol",
                new HashSet<string>
                {
                    "bar",
                    "liquor_store"
                }
            },
            {
                "pharmacy",
                new HashSet<string>
                {
                    "drugstore",
                    "pharmacy"
                }
            },
            {
                "other",
                new HashSet<string>
                {
                    "gas_station",
                    "gym",
                    "home_goods_store",
                    "roofing_contractor",
                    "stadium",
                    "storage"
                }
            },
            {
                "sports_places",
                new HashSet<string>
                {
                    "gym",
                    "stadium"
                }

            }
        };

        public static Dictionary<string, string> PlaceCategoriesTranslated { get; set; } = new()
        {
            { "casino", "Dla dorosłych - Kasyno" },
            { "night_club", "Dla dorosłych - Klub Nocny" },
            { "church", "Kult religijny - Kościół" },
            { "mosque", "Kult religijny - Meczet" },
            { "synagogue", "Kult religijny - Synagoga" },
            { "hindu_temple", "Kult religijny - Świątynia Indyjska" },
            { "tourist_attraction", "Kultura i rozrywka - Atrakcja Turystyczna" },
            { "library", "Kultura i rozrywka - Biblioteka" },
            { "art_gallery", "Kultura i rozrywka - Galeria Sztuki" },
            { "movie_theater", "Kultura i rozrywka - Kino" },
            { "bowling_alley", "Kultura i rozrywka - Kręgielnia" },
            { "book_store", "Kultura i rozrywka - Księgarnia" },
            { "museum", "Kultura i rozrywka - Muzeum" },
            { "aquarium", "Kultura i rozrywka - Oceanarium" },
            { "zoo", "Kultura i rozrywka - Ogród Zoologiczny" },
            { "park", "Kultura i rozrywka - Park" },
            { "amusement_park", "Kultura i rozrywka - Park Rozrywki" },
            { "stadium", "Kultura i rozrywka - Stadion" },
            { "movie_rental", "Kultura i rozrywka - Wypożyczalnia Filmów" },
            { "school", "Nauka - Szkoła" },
            { "primary_school", "Nauka - Szkoła Podstawowa" },
            { "secondary_school", "Nauka - Szkoła Średnia" },
            { "university", "Nauka - Uczelnia wyższa" },
            { "travel_agency", "Podróże - Biuro Podróży" },
            { "bus_station", "Podróże - Dworzec Autobusowy" },
            { "campground", "Podróże - Kemping" },
            { "lodging", "Podróże - Kwatera" },
            { "airport", "Podróże - Lotnisko" },
            { "parking", "Podróże - Parking" },
            { "taxi_stand", "Podróże - Postój Taksówek" },
            { "light_rail_station", "Podróże - Stacja Kolei Miejskiej" },
            { "train_station", "Podróże - Stacja Kolejowa" },
            { "subway_station", "Podróże - Stacja Metra" },
            { "transit_station", "Podróże - Stacja Przejściowa" },
            { "car_rental", "Podróże - Wypożyczalnia Samochodów" },
            { "embassy", "POI - Ambasada" },
            { "bank", "POI - Bank" },
            { "atm", "POI - Bankomat" },
            { "cemetery", "POI - Cmentarz" },
            { "car_dealer", "POI - Dealer Samochodowy" },
            { "funeral_home", "POI - Dom Pogrzebowy" },
            { "post_office", "POI - Poczta" },
            { "police", "POI - Policja" },
            { "city_hall", "POI - Ratusz" },
            { "courthouse", "POI - Sąd" },
            { "gas_station", "POI - Stacja Paliw" },
            { "fire_station", "POI - Straż Pożarna" },
            { "local_government_office", "POI - Urząd Miejski" },
            { "restaurant", "Restauracja" },
            { "meal_takeaway", "Restauracja -  Fastfood" },
            { "meal_delivery", "Restauracja -  Z Zdostawą" },
            { "bar", "Restauracja - Bar" },
            { "cafe", "Restauracja - Kawiarnia" },
            { "car_repair", "Serwis Samochodowy" },
            { "store", "Sklep" },
            { "home_goods_store", "Sklep - Artykuły Gospodarstwa Domowego" },
            { "shopping_mall", "Sklep - Centrum Handlowe" },
            { "department_store", "Sklep - Dom Handlowy" },
            { "electronics_store", "Sklep - Elektronika AGD/RTV" },
            { "furniture_store", "Sklep - Meblowy" },
            { "liquor_store", "Sklep - Monopolowy" },
            { "hardware_store", "Sklep - Narzędziowy" },
            { "shoe_store", "Sklep - Obuwniczy" },
            { "clothing_store", "Sklep - Odzieżowy" },
            { "bakery", "Sklep - Piekarnia" },
            { "bicycle_store", "Sklep - Rowerowy" },
            { "convenience_store", "Sklep - Spożywczy" },
            { "supermarket", "Sklep - Supermarket" },
            { "pet_store", "Sklep - Zoologiczny" },
            { "real_estate_agency", "Usługi - Agencja Nieruchomości" },
            { "insurance_agency", "Usługi - Agencja Ubezpieczeniowa" },
            { "electrician", "Usługi - Elektryk" },
            { "moving_company", "Usługi - Firma Przeprowadzkowa" },
            { "plumber", "Usługi - Hydraulik" },
            { "jewelry_store", "Usługi - Jubiler" },
            { "accounting", "Usługi - Księgowość" },
            { "florist", "Usługi - Kwiaciarnia" },
            { "storage", "Usługi - Magazyn" },
            { "painter", "Usługi - Malarz" },
            { "car_wash", "Usługi - Myjnia Samochodowa" },
            { "laundry", "Usługi - Pralnia" },
            { "lawyer", "Usługi - Prawnik" },
            { "locksmith", "Usługi - Ślusarz" },
            { "drugstore", "Zdrowie - Apteka" },
            { "pharmacy", "Zdrowie - Apteka" },
            { "dentist", "Zdrowie - Dentysta" },
            { "physiotherapist", "Zdrowie - Fizjoterapeuta" },
            { "doctor", "Zdrowie - Lekarz" },
            { "veterinary_care", "Zdrowie - Opieka Weterynaryjna" },
            { "hair_care", "Zdrowie - Pielęgnacja Włosów" },
            { "beauty_salon", "Zdrowie - Salon Piękności" },
            { "gym", "Zdrowie - Siłownia" },
            { "spa", "Zdrowie - SPA" },
            { "hospital", "Zdrowie - Szpital" }
        };

        /// <summary>
        /// Gets Place list where at least one of Place Type property elements match searched subcategories
        /// </summary>
        /// <param name="places">List of Place objects where to search from</param>
        /// <param name="mainCategory">Searched main category (key) from PlaceCategories dictionary</param>
        /// <returns>List of Place objects with matched subcategories</returns>
        public static List<Place> GetPlaces(List<Place> places, string mainCategory)
        {
            PlaceCategories.TryGetValue(mainCategory, out var subCategories);
            return places.Where(place => subCategories != null && subCategories.Overlaps(place.Types)).ToList();
        }

        /// <summary>
        /// Gets Place list where at least one of Place Type property elements match searched subcategory
        /// </summary>
        /// <param name="places">List of Place objects where to search from</param>
        /// <param name="subCategory">Searched subcategory</param>
        /// <returns>List of Place objects with matched subcategory</returns>
        public static List<Place> GetPlacesExactCategory(List<Place> places, string subCategory)
        {
            return places.Where(place => place.Types.Contains(subCategory)).ToList();
        }

    }
}
