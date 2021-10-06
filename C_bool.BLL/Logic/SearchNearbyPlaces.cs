using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.Places;

namespace C_bool.BLL.Logic
{
    public static class SearchNearbyPlaces
    {
        const double pi = Math.PI;
        const double earthRadius = 6371000;

        public static double DistanceBetweenPlaces(double place1Latitude, double place1Longitude, double place2Latitude, double place2Longitude)
        {
            double sin_place1Latitude = Math.Sin(Radians(place1Latitude));
            double sin_place2Latitude = Math.Sin(Radians(place2Latitude));
            double cos_place1Latitude = Math.Cos(Radians(place1Latitude));
            double cos_place2Latitude = Math.Cos(Radians(place2Latitude));

            double distance = earthRadius * Math.Acos(sin_place1Latitude * sin_place2Latitude + cos_place1Latitude * cos_place2Latitude * Math.Cos(Radians(place1Longitude) - Radians(place2Longitude)));

            return distance;
        }

        public static double Radians(double x)
        {
            return x * pi / 180;
        }

        public static List<Place> GetPlaces(double lat1, double lon1, double radius, List<Place> places)
        {
            List<Place> outputList = new List<Place>();

            foreach (var place in places)
            {
                var distance = DistanceBetweenPlaces(lat1, lon1, place.Geometry.Location.Latitude,
                    place.Geometry.Location.Longitude);
                if (distance <= radius)
                {
                    outputList.Add(place);
                }
            }
            return outputList;
        }
    }
}
