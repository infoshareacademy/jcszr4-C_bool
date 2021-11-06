using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;

namespace C_bool.BLL.Logic
{
    public class SearchPlaceByCategory
    {
        public static Dictionary<string, HashSet<string>> PlaceCategories { get; set; } = new()
        {
            {
                "transport", new HashSet<string>
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
                "food", new HashSet<string>
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
                "culture", new HashSet<string>
                {
                    "art_gallery",
                    "movie_rental",
                    "movie_theater",
                    "museum"
                }
            },
            {
                "services", new HashSet<string>
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
                "entertainment", new HashSet<string>
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
                "finance_places", new HashSet<string>
                {
                    "accounting",
                    "bank",
                    "atm"
                }
            },
            {
                "state_offices", new HashSet<string>
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
                "tourism_and_recreation", new HashSet<string>
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
                "places_of_cult", new HashSet<string>
                {
                    "cemetery",
                    "church",
                    "mosque",
                    "synagogue",
                    "hindu_temple",
                }
            },
            {
                "medical_services", new HashSet<string>
                {
                    "dentist",
                    "doctor",
                    "hospital",
                    "physiotherapist",
                    "veterinary_care"
                }
            },
            {
                "shops", new HashSet<string>
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
                "education", new HashSet<string>
                {
                    "primary_school",
                    "school",
                    "secondary_school",
                    "university"
                }
            },
            {
                "places_with_alcohol", new HashSet<string>
                {
                    "bar",
                    "liquor_store"
                }
            },
            {
                "pharmacy", new HashSet<string>
                {
                    "drugstore",
                    "pharmacy"
                }
            },
            {
                "other", new HashSet<string>
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
                    "stadion"
                }

            }
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
