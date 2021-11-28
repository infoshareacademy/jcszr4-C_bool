using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models.Places;

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
            { "accounting", "Księgowość" },
            { "airport", "Lotnisko" },
            { "amusement_park", "Park Rozrywki" },
            { "aquarium", "Akwarium" },
            { "art_gallery", "Galeria Sztuki" },
            { "atm", "Bankomat" },
            { "bakery", "Piekarnia" },
            { "bank", "Bank" },
            { "bar", "Bar" },
            { "beauty_salon", "Salon Piękności" },
            { "bicycle_store", "Sklep Rowerowy" },
            { "book_store", "Księgarnia" },
            { "bowling_alley", "Kręgielnia" },
            { "bus_station", "Dworzec Autobusowy" },
            { "cafe", "Kawiarnia" },
            { "campground", "Kemping" },
            { "car_dealer", "Sprzedawca Samochodów" },
            { "car_rental", "Wypożyczalnia Samochodów" },
            { "car_repair", "Serwis Samochodowy" },
            { "car_wash", "Myjnia Samochodowa" },
            { "casino", "Kasyno" },
            { "cemetery", "Cmentarz" },
            { "church", "Kościół" },
            { "city_hall", "Ratusz" },
            { "clothing_store", "Sklep Odzieżowy" },
            { "convenience_store", "Sklep Spożywczy" },
            { "courthouse", "Sąd" },
            { "dentist", "Dentysta" },
            { "department_store", "Dom Handlowy" },
            { "doctor", "Lekarz" },
            { "drugstore", "Apteka" },
            { "electrician", "Elektryk" },
            { "electronics_store", "Sklep Z Elektroniką" },
            { "embassy", "Ambasada" },
            { "fire_station", "Straż Pożarna" },
            { "florist", "Kwiaciarnia" },
            { "funeral_home", "Dom Pogrzebowy" },
            { "furniture_store", "Sklep Meblowy" },
            { "gas_station", "Stacja Paliw" },
            { "gym", "Siłownia" },
            { "hair_care", "Pielęgnacja Włosów" },
            { "hardware_store", "Sklep Z Narzędziami" },
            { "hindu_temple", "Świątynia Indyjska" },
            { "home_goods_store", "Home_Goods_Store" },
            { "hospital", "Szpital" },
            { "insurance_agency", "Agencja Ubezpieczeniowa" },
            { "jewelry_store", "Jubiler" },
            { "laundry", "Pralnia" },
            { "lawyer", "Prawnik" },
            { "library", "Biblioteka" },
            { "light_rail_station", "Stacja Kolei Miejskiej" },
            { "liquor_store", "Sklep Alkoholowy" },
            { "local_government_office", "Urząd Miejski" },
            { "locksmith", "Ślusarz" },
            { "lodging", "Kwatera" },
            { "meal_delivery", "Jedzenie Z Zdostawą" },
            { "meal_takeaway", "Fastfood" },
            { "mosque", "Meczet" },
            { "movie_rental", "Wypożyczalnia Filmów" },
            { "movie_theater", "Kino" },
            { "moving_company", "Firma Przeprowadzkowa" },
            { "museum", "Muzeum" },
            { "night_club", "Klub Nocny" },
            { "painter", "Malarz" },
            { "park", "Park" },
            { "parking", "Parking" },
            { "pet_store", "Sklep Zoologiczny" },
            { "pharmacy", "Apteka" },
            { "physiotherapist", "Fizjoterapeuta" },
            { "plumber", "Hydraulik" },
            { "police", "Policja" },
            { "post_office", "Poczta" },
            { "primary_school", "Szkoła Podstawowa" },
            { "real_estate_agency", "Agencja Nieruchomości" },
            { "restaurant", "Restauracja" },
            { "roofing_contractor", "Roofing_Contractor" },
            { "rv_park", "Rv_Park" },
            { "school", "Szkoła" },
            { "secondary_school", "Szkoła Średnia" },
            { "shoe_store", "Sklep Obuwniczy" },
            { "shopping_mall", "Centrum Handlowe" },
            { "spa", "Spa" },
            { "stadium", "Stadion" },
            { "storage", "Magazyn" },
            { "store", "Sklep" },
            { "subway_station", "Stacja Metra" },
            { "supermarket", "Supermarket" },
            { "synagogue", "Synagoga" },
            { "taxi_stand", "Postój Taksówek" },
            { "tourist_attraction", "Atrakcja Turystyczna" },
            { "train_station", "Stacja Kolejowa" },
            { "transit_station", "Stacja Przejściowa" },
            { "travel_agency", "Biuro Podróży" },
            { "university", "Uniwersytet" },
            { "veterinary_care", "Opieka Weterynaryjna" },
            { "zoo", "Ogród Zoologiczny" },
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
